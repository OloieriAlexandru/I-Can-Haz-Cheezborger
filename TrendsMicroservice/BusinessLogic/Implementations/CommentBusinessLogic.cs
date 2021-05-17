using AutoMapper;
using BusinessLogic.Abstractions;
using DataAccess.Abstractions;
using Entities;
using Models.Comments;
using System;
using System.Collections.Generic;

namespace BusinessLogic.Implementations
{
    public class CommentBusinessLogic : ICommentBusinessLogic
    {
        private readonly IRepository<Post> postRepository;

        private readonly IRepository<Comment> commentRepository;

        private readonly IRepository<CommentReact> commentReactRepository;

        private readonly IMapper mapper;

        private readonly IContentScanTaskService contentScanService;

        public CommentBusinessLogic(IRepository<Post> postRepository, IRepository<Comment> commentRepository,
            IRepository<CommentReact> commentReactRepository, IMapper mapper)
        {
            this.postRepository = postRepository;
            this.commentRepository = commentRepository;
            this.commentReactRepository = commentReactRepository;
            this.mapper = mapper;
            this.contentScanService = contentScanService;
        }

        ICollection<CommentGetDto> ICommentBusinessLogic.GetAll(Guid postId)
        {
            ICollection<Comment> comments = commentRepository.GetAllByFilter(c => c.PostId == postId);
            return mapper.Map<ICollection<CommentGetDto>>(comments);
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
            contentScanService.CreateTask(post.MediaPath, post.Title, $"/api/v1/trends/{post.TrendId}/posts/{post.Id}/comments/{newComment.Id}");

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

            if (commentReact == null)
            {
                commentReact = new CommentReact
                {
                    CommentId = commentPatchReact.Id,
                    UserId = commentPatchReact.CreatorId,
                    Type = type
                };
            }
            else
            {
                if (type == ReactType.None)
                {
                    commentReactRepository.Delete(commentReact.Id);
                    commentReactRepository.SaveChanges();
                }
                else if (type != commentReact.Type)
                {
                    commentReact.Type = type;
                    commentReactRepository.Update(commentReact);
                    commentReactRepository.SaveChanges();
                }
            }
        }
    }
}
