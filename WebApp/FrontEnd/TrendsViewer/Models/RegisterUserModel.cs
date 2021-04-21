using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrendsViewer.Models
{
    public class RegisterUserModel
    {
        [Required]
        [MinLength(6)]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [CompareProperty("Password")]
        public string PasswordCheck { get; set; }

    }
}
