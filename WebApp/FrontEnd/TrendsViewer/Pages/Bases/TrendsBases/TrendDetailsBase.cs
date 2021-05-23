using AutoMapper;
using Microsoft.AspNetCore.Components;
using Models.Posts;
using Models.Trends;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrendsViewer.FormModels;
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
        public string TrendId { get; set; }

        public CreatePostModel CreatePostModel { get; set; }

        public TrendGetByIdDto Trend { get; set; }
        public IEnumerable<TrendGetAllDto> Trends { get; set; }
        public ICollection<PostGetAllDto> Posts { get; set; }

        public TrendDetailsBase()
        {
            CreatePostModel = new CreatePostModel();
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                Trend = await TrendService.GetById(Guid.Parse(TrendId));
                Trends = await TrendService.GetAll();
                Posts = new List<PostGetAllDto>(await PostService.GetPosts(Guid.Parse(TrendId)));
                StateHasChanged();
            }
        }

        protected async Task HandleValidSubmit()
        {
            PostCreateDto newPost = new PostCreateDto { Title = CreatePostModel.Title, MediaPath=CreatePostModel.MediaPath};
            Mapper.Map(CreatePostModel, newPost);
            newPost.TrendId = Guid.Parse(TrendId);

            await PostService.CreatePost(Guid.Parse(TrendId), newPost);
            NavigationManager.NavigateTo($"/trends/{TrendId}", forceLoad:true);
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

        protected void LikePost(PostGetAllDto post)
        {
            if (post.Liked)
            {
                post.Upvotes--;
            }
            else
            {
                post.Upvotes++;
                if (post.Disliked)
                {
                    post.Downvotes--;
                    post.Disliked = !post.Disliked;
                }
            }
            post.Liked = !post.Liked;
            UpdatePostReact(post);
        }

        protected void DislikePost(PostGetAllDto post)
        {
            if (post.Disliked)
            {
                post.Downvotes--;
            }
            else
            {
                post.Downvotes++;
                if (post.Liked)
                {
                    post.Upvotes--;
                    post.Liked = !post.Liked;
                }
            }
            post.Disliked = !post.Disliked;
            UpdatePostReact(post);
        }

        private void UpdatePostReact(PostGetAllDto post)
        {
            PostPatchReactDto postPatchReactDto = new PostPatchReactDto
            {
                Id = post.Id
            };
            if (post.Liked)
            {
                postPatchReactDto.Type = "Like";
            }
            else if (post.Disliked)
            {
                postPatchReactDto.Type = "Dislike";
            }
            else
            {
                postPatchReactDto.Type = "None";
            }
            PostService.PatchPostReact(Guid.Parse(TrendId), postPatchReactDto);
            StateHasChanged();
        }
    }
}
