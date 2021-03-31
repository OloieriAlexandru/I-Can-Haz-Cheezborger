using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
