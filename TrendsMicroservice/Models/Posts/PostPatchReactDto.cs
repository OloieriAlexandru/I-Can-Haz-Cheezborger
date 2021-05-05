using System;

namespace Models.Posts
{
    public class PostPatchReactDto : UserInfoModel
    {
        public Guid Id { get; set; }

        public string Type { get; set; }
    }
}
