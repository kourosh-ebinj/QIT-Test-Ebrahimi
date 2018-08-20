using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Dto
{
    public class ClassDto : BaseDto
    {

        public string Name { get; set; }
        public string Location { get; set; }
        public string TeacherName { get; set; }
        public List<StudentDto> Students { get; set; }

    }
}
