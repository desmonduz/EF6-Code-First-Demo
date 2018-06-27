using System;

namespace EF6CodeFirstDemo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", Environment.SpecialFolder.ApplicationData);

            using (var ctx = new SchoolContext())
            {
                var student = new Student() { StudentName = "Bill" };

                ctx.Students.Add(student);
                ctx.SaveChanges();
            }
            Console.WriteLine("Demo completed.");
            Console.ReadLine();
        }
    }
}