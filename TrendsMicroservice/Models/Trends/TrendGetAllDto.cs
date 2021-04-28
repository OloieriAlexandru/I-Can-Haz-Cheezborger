using System;

namespace Models.Trends
{
    public class TrendGetAllDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int Followers { get; set; } = 0;

        public int NumberPosts { get; set; } = 0;

        public string Username { get; set; }
        public Boolean FollowClicked { get; set; } = false;

        public override bool Equals(object obj)
        {
            return obj is TrendGetAllDto dto &&
                   Id == dto.Id &&
                   Name == dto.Name &&
                   Description == dto.Description &&
                   ImageUrl == dto.ImageUrl;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Description, ImageUrl);
        }
    }
}
