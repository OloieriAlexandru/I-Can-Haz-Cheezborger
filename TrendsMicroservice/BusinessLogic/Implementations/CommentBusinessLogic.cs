using AutoMapper;
using BusinessLogic.Abstractions;
using BusinessLogic.Utils;
using DataAccess.Abstractions;
using Entities;
using Models;
using Models.Comments;
using Models.Common;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Implementations
{
    public class CommentBusinessLogic : ICommentBusinessLogic
    {
        private readonly IRepository<Post> postRepository;

        private readonly IRepository<Comment> commentRepository;

        private readonly IRepository<CommentReact> commentReactRepository;

        private readonly IMapper mapper;

        private readonly IContentScanTaskService contentScanTaskService;

        public CommentBusinessLogic(IRepository<Post> postRepository, IRepository<Comment> commentRepository,
            IRepository<CommentReact> commentReactRepository, IMapper mapper, IContentScanTaskService contentScanTaskService)
        {
            this.postRepository = postRepository;
            this.commentRepository = commentRepository;
            this.commentReactRepository = commentReactRepository;
            this.mapper = mapper;
            this.contentScanTaskService = contentScanTaskService;
        }

        ICollection<CommentGetDto> ICommentBusinessLogic.GetAll(Guid postId, UserInfoModel userInfo)
        {
            ICollection<Comment> comments = commentRepository.GetAllByFilter(c => c.PostId == postId, "Reacts");
            ICollection<CommentGetDto> returnedComemnts = new List<CommentGetDto>();

            foreach (Comment comment in comments)
            {
                CommentGetDto commentGetDto = mapper.Map<CommentGetDto>(comment);
                if (userInfo != null && comment.Reacts != null)
                {
                    CommentReact react = comment.Reacts.FirstOrDefault(cr => cr.UserId == userInfo.CreatorId);
                    if (react != null)
                    {
                        if (react.Type == ReactType.Like)
                        {
                            commentGetDto.Liked = true;
                        }
                        else if (react.Type == ReactType.Dislike)
                        {
                            commentGetDto.Disliked = true;
                        }
                    }
                }
                returnedComemnts.Add(commentGetDto);
            }

            return returnedComemnts;
        }

        CommentGetDto ICommentBusinessLogic.GetById(Guid id)
        {
            Comment comment = commentRepository.GetById(id);
            if (comment == null)
            {
                return null;
            }
            return mapper.Map<CommentGetDto>(comment);
        }

        CommentGetDto ICommentBusinessLogic.Create(CommentCreateDto comment)
        {
            Comment newComment = mapper.Map<Comment>(comment);
            newComment.ApprovedImage = false;
            newComment.ApprovedText = false;
            commentRepository.Insert(newComment);

            Post post = postRepository.GetById(comment.PostId);
            ++post.CommentsCount;
            postRepository.Update(post);
            postRepository.SaveChanges();

            CreateContentScanTaskDto createContentScanTaskDto = new CreateContentScanTaskDto()
            {
                ImageUrl = null,
                Text = newComment.Text,
                CallbackUrl = $"/api/v1/trends/{post.TrendId}/posts/{post.Id}/comments/{newComment.Id}/content-scan-result"
            };
            contentScanTaskService.CreateTask(createContentScanTaskDto);

            return mapper.Map<CommentGetDto>(newComment);
        }

        void ICommentBusinessLogic.Patch(CommentPatchDto comment)
        {
            Comment updatedComment = commentRepository.GetById(comment.Id);
            if (updatedComment == null)
            {
                return;
            }
            mapper.Map<CommentPatchDto, Comment>(comment, updatedComment);

            commentRepository.Update(updatedComment);
            commentRepository.SaveChanges();
        }

        void ICommentBusinessLogic.Delete(Guid id)
        {
            Comment comment = commentRepository.GetById(id);

            Post post = postRepository.GetById(comment.PostId);
            --post.CommentsCount;
            postRepository.Update(post);

            commentRepository.Delete(id);
            commentRepository.SaveChanges();
        }

        void ICommentBusinessLogic.PatchReact(CommentPatchReactDto commentPatchReact)
        {
            CommentReact commentReact = commentReactRepository.GetByFilter(
                cr => cr.UserId == commentPatchReact.CreatorId && cr.CommentId == commentPatchReact.Id);
            ReactType type = (ReactType)Enum.Parse(typeof(ReactType), commentPatchReact.Type);

            int deltaUpvotes = 0, deltaDownvotes = 0;
            if (commentReact == null)
            {
                commentReact = new CommentReact
                {
                    CommentId = commentPatchReact.Id,
                    UserId = commentPatchReact.CreatorId,
                    Type = type
                };
                commentReactRepository.Insert(commentReact);
                commentRepository.SaveChanges();
                ReactsUtils.UpdateDeltas(ref deltaUpvotes, ref deltaDownvotes, type, ReactType.None);
            }
            else
            {
                if (type == ReactType.None)
                {
                    ReactsUtils.UpdateDeltas(ref deltaUpvotes, ref deltaDownvotes, type, commentReact.Type);
                    commentReactRepository.Delete(commentReact.Id);
                    commentReactRepository.SaveChanges();
                }
                else if (type != commentReact.Type)
                {
                    ReactsUtils.UpdateDeltas(ref deltaUpvotes, ref deltaDownvotes, type, commentReact.Type);
                    commentReact.Type = type;
                    commentReactRepository.Update(commentReact);
                    commentReactRepository.SaveChanges();
                }
            }
            if (deltaUpvotes != 0 || deltaDownvotes != 0)
            {
                Comment comment = commentRepository.GetById(commentPatchReact.Id);
                comment.Upvotes += deltaUpvotes;
                comment.Downvotes += deltaDownvotes;
                commentRepository.Update(comment);
                commentRepository.SaveChanges();
            }
        }

        void ICommentBusinessLogic.PatchContentScanTaskApprovals(Guid id, PatchContentScanTaskApprovalsDto taskApprovalsDto)
        {
            Comment comment = commentRepository.GetById(id);
            comment.ApprovedImage = taskApprovalsDto.ApprovedImage;
            comment.ApprovedText = taskApprovalsDto.ApprovedText;
            commentRepository.Update(comment);
            commentRepository.SaveChanges();
        }
    }
}
