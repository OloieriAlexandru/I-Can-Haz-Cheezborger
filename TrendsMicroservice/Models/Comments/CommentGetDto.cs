using System;

namespace Models.Comments
{
    public class CommentGetDto : UserInfoModel
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public int Upvotes { get; set; }

        public int Downvotes { get; set; }

        public string CreatedBy { get; set; }

        public Guid PostId { get; set; }
    }
}