using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Dto
{
    public class StudentDto : BaseDto
    {

        public string Name { get; set; }
        public int Age { get; set; }
        public float GPA { get; set; }
        public int ClassId { get; set; }

    }
}
