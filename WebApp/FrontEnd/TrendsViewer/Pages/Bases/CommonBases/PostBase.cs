using Microsoft.AspNetCore.Components;
using Models.Posts;
using System.Threading.Tasks;
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

        protected async Task LikePost(PostGetAllDto post)
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
            await UpdatePostReact(post);
        }

        protected async Task DislikePost(PostGetAllDto post)
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
            await UpdatePostReact(post);
        }

        private async Task UpdatePostReact(PostGetAllDto post)
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
            await PostService.PatchPostReact(post.TrendId, postPatchReactDto);
            StateHasChanged();
        }
    }
}
