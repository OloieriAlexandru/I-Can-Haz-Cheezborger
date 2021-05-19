using Microsoft.AspNetCore.Components;
using Models.Posts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrendsViewer.Services.Abstractions;

namespace TrendsViewer.Pages
{
    public class IndexBase : ComponentBase
    {
        [Inject]
        public IAuthService AuthService { get; set; }
        [Inject]
        public IPostService PostService { get; set; }

        public ICollection<PostGetAllDto> Posts { get; set; }

        public Guid TrendId { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                TrendId = Guid.Parse("44610d77-1627-47ec-a6d7-03bdd3d83c32");
                Posts = await PostService.GetPosts(TrendId);
                StateHasChanged();
                await AuthService.Initialize();
            }
        }
    }
}
