using AutoMapper;
using Microsoft.AspNetCore.Components;
using Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public IEnumerable<CommentGetDto> Comments { get; set; }

        public CommentModel commentModel = new CommentModel();
        public CommentCreateDto createdComment;

        public PostDetailsBase()
        {
            createdComment = new CommentCreateDto();
        }

        protected async override Task OnInitializedAsync()
        {
            Post = await PostService.GetPost(Guid.Parse(TrendId), Guid.Parse(PostId));
            Comments = await CommentService.GetComments(Guid.Parse(TrendId), Guid.Parse(PostId));
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
            NavigationManager.NavigateTo($"/trends/{TrendId}/posts/{PostId}");
        }
    }
}
