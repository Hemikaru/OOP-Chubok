using System;
using System.Collections.Generic;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Student> students = new List<Student>
            {
                new Bachelor("Іван", 85),
                new Bachelor("Оля", 92),
                new Master("Петро", 95),
                new Master("Марія", 70)
            };

            Console.WriteLine("Список студентів:");
            foreach (var st in students)
                Console.WriteLine(st.GetInfo());

            double avg = Student.GetAverageMark(students);
            Console.WriteLine($"\nСередній бал групи: {avg}");

            double percent90 = Student.GetPercentAbove90(students);
            Console.WriteLine($"Відсоток студентів з балом > 90: {percent90}%");
        }
    }
}
