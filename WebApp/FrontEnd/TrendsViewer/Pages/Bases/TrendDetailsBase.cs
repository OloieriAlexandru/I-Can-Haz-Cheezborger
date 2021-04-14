using AutoMapper;
using Microsoft.AspNetCore.Components;
using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrendsViewer.Models;
using TrendsViewer.Services;

namespace TrendsViewer.Pages
{
    public class TrendDetailsBase : ComponentBase
    {
        [Inject]
        public ITrendService TrendService { get; set; }
        [Inject]
        public IPostService PostService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string Id { get; set; }

        public TrendGetByIdDto Trend { get; set; }
        [Inject]
        public IMapper Mapper { get; set; }
        public IEnumerable<PostGetAllDto> Posts { get; set; }
        public CreatePostModel createPostModel = new CreatePostModel();

        protected async override Task OnInitializedAsync()
        {
            Trend = await TrendService.GetTrend(Guid.Parse(Id));
            Posts = await PostService.GetPosts(Guid.Parse(Id));
        }

        protected async void HandleValidSubmit()
        {
            PostCreateDto newPost = new PostCreateDto { Title = createPostModel.Title, TrendId= Guid.Parse(Id), MediaPath=createPostModel.MediaPath};
            Mapper.Map(createPostModel, newPost);

            await PostService.CreatePost(Guid.Parse(Id), newPost);
            NavigationManager.NavigateTo($"/trends/{Id}", forceLoad:true);
        }
    }
}