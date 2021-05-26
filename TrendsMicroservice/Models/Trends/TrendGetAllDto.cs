using System;

namespace Models.Trends
{
    public class TrendGetAllDto : UserInfoModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int FollowersCount { get; set; }

        public int PostsCount { get; set; }

        public bool Followed { get; set; }

        public bool ApprovedImage { get; set; }

        public bool ApprovedText { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
