using AutoMapper;
using BusinessLogic.Abstractions;
using BusinessLogic.Utils;
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

        public CommentBusinessLogic(IRepository<Post> postRepository, IRepository<Comment> commentRepository,
            IRepository<CommentReact> commentReactRepository, IMapper mapper)
        {
            this.postRepository = postRepository;
            this.commentRepository = commentRepository;
            this.commentReactRepository = commentReactRepository;
            this.mapper = mapper;
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
            commentRepository.Insert(newComment);

            Post post = postRepository.GetById(comment.PostId);
            ++post.CommentsCount;
            postRepository.Update(post);
            postRepository.SaveChanges();

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
                    commentReactRepository.Delete(commentReact.Id);
                    commentReactRepository.SaveChanges();
                    ReactsUtils.UpdateDeltas(ref deltaUpvotes, ref deltaDownvotes, type, commentReact.Type);
                }
                else if (type != commentReact.Type)
                {
                    commentReact.Type = type;
                    commentReactRepository.Update(commentReact);
                    commentReactRepository.SaveChanges();
                    ReactsUtils.UpdateDeltas(ref deltaUpvotes, ref deltaDownvotes, type, commentReact.Type);
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
    }
}
