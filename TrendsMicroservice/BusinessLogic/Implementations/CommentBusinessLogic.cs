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

        public CommentBusinessLogic(IRepository<Comment> _commentRepository)
        {
            commentRepository = _commentRepository;
        }

        ICollection<CommentDto> ICommentBusinessLogic.GetAll()
        {
            ICollection<Comment> comments = commentRepository.GetAll();
            ICollection<CommentDto> commentDtos = new List<CommentDto>();

            foreach (Comment c in comments)
            {
                TrendDto _trendDto = new TrendDto()
                {
                    Id = c.Post.Trend.Id,
                    Name = c.Post.Trend.Name
                };
                PostDto _postDto = new PostDto()
                {
                    Id = c.Post.Id,
                    Title = c.Post.Title,
                    MediaPath = c.Post.MediaPath,
                    Upvotes = c.Post.Upvotes,
                    Downvotes = c.Post.Downvotes,
                    TrendDto = _trendDto
                };
                commentDtos.Add(new CommentDto()
                {
                    Id = c.Id,
                    Text = c.Text,
                    Upvotes = c.Upvotes,
                    Downvotes = c.Downvotes,
                    PostId = c.PostId,
                    PostDto = _postDto
                });
            }
            return commentDtos;
        }
        CommentDto ICommentBusinessLogic.GetById(Guid id)
        {
            Comment comment = commentRepository.GetById(id);
            CommentDto commentDto = null;

            if (comment != null)
            {
                TrendDto _trendDto = new TrendDto()
                {
                    Id = comment.Post.Trend.Id,
                    Name = comment.Post.Trend.Name
                };
                PostDto _postDto = new PostDto()
                {
                    Id = comment.Post.Id,
                    Title = comment.Post.Title,
                    MediaPath = comment.Post.MediaPath,
                    Upvotes = comment.Post.Upvotes,
                    Downvotes = comment.Post.Downvotes,
                    TrendDto = _trendDto
                };
                commentDto = new CommentDto()
                {
                    Id = comment.Id,
                    Text = comment.Text,
                    Upvotes = comment.Upvotes,
                    Downvotes = comment.Downvotes,
                    PostId = comment.PostId,
                    PostDto = _postDto
                };
            }
            return commentDto;
        }

        void ICommentBusinessLogic.Create(CommentDto comment)
        {
            Trend _trend = new Trend()
            {
                Name = comment.PostDto.TrendDto.Name
            };
            Post _post = new Post()
            {
                Title = comment.PostDto.Title,
                MediaPath = comment.PostDto.MediaPath,
                Upvotes = comment.PostDto.Upvotes,
                Downvotes = comment.PostDto.Downvotes,
                TrendId = comment.PostDto.TrendId,
                Trend = _trend
            };
            Comment newComment = new Comment()
            {
                Text = comment.Text,
                Upvotes = comment.Upvotes,
                Downvotes = comment.Downvotes,
                PostId = comment.PostId,
                Post =_post
            };
            commentRepository.Insert(newComment);
            commentRepository.SaveChanges();
            comment.Id = newComment.Id;
        }

        void ICommentBusinessLogic.Update(CommentDto comment)
        {
            Trend _trend = new Trend()
            {
                Name = comment.PostDto.TrendDto.Name
            };
            Post _post = new Post()
            {
                Title = comment.PostDto.Title,
                MediaPath = comment.PostDto.MediaPath,
                Upvotes = comment.PostDto.Upvotes,
                Downvotes = comment.PostDto.Downvotes,
                Trend = _trend
            };
            Comment updatedComment = new Comment()
            {
                Id = comment.Id.Value,
                Text = comment.Text,
                Upvotes = comment.Upvotes,
                Downvotes = comment.Downvotes,
                PostId = comment.PostId,
                Post = _post
            };
            commentRepository.Update(updatedComment);
            commentRepository.SaveChanges();
        }
    }
}
