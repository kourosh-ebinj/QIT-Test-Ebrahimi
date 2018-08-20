using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestDAL.Core.Domains
{
    public class Class: Base
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string TeacherName { get; set; }
        public List<Student> Students { get; set; }

    }
}