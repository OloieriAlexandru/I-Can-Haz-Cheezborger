using Microsoft.AspNetCore.Components;
using Models.Posts;

namespace TrendsViewer.Pages
{
    public class PostCardBase : ComponentBase
    {
        [Parameter]
        public PostGetAllDto Post { get; set; }

        [Parameter]
        public string PostName { get; set; }

        [Parameter]
        public string PostDescription { get; set; }

        [Parameter]
        public bool InList { get; set; } = true;
    
        [Parameter]
        public string ImageUrl { get; set; }
    
        [Parameter]
        public bool ActivateButtons { get; set; }

        [Parameter]
        public bool Authenticated { get; set; }

        [Parameter]
        public EventCallback<PostGetAllDto> OnPostLiked { get; set; }
    
        [Parameter]
        public EventCallback<PostGetAllDto> OnPostDisliked { get; set; }
    
        [Parameter]
        public EventCallback<PostGetAllDto> OnNavigateToPostPage { get; set; }

        protected async void LikePost()
        {
            await OnPostLiked.InvokeAsync(Post);
        }

        protected async void DislikePost()
        {
            await OnPostDisliked.InvokeAsync(Post);
        }

        protected async void NavigateToPostPage()
        {
            await OnNavigateToPostPage.InvokeAsync(Post);
        }
    }
}
