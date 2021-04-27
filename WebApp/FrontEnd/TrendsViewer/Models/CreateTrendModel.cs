using System.ComponentModel.DataAnnotations;

namespace TrendsViewer.Models
{
    public class CreateTrendModel
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        [Required]
        [MinLength(2)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }
        
    }
}
