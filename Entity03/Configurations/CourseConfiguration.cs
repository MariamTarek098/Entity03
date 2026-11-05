using Entity03.Enums;
using Entity03.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entity03.Configurations
{
	public class CourseConfiguration : IEntityTypeConfiguration<Course>
	{
		public void Configure(EntityTypeBuilder<Course> builder)
		{
			builder.HasKey(c => c.Id);

			builder.Property(c => c.Title)
				   .IsRequired()
				   .HasMaxLength(200);

			builder.Property(c => c.Description)
				   .IsRequired(false)
				   .HasMaxLength(1000);

			builder.Property(c => c.Price)
				   .HasPrecision(10, 2);

			builder.HasOne(c => c.Instructor)
				   .WithMany(i => i.Courses)
				   .HasForeignKey(c => c.InstructorId)
				   .OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(c => c.Category)
				   .WithMany(cat => cat.Courses)
				   .HasForeignKey(c => c.CategoryId)
				   .OnDelete(DeleteBehavior.Restrict);

			builder.HasData(
	new Course
	{
		Id = 1,
		Title = "C# Fundamentals",
		Description = "Learn C# basics and OOP",
		Price = 500,
		Level = CourseLevel.Beginner,
		IsPublished = true,
		PublishedDate = new DateTime(2024, 5, 1),
		InstructorId = 1,
		CategoryId = 2
	},
	new Course
	{
		Id = 2,
		Title = "React Fundamentals",
		Description = "Learn React from scratch",
		Price = 700,
		Level = CourseLevel.Intermediate,
		IsPublished = false,
		PublishedDate = null,
		InstructorId = 2,
		CategoryId = 3
	}
);

		}
	}
}