using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentManagementSystem.Domain.Entities;

namespace StudentManagementSystem.Infrastructure.Data.Configurations
{
    public class FacultyConfiguration : IEntityTypeConfiguration<Faculty>
    {
        public void Configure(EntityTypeBuilder<Faculty> builder)
        {
            builder.ToTable("Faculties");

            builder.HasKey(f => f.FacultyId);

            builder.Property(f => f.FacultyName)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(f => f.Students)
                .WithOne(s => s.Faculty)
                .HasForeignKey(s => s.FacultyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
