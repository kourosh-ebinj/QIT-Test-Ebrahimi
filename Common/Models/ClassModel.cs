using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Models
{
    public class ClassModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(250)]
        public string Name { get; set; }

        [Display(Name = "Location")]
        [Required(ErrorMessage = "Location is required")]
        [MaxLength(1000)]
        public string Location { get; set; }
        
        [Display(Name = "Teacher Name")]
        [Required(ErrorMessage = "Teacher Name is required")]
        [MaxLength(250)]
        public string TeacherName { get; set; }
        
    }
}
