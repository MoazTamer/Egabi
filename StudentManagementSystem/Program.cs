namespace StudentManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Student> students = new List<Student>();

            // Add 3 students
            for (int i = 1; i <= 3; i++)
            {
                Console.WriteLine($"--------------- Enter Student {i} Data ---------------");
                students.Add(AddStudent());
            }
            Console.Clear();

            // Print Students Data
            Console.WriteLine("=========== All Students ===========");

            foreach (Student student in students)
            {
                student.PrintStudent();
                Console.WriteLine("\n-----------------------------------------------------\n");
            }


            // Print Excellent Students
            Console.WriteLine("\n=========== Excellent Students ===========");
            foreach (Student student in students)
            {
                if (student.IsExcellent())
                    Console.WriteLine($"{student.Name} - GPA: {student.GPA}");
            }

        }
        static Student AddStudent()
        {
            string studentName, studentFaculty;

            do
            {
                Console.Write("Enter student name: ");
                studentName = Console.ReadLine();
            }
            while (string.IsNullOrWhiteSpace(studentName));

            int studentAge;
            do
            {
                Console.Write("Enter age: ");
                studentAge = int.Parse(Console.ReadLine());
            }
            while (studentAge <= 0);

            do
            {
                Console.Write("Enter faculty: ");
                studentFaculty = Console.ReadLine();
            }
            while (string.IsNullOrWhiteSpace(studentFaculty));

            double studentGpa;
            do
            {
                Console.Write("Enter GPA: ");
                studentGpa = double.Parse(Console.ReadLine());
            }
            while (studentGpa < 0 || studentGpa > 4);

            Console.WriteLine("Student Added Successfully.\n");
            return new Student(studentName, studentAge, studentFaculty, studentGpa);
        }

    }
}