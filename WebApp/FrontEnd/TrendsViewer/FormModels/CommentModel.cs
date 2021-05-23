using System.ComponentModel.DataAnnotations;

namespace TrendsViewer.FormModels
{
    public class CommentModel
    {
        [Required]
        [StringLength(500, ErrorMessage = "Your comment is too long")]
        public string CommentText { get; set; }
    }
}
