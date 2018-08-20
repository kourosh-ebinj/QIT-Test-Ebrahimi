using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestDAL.Core.Domains
{
    public class Student: Base
    {

        public string Name { get; set; }
        public int Age { get; set; }
        public float GPA { get; set; }
        public int ClassId { get; set; }
        public Class Class { get; set; }

    }
}