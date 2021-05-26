using Models.Comments;
using System;
using System.Collections.Generic;

namespace Models.Posts
{
    public class PostGetByIdDto : UserInfoModel
    {
        public Guid Id { get; set; }
        
        public string Title { get; set; }

        public string MediaPath { get; set; }

        public int Upvotes { get; set; }

        public int Downvotes { get; set; }

        public Guid TrendId { get; set; }

        public bool Liked { get; set; }

        public bool Disliked { get; set; }

        public int CommentsCount { get; set; }

        public bool ApprovedImage { get; set; }

        public bool ApprovedText { get; set; }

        public DateTime CreateDate { get; set; }

        public ICollection<CommentGetDto> Comments { get; set; }
    }
}
