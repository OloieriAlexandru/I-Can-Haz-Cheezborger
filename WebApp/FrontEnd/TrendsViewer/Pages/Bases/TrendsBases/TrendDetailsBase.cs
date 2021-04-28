using AutoMapper;
using Microsoft.AspNetCore.Components;
using Models.Posts;
using Models.Trends;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrendsViewer.Models;
using TrendsViewer.Services.Abstractions;

namespace TrendsViewer.Pages
{
    public class TrendDetailsBase : ComponentBase
    {
        [Inject]
        public ITrendService TrendService { get; set; }
        [Inject]
        public IPostService PostService { get; set; }
        [Inject]
        public IAuthService AuthService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IMapper Mapper { get; set; }

        [Parameter]
        public string Id { get; set; }

        public CreatePostModel CreatePostModel { get; set; }

        public TrendGetByIdDto Trend { get; set; }
        public IEnumerable<TrendGetAllDto> Trends { get; set; }
        public ICollection<PostGetAllDto> Posts { get; set; }

        public TrendDetailsBase()
        {
            CreatePostModel = new CreatePostModel();
        }

        protected async override Task OnInitializedAsync()
        {
            Trend = await TrendService.GetTrend(Guid.Parse(Id));
            Trends = await TrendService.GetTrends();
            Posts = new List<PostGetAllDto>(await PostService.GetPosts(Guid.Parse(Id)));
        }

        protected async Task HandleValidSubmit()
        {
            PostCreateDto newPost = new PostCreateDto { Title = CreatePostModel.Title, TrendId= Id, MediaPath=CreatePostModel.MediaPath};
            Mapper.Map(CreatePostModel, newPost);

            await PostService.CreatePost(Guid.Parse(Id), newPost);
            NavigationManager.NavigateTo($"/trends/{Id}", forceLoad:true);
        }

        protected async Task DeleteClick(Guid postId)
        {
            await PostService.DeletePost(Trend.Id, postId);
            
            foreach (PostGetAllDto post in Posts)
            {
                if (post.Id == postId)
                {
                    Posts.Remove(post);
                    break;
                }
            }
        }
        protected void NavigateTrendId(Guid trendId)
        {
            NavigationManager.NavigateTo($"/trends/{trendId}", forceLoad: true);
        }

        protected void NavigateToPostId(string trendId, string postId)
        {
            NavigationManager.NavigateTo($"/trends/{trendId}/posts/{postId}", forceLoad: true);
        }
        protected void NavigateNewTrend()
        {
            NavigationManager.NavigateTo($"/trends/create", forceLoad: true);
        }
        protected void LikePostId(PostGetAllDto post)
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

        protected void DislikePostId(PostGetAllDto post)
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