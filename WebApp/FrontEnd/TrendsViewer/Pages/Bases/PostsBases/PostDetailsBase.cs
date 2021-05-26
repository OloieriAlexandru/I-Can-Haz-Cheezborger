using AutoMapper;
using Microsoft.AspNetCore.Components;
using Models.Comments;
using Models.Posts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrendsViewer.FormModels;
using TrendsViewer.Pages.Bases.CommonBases;
using TrendsViewer.Services.Abstractions;

namespace TrendsViewer.Pages
{
    public class PostDetailsBase : PostBase
    {
        [Inject]
        public ICommentService CommentService { get; set; }
        [Inject]
        public IMapper Mapper { get; set; }

        [Parameter]
        public string TrendId { get; set; }
        [Parameter]
        public string PostId { get; set; }

        public ICollection<CommentGetDto> Comments { get; set; }
        
        public CommentModel CommentModel { get; set; }

        public PostGetByIdDto Post { get; set; }

        public PostGetAllDto UsedPost { get; set; }

        public PostDetailsBase()
        {
            CommentModel = new CommentModel();
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                Post = await PostService.GetPost(Guid.Parse(TrendId), Guid.Parse(PostId));
                UsedPost = Mapper.Map<PostGetAllDto>(Post);

                Comments = new List<CommentGetDto>(await CommentService.GetComments(Guid.Parse(TrendId), Guid.Parse(PostId)));
                StateHasChanged();
            }
        }

        protected async Task HandleValidSubmit()
        {
            CommentCreateDto newComment = new CommentCreateDto
            {
                Text = CommentModel.CommentText,
                PostId = Guid.Parse(PostId)
            };
            CommentGetDto comment = await CommentService.CreateComment(Guid.Parse(TrendId), Guid.Parse(PostId), newComment);
            Comments.Add(comment);
            StateHasChanged();
        }

        protected void DeleteComment(CommentGetDto deletedComment)
        {
            foreach (CommentGetDto comment in Comments)
            {
                if (comment.Id == deletedComment.Id)
                {
                    Comments.Remove(comment);
                    break;
                }
            }
            StateHasChanged();
        }

        protected void ChangeState(CommentGetDto comment)
        {
            StateHasChanged();
        }

        protected bool IsOwner(CommentGetDto comment)
        {
            return AuthService.IsLoggedIn() && AuthService.GetUsername() == comment.CreatorUsername;
        }
    }
}
