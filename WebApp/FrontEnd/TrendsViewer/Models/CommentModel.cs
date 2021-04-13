using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrendsViewer.Models
{
    public class CommentModel
    {
        [Required]
        [StringLength(500, ErrorMessage = "Your comment is too long")]
        public string CommentText { get; set; }
    }
}
