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

        public CreatePostModel CreatePostModel { get; set; }

        public string MediaPath { get; set; } = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/46/Question_mark_%28black%29.svg/1200px-Question_mark_%28black%29.svg.png";

        public PostGetAllDto Post { get; set; }

        public PostCreateBase()
        {
            CreatePostModel = new CreatePostModel();
            Post = new PostGetAllDto();
        }

        protected void ImageSelected(string mediaPath)
        {
            MediaPath = mediaPath;
            StateHasChanged();
        }

        protected async Task HandleValidSubmit()
        {
            PostCreateDto post = new PostCreateDto();
            Mapper.Map(CreatePostModel, post);
            post.MediaPath = MediaPath;
            post.TrendId = Guid.Parse(TrendId);

            await PostService.CreatePost(Guid.Parse(TrendId), post);
            NavigationManager.NavigateTo($"/trends/{TrendId}");
        }
    }
}
