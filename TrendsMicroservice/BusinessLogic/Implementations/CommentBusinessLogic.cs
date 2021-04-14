using AutoMapper;
using BusinessLogic.Abstractions;
using DataAccess.Abstractions;
using Entities;
using Models;
using System;
using System.Collections.Generic;

namespace BusinessLogic.Implementations
{
    public class CommentBusinessLogic : ICommentBusinessLogic
    {
        private readonly IRepository<Comment> commentRepository;

        private readonly IMapper mapper;

        public CommentBusinessLogic(IRepository<Comment> _commentRepository, IMapper _mapper)
        {
            commentRepository = _commentRepository;
            mapper = _mapper;
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
            commentRepository.SaveChanges();

            return mapper.Map<CommentGetDto>(newComment);
        }

        void ICommentBusinessLogic.Update(CommentUpdateDto comment)
        {
            Comment updatedComment = commentRepository.GetById(comment.Id);
            if (updatedComment == null)
            {
                return;
            }
            mapper.Map<CommentUpdateDto, Comment>(comment, updatedComment);

            commentRepository.Update(updatedComment);
            commentRepository.SaveChanges();
        }

        void ICommentBusinessLogic.Delete(Guid id)
        {
            commentRepository.Delete(id);
            commentRepository.SaveChanges();
        }
    }
}
