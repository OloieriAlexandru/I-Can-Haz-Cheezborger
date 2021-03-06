using System;

namespace Models.Comments
{
    public class CommentGetDto : UserInfoModel
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public int Upvotes { get; set; }

        public int Downvotes { get; set; }

        public bool Liked { get; set; }

        public bool Disliked { get; set; }

        public bool ApprovedImage { get; set; }

        public bool ApprovedText { get; set; }

        public DateTime CreateDate { get; set; }

        public Guid PostId { get; set; }
    }
}