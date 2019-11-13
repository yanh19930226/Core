using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptionSample
{
    public class Class1
    {
        public string ClassNo { get; set; }
        public string ClassName { get; set; }

        public string ClassDes { get; set; }

        public List<Student> Students { get; set; }
    }
    public class Student
    {
        public string Name { get; set; }
        public string Age { get; set; }

    }
}
