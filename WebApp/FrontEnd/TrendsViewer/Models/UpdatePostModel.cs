using System.ComponentModel.DataAnnotations;

namespace TrendsViewer.Models
{
    public class UpdatePostModel
    {
        [Required]
        public string Title { get; set; }

        public string MediaPath { get; set; }
    }
}
