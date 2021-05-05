using System;

namespace Models.Comments
{
    public class CommentPatchReactDto : UserInfoModel
    {
        public Guid Id { get; set; }

        public string Type { get; set; }
    }
}
