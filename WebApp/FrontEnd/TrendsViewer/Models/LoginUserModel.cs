using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
