using Microsoft.AspNetCore.Components;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrendsViewer.Services;

namespace TrendsViewer.Pages
{
    public class TrendDetailsBase : ComponentBase
    {

        public TrendDto Trend { get; set; } = new TrendDto();
        public IEnumerable<PostDto> Posts { get; set; }
        [Inject]
        public ITrendService TrendService { get; set; }
        //[Inject]
        //public IPostService PostService { get; set; }

        [Parameter]
        public string Id { get; set; }

        protected async override Task OnInitializedAsync()
        {
            Trend = await TrendService.GetTrend(Guid.Parse(Id));
            //           Posts = (await PostService.GetPosts(Guid.Parse(Id))).ToList();
            PostDto postDto1 = new PostDto{Title = "post1", Id = Guid.NewGuid()};
            PostDto postDto2 = new PostDto { Title = "post2", Id = Guid.NewGuid() };
            PostDto postDto3 = new PostDto { Title = "post3", Id = Guid.NewGuid() };

            List<PostDto> list = new List<PostDto>();
            list.Add(postDto1);
            list.Add(postDto2);
            list.Add(postDto3);
            
            this.Posts = list;


        }
    }
}