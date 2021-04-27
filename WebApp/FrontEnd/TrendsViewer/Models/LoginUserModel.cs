using System.ComponentModel.DataAnnotations;

namespace TrendsViewer.Models
{
    public class LoginUserModel
    {
        [Required]
        [MinLength(6)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
