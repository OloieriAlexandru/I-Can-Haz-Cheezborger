using AutoMapper;
using Microsoft.AspNetCore.Components;
using Models.Posts;
using System;
using System.Threading.Tasks;
using TrendsViewer.Models;
using TrendsViewer.Services.Abstractions;

namespace TrendsViewer.Pages
{
    public class PostEditBase : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IMapper Mapper { get; set; }
        [Inject]
        public IPostService PostService { get; set; }

        [Parameter]
        public string TrendId { get; set; }
        [Parameter]
        public string PostId { get; set; }

        public EditPostModel EditPostModel { get; set; }

        private PostPatchDto Post { get; set; }

        public PostEditBase()
        {
            EditPostModel = new EditPostModel();
            Post = new PostPatchDto();
        }

        protected async override Task OnInitializedAsync()
        {
            if (!Guid.TryParse(TrendId, out Guid trendId))
            {
                throw new InvalidProgramException("Invalid Id!");
            }
            PostGetByIdDto updatedPost = await PostService.GetPost(Guid.Parse(TrendId), Guid.Parse(PostId));
            Post = Mapper.Map<PostPatchDto>(updatedPost);
            Mapper.Map(Post, EditPostModel);
        }

        protected async Task HandleValidSubmit()
        {
            Mapper.Map(EditPostModel, Post);
            await PostService.PatchPost(Guid.Parse(TrendId), Guid.Parse(PostId), Post);
            NavigationManager.NavigateTo($"/trends/{TrendId}/");
        }

        protected async Task DeleteClick()
        {
            await PostService.DeletePost(Guid.Parse(TrendId), Guid.Parse(PostId));
            NavigationManager.NavigateTo("/trends/{TrendId}");
        }
    }
}
