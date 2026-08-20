using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentManagementSystem.Domain.Entities;

namespace StudentManagementSystem.Infrastructure.Data.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");

            builder.HasKey(s => s.StudentId);

            builder.Property(s => s.StudentName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.Age)
                .IsRequired();

            builder.Property(s => s.GPA)
                .IsRequired()
                .HasPrecision(3, 2);

            builder.ToTable(t => t.HasCheckConstraint("CK_Student_Age", "[Age] > 0"));
            builder.ToTable(t => t.HasCheckConstraint("CK_Student_GPA", "[GPA] >= 0 AND [GPA] <= 4"));

            builder.HasOne(s => s.Faculty)
                .WithMany(f => f.Students)
                .HasForeignKey(s => s.FacultyId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(s => s.StudentName);
            builder.HasIndex(s => s.FacultyId);
        }
    }
}
