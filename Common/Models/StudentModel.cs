using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Models
{
    public class StudentModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(250)]
        public string Name { get; set; }

        [Display(Name = "Age")]
        [Required(ErrorMessage = "Age is required")]
        [Range(10,99)]
        public decimal Age { get; set; }

        [Display(Name = "GPA")]
        [Required(ErrorMessage = "GPA is required")]
        [Range(0, 10)]
        public decimal GPA { get; set; }

        public int ClassId { get; set; }

    }
}
