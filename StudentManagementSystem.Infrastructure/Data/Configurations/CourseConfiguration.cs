using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentManagementSystem.Domain.Entities;

namespace StudentManagementSystem.Infrastructure.Data.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Courses");

            builder.HasKey(c => c.CourseId);

            builder.Property(c => c.CourseName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(c => c.Credits)
                .IsRequired();

            builder.ToTable(t => t.HasCheckConstraint("CK_Course_Credits", "[Credits] > 0"));
        }
    }
}
