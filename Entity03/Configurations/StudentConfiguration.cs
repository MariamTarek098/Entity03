using Entity03.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entity03.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.FullName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(s => s.Email)
       .IsRequired()
       .HasMaxLength(150);

            builder.HasIndex(s => s.Email)
                   .IsUnique();

            builder.Property(s => s.DateOfBirth)
                   .IsRequired();
            builder.HasData(
    new Student
    {
        Id = 1,
        FullName = "Mariam Ahmed",
        Email = "mariam@example.com",
        DateOfBirth = new DateTime(2002, 5, 10)
    },
    new Student
    {
        Id = 2,
        FullName = "Omar Mohamed",
        Email = "omar@example.com",
        DateOfBirth = new DateTime(2001, 8, 20)
    },
    new Student
    {
        Id = 3,
        FullName = "Nour Ali",
        Email = "nour@example.com",
        DateOfBirth = new DateTime(2003, 2, 15)
    }
);
        }
    }
}