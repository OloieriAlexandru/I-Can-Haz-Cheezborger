using System;
using System.ComponentModel.DataAnnotations;

namespace TrendsViewer.Models
{
    public class CreatePostModel
    {
        [Required]
        [MinLength(2)]
        public string Title { get; set; }

        [Required]
        public string MediaPath { get; set; }

        public string Description { get; set; }


        public string TrendId { get; set; }
    }
}
