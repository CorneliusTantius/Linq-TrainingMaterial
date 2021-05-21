using linq_training.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_training
{
    class Program
    {
        // teken alt+enter kalau cacing merah
        private static List<Student> studentList = new List<Student>
        {
            new Student() { StudentId="001", StudentName="kornel", StudentAge=19, StudentGender="M" },
            new Student() { StudentId="002", StudentName="billy", StudentAge=18, StudentGender="F" },
            new Student() { StudentId="003", StudentName="jokris", StudentAge=17, StudentGender="M" },
            new Student() { StudentId="004", StudentName="adrian", StudentAge=20, StudentGender="M" },
            new Student() { StudentId="005", StudentName="angel", StudentAge=18, StudentGender="F" },
            new Student() { StudentId="006", StudentName="philip", StudentAge=20, StudentGender="M" },
            new Student() { StudentId="007", StudentName="japar", StudentAge=17, StudentGender="F" }
        };

        private static List<Course> courseList = new List<Course>
        {
            new Course() { StudentId="001", Courses=new List<string>{"COMP001", "COMP123"}, CourseSetId = "100"},
            new Course() { StudentId="003", Courses=new List<string>{"COMP021", "COMP123"}, CourseSetId = "200" },
            new Course() { StudentId="005", Courses=new List<string>{"COMP002", "COMP012"}, CourseSetId = "300" },
            new Course() { StudentId="007", Courses=new List<string>{"COMP003", "COMP001"}, CourseSetId = "400" },
            new Course() { StudentId="007", Courses=new List<string>{"COMP021", "COMP123"}, CourseSetId = "200" }
        };

        static void Main(string[] args)
        {
            linqGroupJoin();
        }

        // actual linq query

        // select StudentName, StudentAge from studentList
        static void linqSelect()
        {
            var data = studentList.Select( s => new { s.StudentName, s.StudentAge } ).ToList();
            foreach (var i in data)
            {
                Console.WriteLine($"> studentname: {i.StudentName}, age: {i.StudentAge}");
            }
        }
        
        // select * from studentList where StudentAge >= 18
        static void linqWhere()
        {
            var data = studentList.Where(
                p => p.StudentAge >= 18 && 
                    p.StudentAge < 20 && 
                    p.StudentGender == "F"
            ).ToList();
            foreach (var i in data)
            {
                Console.WriteLine(i.StudentName);
            }
        }

        static void linqFirstOrDefault()
        {
            Student data = studentList.FirstOrDefault();
            Console.WriteLine($"> {data.StudentName} {data.StudentId}");
        }

        static void linqOrderBy()
        {
            var data = studentList.OrderBy(p => p.StudentName).ToList();
            foreach (var i in data)
            {
                Console.Write(i.StudentName);
                Console.WriteLine(i.StudentAge);
            }
        }

        static void linqDistinct()
        {
            var data = studentList.Select(s => s.StudentGender).Distinct().ToList();
            foreach (string i in data)
            {
                Console.WriteLine(i);
            }
        }

        static void linqContains()
        {
            List<string> blacklistedStudent = new List<string> { "japar", "billy" };
            var data = studentList.
                Where(p => blacklistedStudent.Contains(p.StudentName)).
                Select(s => s.StudentId).ToList();
            foreach(string i in data)
            {
                Console.WriteLine(i);
            }
        }
    
        static void linqAllAny()
        {
            bool testAll = studentList.All(p => p.StudentGender == "M");
            bool testAny = studentList.Any(p => p.StudentGender == "M");
            Console.WriteLine(testAll);
            Console.WriteLine(testAny);
        }
        
        static void linqCountMax()
        {
            // Count() -> method
            // Count -> variabel
            int linqcount = studentList.Count();
            int propcount = studentList.Count;
            Console.WriteLine(linqcount);
            Console.WriteLine(propcount);

            var data = studentList.Select(s => s.StudentAge).ToList();
            Console.WriteLine(data.Max());
        }
        
        static void linqGroupBy()
        {
            var data = studentList.GroupBy(p => p.StudentGender).ToList();
            foreach(var i in data)
            {
                Console.WriteLine(i.Key);
                foreach(Student j in i)
                {
                    Console.WriteLine(j.StudentName);
                }
            }
        }
   
        static void linqSelectMany()
        {
            var data = courseList.SelectMany(s => s.Courses).ToList();
            foreach(string i in data)
            {
                Console.WriteLine(i);
            }
        }

        static void linqJoin()
        {
            var data = studentList.Join(courseList,
                keyStudentList => keyStudentList.StudentId,
                keyCourseList => keyCourseList.StudentId,
                (s1, s2) => new
                {
                    s1.StudentId, s1.StudentName,
                    s1.StudentGender, s2.Courses
                }
            ).ToList();
            foreach(var i in data)
            {
                Console.WriteLine($"> {i.StudentId}|{i.StudentName} {i.StudentGender} Courses:");
                foreach(string j in i.Courses)
                {
                    Console.WriteLine(j);
                }
            }
        }

        static void linqGroupJoin()
        {
            var data = studentList.GroupJoin(courseList,
                leftkey => leftkey.StudentId,
                rightkey => rightkey.StudentId,
                (s1, s2) => new
                {
                    s1.StudentId,
                    s1.StudentName,
                    s2
                }
            );
            foreach(var i in data)
            {
                Console.Write("> ");
                Console.WriteLine(i.StudentId);
                if(i.s2.Count() != 0)
                {
                    Console.WriteLine($"User has {i.s2.Count()}. those courses set are:");
                    foreach(Course j in i.s2)
                    {
                        Console.Write($"-");
                        foreach(string courescode in j.Courses)
                        {
                            Console.Write($" {courescode}");
                        }
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("No course for specified student!");
                }
            }
        }
    }
}
