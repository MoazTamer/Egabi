using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Domain.Entities;

namespace StudentManagementSystem.Infrastructure.Data
{
    public static class SeedData
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            // ---------------- Faculties ----------------
            modelBuilder.Entity<Faculty>().HasData(
                new Faculty { FacultyId = 1, FacultyName = "Computer Science" },
                new Faculty { FacultyId = 2, FacultyName = "Engineering" },
                new Faculty { FacultyId = 3, FacultyName = "Business" },
                new Faculty { FacultyId = 4, FacultyName = "Medicine" }
            );

            // ---------------- Students ----------------
            modelBuilder.Entity<Student>().HasData(
                new Student { StudentId = 1, StudentName = "Ahmed Ali", Age = 20, GPA = 3.50, FacultyId = 1 },
                new Student { StudentId = 2, StudentName = "Mohamed Hassan", Age = 21, GPA = 3.80, FacultyId = 1 },
                new Student { StudentId = 3, StudentName = "Sara Ahmed", Age = 19, GPA = 2.90, FacultyId = 1 },
                new Student { StudentId = 4, StudentName = "Omar Khaled", Age = 22, GPA = 3.20, FacultyId = 2 },
                new Student { StudentId = 5, StudentName = "Mariam Adel", Age = 21, GPA = 3.70, FacultyId = 2 },
                new Student { StudentId = 6, StudentName = "Youssef Samir", Age = 20, GPA = 2.60, FacultyId = 3 },
                new Student { StudentId = 7, StudentName = "Nour Mohamed", Age = 22, GPA = 3.40, FacultyId = 3 },
                new Student { StudentId = 8, StudentName = "Hana Mahmoud", Age = 23, GPA = 3.90, FacultyId = 4 }
            );

            // ---------------- Courses ----------------
            modelBuilder.Entity<Course>().HasData(
                new Course { CourseId = 1, CourseName = "Database Systems", Credits = 3 },
                new Course { CourseId = 2, CourseName = "Object Oriented Programming", Credits = 3 },
                new Course { CourseId = 3, CourseName = "Web Development", Credits = 3 },
                new Course { CourseId = 4, CourseName = "Computer Networks", Credits = 3 },
                new Course { CourseId = 5, CourseName = "Software Engineering", Credits = 4 }
            );

            // ---------------- Enrollments ----------------
            // Fixed date (not DateTime.UtcNow) so the generated migration is deterministic.
            var seedDate = new DateTime(2026, 1, 15, 0, 0, 0, DateTimeKind.Utc);

            modelBuilder.Entity<Enrollment>().HasData(
                new Enrollment { StudentId = 1, CourseId = 1, EnrollmentDate = seedDate }, // Ahmed Ali -> Database Systems
                new Enrollment { StudentId = 1, CourseId = 2, EnrollmentDate = seedDate }, // Ahmed Ali -> OOP
                new Enrollment { StudentId = 2, CourseId = 2, EnrollmentDate = seedDate }, // Mohamed Hassan -> OOP
                new Enrollment { StudentId = 2, CourseId = 3, EnrollmentDate = seedDate }, // Mohamed Hassan -> Web Dev
                new Enrollment { StudentId = 3, CourseId = 1, EnrollmentDate = seedDate }, // Sara Ahmed -> Database Systems
                new Enrollment { StudentId = 4, CourseId = 4, EnrollmentDate = seedDate }, // Omar Khaled -> Computer Networks
                new Enrollment { StudentId = 5, CourseId = 4, EnrollmentDate = seedDate }, // Mariam Adel -> Computer Networks
                new Enrollment { StudentId = 5, CourseId = 5, EnrollmentDate = seedDate }, // Mariam Adel -> Software Engineering
                new Enrollment { StudentId = 6, CourseId = 3, EnrollmentDate = seedDate }, // Youssef Samir -> Web Dev
                new Enrollment { StudentId = 7, CourseId = 5, EnrollmentDate = seedDate }, // Nour Mohamed -> Software Engineering
                new Enrollment { StudentId = 8, CourseId = 1, EnrollmentDate = seedDate }, // Hana Mahmoud -> Database Systems
                new Enrollment { StudentId = 8, CourseId = 5, EnrollmentDate = seedDate }  // Hana Mahmoud -> Software Engineering
            );
        }
    }
}
