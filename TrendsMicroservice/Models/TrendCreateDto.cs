using System;

namespace Models
{
    public class TrendCreateDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public override bool Equals(object obj)
        {
            return obj is TrendGetAllDto dto &&
                   Name == dto.Name &&
                   Description == dto.Description &&
                   ImageUrl == dto.ImageUrl;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Description, ImageUrl);
        }
    }
}
