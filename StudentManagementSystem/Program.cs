#region Read Student Information
Console.Write("Enter student name: ");
string studentName = Console.ReadLine();

Console.Write("Enter age: ");
int studentAge = int.Parse(Console.ReadLine());

Console.Write("Enter faculty: ");
string studentFaculty = Console.ReadLine();

Console.Write("Enter GPA: ");
double studentGpa = double.Parse(Console.ReadLine());

Console.WriteLine("-----------------------------------------------------");
#endregion

#region Display Student Information
Console.WriteLine("Student All Information");
Console.WriteLine("-----------------------------------------------------");

Console.WriteLine($"Name    : {studentName}");
Console.WriteLine($"Age     : {studentAge}");
Console.WriteLine($"Faculty : {studentFaculty}");
Console.WriteLine($"GPA     : {studentGpa}");

if (studentGpa >= 3.0)
    Console.WriteLine("Excellent Student");
#endregion