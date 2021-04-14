using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrendsViewer.Models
{
    public class UpdatePostModel
    {
        [Required]
        public string Title { get; set; }
        public string MediaPath { get; set; }
    }
}
