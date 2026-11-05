using Entity03.Enums;
using Entity03.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entity03.Configurations
{
    public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.HasKey(e => new { e.StudentId, e.CourseId });

            builder.Property(e => e.EnrollmentDate)
                    .IsRequired();

            builder.Property(e => e.ProgressPercentage)
                   .IsRequired()
                   .HasPrecision(5, 2);

            builder.Property(e => e.Status)
                   .IsRequired();

            builder.Property(e => e.FinalGrade)
                   .IsRequired(false)
                   .HasPrecision(5, 2);

            builder.HasOne(e => e.Student)
                   .WithMany(s => s.Enrollments)
                   .HasForeignKey(e => e.StudentId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Course)
                   .WithMany(c => c.Enrollments)
                   .HasForeignKey(e => e.CourseId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasData(
    new Enrollment
    {
        StudentId = 1,
        CourseId = 1,
        EnrollmentDate = new DateTime(2024, 5, 5),
        ProgressPercentage = 50,
        Status = EnrollmentStatus.Active,
        FinalGrade = null
    },
    new Enrollment
    {
        StudentId = 2,
        CourseId = 1,
        EnrollmentDate = new DateTime(2024, 5, 10),
        ProgressPercentage = 100,
        Status = EnrollmentStatus.Completed,
        FinalGrade = 95
    }
);

        }
    }
}