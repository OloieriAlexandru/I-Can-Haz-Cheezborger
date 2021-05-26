using Microsoft.AspNetCore.Components;
using Models.Posts;
using TrendsViewer.Services.Abstractions;

namespace TrendsViewer.Pages.Bases.CommonBases
{
    public abstract class PostBase : ComponentBase
    {
        [Inject]
        public IPostService PostService { get; set; }
        [Inject]
        public IAuthService AuthService { get; set; }
        [Inject]
        public IImageService ImageService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

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
            PostService.PatchPostReact(post.TrendId, postPatchReactDto);
            StateHasChanged();
        }
    }
}
