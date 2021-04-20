using System.ComponentModel.DataAnnotations;
using System.Net.Http;

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
        
        public string ImageUrl { get; set; }
        
        public MultipartFormDataContent Image { get; set; }
    }
}
