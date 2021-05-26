using AutoMapper;
using Microsoft.AspNetCore.Components;
using Models.Posts;
using Models.Trends;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrendsViewer.Pages.Bases.CommonBases;
using TrendsViewer.Services.Abstractions;

namespace TrendsViewer.Pages
{
    public class TrendDetailsBase : PostListBase
    {
        [Inject]
        public ITrendService TrendService { get; set; }
        [Inject]
        public IMapper Mapper { get; set; }

        [Parameter]
        public string TrendId { get; set; }

        public TrendGetByIdDto Trend { get; set; }

        public ICollection<PostGetAllDto> Posts { get; set; }
        public ICollection<PostGetAllDto> PostsList { get; set; }

        public ArrayList SeeContent { get; set; } = new ArrayList();

        public const int PAGESIZE = 10;
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                Trend = await TrendService.GetById(Guid.Parse(TrendId));
                Posts = new List<PostGetAllDto>(await PostService.GetPosts(Guid.Parse(TrendId)));

                PostsList = Posts.Take(PAGESIZE).ToList();
                TotalPages = (int)Math.Ceiling(Posts.Count / (decimal)PAGESIZE);

                //Aici va veni logica se setare a unui post daca vrei sa se vada sau nu
                for (var i=0; i<Posts.Count; i++)
                {
                    if (i == 0)
                    {
                        SeeContent.Add(false);
                    }
                    else
                    {
                        SeeContent.Add(true);
                    }
                }

                StateHasChanged();
            }
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

        protected void ViewContent(PostGetAllDto postViewed)
        {
            foreach(var item in Posts.Select((value, i) => new { i, value }))
            {
                if(item.value.Id == postViewed.Id)
                {
                    SeeContent[item.i] = true;
                }
            } 
            StateHasChanged();
        }

        public void UpdateList(int pageNumber)
        {
            if (pageNumber >= 0 && pageNumber < TotalPages)
            {
                return;
            }
            CurrentPage = pageNumber;
            PostsList = Posts.Skip(pageNumber * PAGESIZE).Take(PAGESIZE).ToList();
        }

        public void NavigateTo(string direction)
        {
            if (direction == "prev" && CurrentPage > 0)
            {
                --CurrentPage;
            }
            else if (direction == "next" && CurrentPage < TotalPages - 1)
            {
                ++CurrentPage;
            }
            else if (direction == "first")
            {
                CurrentPage = 0;
            }
            else if (direction == "last")
            {
                CurrentPage = TotalPages - 1;
            }

            UpdateList(CurrentPage);
        }
    }
}
