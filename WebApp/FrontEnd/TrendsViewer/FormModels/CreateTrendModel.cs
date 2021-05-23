using System.ComponentModel.DataAnnotations;

namespace TrendsViewer.FormModels
{
    public class CreateTrendModel
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        [MinLength(10)]
        public string Description { get; set; }        
    }
}
