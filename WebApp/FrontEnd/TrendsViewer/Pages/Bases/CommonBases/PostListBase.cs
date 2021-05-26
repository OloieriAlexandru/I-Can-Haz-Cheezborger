using Microsoft.AspNetCore.Components;
using Models.Posts;

namespace TrendsViewer.Pages.Bases.CommonBases
{
    public abstract class PostListBase : PostBase
    {
        protected void NavigateToPostPage(PostGetAllDto post)
        {
            NavigationManager.NavigateTo($"/trends/{post.TrendId}/posts/{post.Id}", forceLoad: true);
        }
    }
}
