using System.ComponentModel.DataAnnotations;

namespace TrendsViewer.Models
{
    public class EditTrendModel
    {   
        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        [CompareProperty("Name", ErrorMessage ="Name and ConfirmName must match")]
        public string ConfirmName { get; set; }
    }
}
