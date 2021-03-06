using Microsoft.AspNetCore.Components;
using Models.Comments;
using System;
using System.Threading.Tasks;
using TrendsViewer.FormModels;
using TrendsViewer.Services.Abstractions;

namespace TrendsViewer.Pages
{
    public class CommentCardBase : ComponentBase
    {
        [Parameter]
        public Guid TrendId { get; set; }

        [Parameter]
        public Guid PostId { get; set; }

        [Parameter]
        public CommentGetDto Comment { get; set; }

        [Parameter]
        public bool Authenticated { get; set; }

        [Parameter]
        public bool Owner { get; set; }

        [Parameter]
        public ICommentService CommentService { get; set; }

        [Parameter]
        public EventCallback<CommentGetDto> OnCommentDeleted { get; set; }

        [Parameter]
        public EventCallback<CommentGetDto> OnStateChanged { get; set; }

        [Parameter]
        public EventCallback<CommentGetDto> OnEditComment { get; set; }

        protected CommentModel CommentModel { get; set; }

        protected bool InEditMode { get; set; }

        public CommentCardBase()
        {
            CommentModel = new CommentModel();
        }

        protected async Task LikeComment(CommentGetDto comment)
        {
            if (comment.Liked)
            {
                comment.Upvotes--;
            }
            else
            {
                comment.Upvotes++;
                if (comment.Disliked)
                {
                    comment.Downvotes--;
                    comment.Disliked = !comment.Disliked;
                }
            }
            comment.Liked = !comment.Liked;
            await UpdateCommentReact(comment);
        }

        protected async Task DislikeComment(CommentGetDto comment)
        {
            if (comment.Disliked)
            {
                comment.Downvotes--;
            }
            else
            {
                comment.Downvotes++;
                if (comment.Liked)
                {
                    comment.Upvotes--;
                    comment.Liked = !comment.Liked;
                }
            }
            comment.Disliked = !comment.Disliked;
            await UpdateCommentReact(comment);
        }

        protected async Task DeleteComment(CommentGetDto deletedComment)
        {
            await CommentService.DeleteComment(TrendId, PostId, deletedComment.Id);
            await OnCommentDeleted.InvokeAsync(deletedComment);
        }

        protected async Task EditComment()
        {
            CommentPatchDto commentPatch = new CommentPatchDto()
            {
                Id = Comment.Id,
                Text = CommentModel.CommentText
            };

            await CommentService.UpdateComment(TrendId, PostId, commentPatch.Id, commentPatch);
            InEditMode = false;
            Comment.Text = CommentModel.CommentText;
            CommentModel.CommentText = string.Empty;
            await OnStateChanged.InvokeAsync(Comment);
        }

        protected async Task EnterEditCommentMode(CommentGetDto comment)
        {
            if (InEditMode)
            {
                CommentModel.CommentText = string.Empty;
            }
            InEditMode = !InEditMode;
            await OnEditComment.InvokeAsync(comment);
        }

        private async Task UpdateCommentReact(CommentGetDto comment)
        {
            CommentPatchReactDto commentPatchReactDto = new CommentPatchReactDto
            {
                Id = comment.Id
            };
            if (comment.Liked)
            {
                commentPatchReactDto.Type = "Like";
            }
            else if (comment.Disliked)
            {
                commentPatchReactDto.Type = "Dislike";
            }
            else
            {
                commentPatchReactDto.Type = "None";
            }
            await CommentService.PatchCommentReact(TrendId, PostId, commentPatchReactDto);
            await OnStateChanged.InvokeAsync(comment);
        }
    }
}
