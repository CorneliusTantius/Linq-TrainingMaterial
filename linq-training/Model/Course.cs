using System;
using System.Collections.Generic;
using System.Text;

namespace linq_training.Model
{
    public class Course
    {
        public string StudentId { get; set; }
        public List<string> Courses { get; set; }
        public string CourseSetId { get; set; }
    }
}
