namespace StudentManagementSystem
{
    public class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Faculty { get; set; }
        public double GPA { get; set; }
        public Student(string name, int age, string faculty, double gpa)
        {
            Name = name;
            Age = age;
            Faculty = faculty;
            GPA = gpa;
        }
        public void PrintStudent()
        {
            Console.WriteLine($"Name    : {Name}");
            Console.WriteLine($"Age     : {Age}");
            Console.WriteLine($"Faculty : {Faculty}");
            Console.WriteLine($"GPA     : {GPA}");
        
            if (IsExcellent())
                Console.WriteLine("Excellent Student");
        }
        public bool IsExcellent()
        {
            return GPA >= 3.0;
        }
    }
}
