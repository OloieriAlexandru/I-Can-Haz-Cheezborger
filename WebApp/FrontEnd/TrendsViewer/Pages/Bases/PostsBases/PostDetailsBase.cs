using AutoMapper;
using Microsoft.AspNetCore.Components;
using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrendsViewer.Models;
using TrendsViewer.Services;
using TrendsViewer.Services.Abstractions;

namespace TrendsViewer.Pages
{
    public class PostDetailsBase : ComponentBase
    {
        [Inject]
        public IPostService PostService { get; set; }
        [Inject]
        public ICommentService CommentService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IMapper Mapper { get; set; }

        [Parameter]
        public string TrendId { get; set; }
        [Parameter]
        public string PostId { get; set; }

        public PostGetByIdDto Post { get; set; }
        public ICollection<CommentGetDto> Comments { get; set; }

        public CommentModel commentModel = new CommentModel();
        public CommentCreateDto createdComment;

        public PostDetailsBase()
        {
            createdComment = new CommentCreateDto();
        }

        protected async override Task OnInitializedAsync()
        {
            Post = await PostService.GetPost(Guid.Parse(TrendId), Guid.Parse(PostId));
            Comments = new List<CommentGetDto>(await CommentService.GetComments(Guid.Parse(TrendId), Guid.Parse(PostId)));
        }

        protected async Task HandleValidSubmit()
        {
            CommentCreateDto newComment = new CommentCreateDto { Text = commentModel.CommentText, PostId = Guid.Parse(PostId)};
            await CommentService.CreateComment(Guid.Parse(TrendId), Guid.Parse(PostId), newComment);
            NavigationManager.NavigateTo($"/trends/{TrendId}/posts/{PostId}", forceLoad: true);
        }

        protected async Task HandleDeleteComment(Guid commentId)
        {
            await CommentService.DeleteComment(Guid.Parse(TrendId), Guid.Parse(PostId), commentId);

            foreach (CommentGetDto comment in Comments)
            {
                if (comment.Id == commentId)
                {
                    Comments.Remove(comment);
                    break;
                }
            }
        }
        protected void LikePostId(PostGetByIdDto post)
        {
            if (post.LikeClicked)
            {
                post.Upvotes--;
            }
            else
            {
                post.Upvotes++;
                if (post.DislikeClicked)
                {
                    post.Downvotes--;
                    post.DislikeClicked = !post.DislikeClicked;
                }
            }
            post.LikeClicked = !post.LikeClicked;
        }

        protected void DislikePostId(PostGetByIdDto post)
        {
            if (post.DislikeClicked)
            {
                post.Downvotes--;
            }
            else
            {
                post.Downvotes++;
                if (post.LikeClicked)
                {
                    post.Upvotes--;
                    post.LikeClicked = !post.LikeClicked;
                }
            }
            post.DislikeClicked = !post.DislikeClicked;
        }

    }
}
