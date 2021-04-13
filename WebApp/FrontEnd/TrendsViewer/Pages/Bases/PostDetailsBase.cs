using AutoMapper;
using Microsoft.AspNetCore.Components;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrendsViewer.Models;
using TrendsViewer.Services;
using TrendsViewer.Services.Abstractions;
using TrendsViewer.Services.Implementations;

namespace TrendsViewer.Pages
{
    public class PostDetailsBase : ComponentBase
    {
        //[Inject]
        //public ICommentService CommentService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IMapper Mapper { get; set; }

        public IEnumerable<CommentDto> Comments { get; set; }

        [Parameter]
        public string trendId { get; set; }
        [Parameter]
        public string postId { get; set; }

        public PostDto Post { get; set; } = new PostDto();
        public CommentModel commentModel = new CommentModel();
        public CommentDto comment = new CommentDto();

        protected async override Task OnInitializedAsync()
        {
            this.Post = new PostDto { Title = "Postarea mea", Id = Guid.NewGuid() };

            CommentDto commentDto1 = new CommentDto { Text = "comment1", Id = Guid.NewGuid(), Upvotes=10, Downvotes=20 };
            CommentDto commentDto2 = new CommentDto { Text = "comment2", Id = Guid.NewGuid(), Upvotes = 5, Downvotes = 50 };
            CommentDto commentDto3 = new CommentDto { Text = "comment3", Id = Guid.NewGuid(), Upvotes = 128, Downvotes = 4 };

            List<CommentDto> list = new List<CommentDto>();
            list.Add(commentDto1);
            list.Add(commentDto2);
            list.Add(commentDto3);

            this.Comments = list;
        }

        protected async Task HandleValidSubmit()
        {
            this.comment.Text = commentModel.CommentText;
            //await CommentService.CreateComment(Guid.Parse(trendId), Guid.Parse(postId), comment);
            NavigationManager.NavigateTo("/trends");
        }

        protected async Task HandleDeleteComment(Guid? commentId)
        {
            //await CommentService.DeleteComment(Guid.Parse(trendId), Guid.Parse(postId), (Guid)commentId);
            //NavigationManager.NavigateTo($"/trends/{trendId}/posts/{postId}");
        }
    }
}
