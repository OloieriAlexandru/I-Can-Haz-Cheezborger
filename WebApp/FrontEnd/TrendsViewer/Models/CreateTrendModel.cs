using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrendsViewer.Models
{
    public class CreateTrendModel
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; }
        [Required]
        [MinLength(2)]
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
