using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab3
{
    class Student
    {
        public string Name { get; set; }
        public double Mark { get; set; }

        public Student(string name, double mark)
        {
            Name = name;
            Mark = mark;
        }

        public virtual string GetInfo()
        {
            return $"{Name} (студент), бал: {Mark}";
        }

        public static double GetAverageMark(List<Student> students)
        {
            return students.Average(s => s.Mark);
        }

        public static double GetPercentAbove90(List<Student> students)
        {
            int count = students.Count(s => s.Mark > 90);
            return (double)count / students.Count * 100;
        }
    }
}
