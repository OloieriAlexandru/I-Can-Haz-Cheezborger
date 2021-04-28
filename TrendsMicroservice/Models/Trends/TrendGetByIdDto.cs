using Models.Posts;
using System;
using System.Collections.Generic;

namespace Models.Trends
{
    public class TrendGetByIdDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string Username { get; set; }

        public ICollection<PostGetAllDto> Posts { get; set; }

        public override bool Equals(object obj)
        {
            return obj is TrendGetByIdDto dto &&
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
