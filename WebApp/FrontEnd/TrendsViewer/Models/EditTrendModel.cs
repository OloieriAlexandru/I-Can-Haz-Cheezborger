using System.ComponentModel.DataAnnotations;

namespace TrendsViewer.Models
{
    public class EditTrendModel
    {
        [MinLength(2)]
        public string Name { get; set; }

        [MinLength(2)]
        public string Description { get; set; }
        
        public string ImageUrl { get; set; }
    }
}
