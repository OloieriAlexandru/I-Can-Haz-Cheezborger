using System.ComponentModel.DataAnnotations;

namespace TrendsViewer.FormModels
{
    public class UpdatePostModel
    {
        [Required]
        public string Title { get; set; }

        public string MediaPath { get; set; }
    }
}
