using System;

namespace Models.Trends
{
    public class TrendPatchFollowDto : UserInfoModel
    {
        public Guid Id { get; set; }

        public string Type { get; set; }
    }
}
