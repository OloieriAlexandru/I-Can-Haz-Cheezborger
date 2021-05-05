using Models.Posts;
using System;
using System.Collections.Generic;

namespace Models.Trends
{
    public class TrendGetByIdDto : UserInfoModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<PostGetAllDto> Posts { get; set; }
    }
}
