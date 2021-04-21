using System.ComponentModel.DataAnnotations;

namespace TrendsViewer.Models
{
    public class LoginUserModel
    {
        [Required]
        [MinLength(6)]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

    }
}
