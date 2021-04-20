using System.ComponentModel.DataAnnotations;

namespace TrendsViewer.Models
{
    public class CreatePostModel
    {
        [Required]
        public string Title { get; set; }

        public string MediaPath { get; set; }

        public string Description { get; set; }
    }
}
