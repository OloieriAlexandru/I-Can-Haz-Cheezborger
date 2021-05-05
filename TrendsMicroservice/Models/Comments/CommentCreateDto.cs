using System;

namespace Models.Comments
{
    public class CommentCreateDto : UserInfoModel
    {
        public string Text { get; set; }

        public Guid PostId { get; set; }
    }
}
