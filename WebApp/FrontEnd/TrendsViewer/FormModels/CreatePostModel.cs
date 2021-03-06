using System.ComponentModel.DataAnnotations;

namespace TrendsViewer.FormModels
{
    public class CreatePostModel
    {
        [Required]
        [MinLength(2)]
        public string Title { get; set; }

        public string Description { get; set; }
    }
}
