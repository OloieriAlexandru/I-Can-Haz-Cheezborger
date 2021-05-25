using System;

namespace Models.Comments
{
    public class CommentPatchReactDto : UserInfoModel
    {
        public Guid Id { get; set; }

        public string Type { get; set; }

        public Guid CreatorId { get; set; }

        public string CreatorUsername { get; set; }

        public int Upvotes { get; set; }

        public int Downvotes { get; set; }

        
    }
}
