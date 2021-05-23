using AutoMapper;
using Microsoft.AspNetCore.Components;
using Models.Comments;
using Models.Posts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrendsViewer.FormModels;
using TrendsViewer.Services.Abstractions;

namespace TrendsViewer.Pages
{
    public class PostDetailsBase : ComponentBase
    {
        [Inject]
        public IPostService PostService { get; set; }
        [Inject]
        public ICommentService CommentService { get; set; }
        [Inject]
        public IAuthService AuthService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IMapper Mapper { get; set; }

        [Parameter]
        public string TrendId { get; set; }
        [Parameter]
        public string PostId { get; set; }

        public PostGetByIdDto Post { get; set; }
        public ICollection<CommentGetDto> Comments { get; set; }

        public CommentModel CommentModel { get; set; }
        public EditCommentModel EditCommentModel { get; set; }
        private CommentPatchDto Comment { get; set; }

        public Guid CommentToEdit { get; set; }

        public PostDetailsBase()
        {
            CommentModel = new CommentModel();
            CommentToEdit = Guid.Empty;
            EditCommentModel = new EditCommentModel();
            Comment = new CommentPatchDto();
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                Post = await PostService.GetPost(Guid.Parse(TrendId), Guid.Parse(PostId));
                Comments = new List<CommentGetDto>(await CommentService.GetComments(Guid.Parse(TrendId), Guid.Parse(PostId)));
                StateHasChanged();
            }
        }

        protected async Task HandleValidSubmit()
        {
            CommentCreateDto newComment = new CommentCreateDto { Text = CommentModel.CommentText, PostId = Guid.Parse(PostId)};
            await CommentService.CreateComment(Guid.Parse(TrendId), Guid.Parse(PostId), newComment);
            NavigationManager.NavigateTo($"/trends/{TrendId}/posts/{PostId}", forceLoad: true);
        }

        protected void LikePost(PostGetByIdDto post)
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
            UpdatePostReact(post);
        }

        protected void DislikePost(PostGetByIdDto post)
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

        protected void EditComment(CommentGetDto comment)
        {
            EditCommentModel.Text = comment.Text;
            CommentToEdit = comment.Id;
        }

        private void UpdatePostReact(PostGetByIdDto post)
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

        protected async Task DeleteComment(CommentGetDto comment2)
        {
            await CommentService.DeleteComment(Guid.Parse(TrendId), Guid.Parse(PostId), comment2.Id);

            foreach (CommentGetDto comment in Comments)
            {
                if (comment.Id == comment2.Id)
                {
                    Comments.Remove(comment);
                    break;
                }
            }
            NavigationManager.NavigateTo($"/trends/{TrendId}/posts/{PostId}");
        }

        protected async Task HandleValidSubmitEdit()
        {
            Mapper.Map(EditCommentModel, Comment);
            Comment.Id = CommentToEdit;

            await CommentService.UpdateComment(Guid.Parse(TrendId), Guid.Parse(PostId), Comment.Id, Comment);
            NavigationManager.NavigateTo($"/trends/{TrendId}/posts/{PostId}", forceLoad: true);
        }
    }
}
