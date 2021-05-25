using AutoMapper;
using Microsoft.AspNetCore.Components;
using Models.Posts;
using System;
using System.Threading.Tasks;
using TrendsViewer.FormModels;
using TrendsViewer.Services.Abstractions;

namespace TrendsViewer.Pages
{
    public class PostCreateBase : ComponentBase
    {
        [Inject]
        public IPostService PostService { get; set; }
        [Inject]
        public IMapper Mapper { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string TrendId { get; set; }

        public string MediaPath { get; set; }

        public CreatePostModel CreatePostModel { get; set; }

        private PostCreateDto Post { get; set; }

        public PostCreateBase()
        {
            CreatePostModel = new CreatePostModel();
            Post = new PostCreateDto();
        }

        protected async override Task OnInitializedAsync()
        {
            await Task.CompletedTask;
            CreatePostModel.TrendId = TrendId;
        }

        protected void ImageSelected(string mediaPath)
        {
            MediaPath = mediaPath;
        }

        protected async Task HandleValidSubmit()
        {
            Mapper.Map(CreatePostModel, Post);
            String date = DateTime.Now.ToString("dd-M-yyyy HH:mm");
            Post.DateTime = date;

            await PostService.CreatePost(Guid.Parse(TrendId), Post);
            NavigationManager.NavigateTo($"/trends/{TrendId}");
        }
    }
}
