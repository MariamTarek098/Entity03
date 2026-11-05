using Entity03.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entity03.Configurations
{
	public class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
	{
		public void Configure(EntityTypeBuilder<Instructor> builder)
		{
			builder.HasKey(i => i.Id);

			builder.Property(i => i.FullName)
				   .IsRequired()
				   .HasMaxLength(100);

            builder.Property(i => i.Email)
				   .IsRequired()
				   .HasMaxLength(150);

            builder.HasIndex(i => i.Email)
                   .IsUnique();

            builder.Property(i => i.Bio)
				   .IsRequired(false)
				   .HasMaxLength(500);

			builder.Property(i => i.JoinedDate)
				   .IsRequired();

            builder.HasData(
                    new Instructor
                    {
                        Id = 1,
                        FullName = "Ahmed Mohamed",
                        Email = "ahmed@example.com",
                        Bio = "C# and .NET Instructor",
                        JoinedDate = new DateTime(2024, 1, 10)
                    },
                    new Instructor
                    {
                        Id = 2,
                        FullName = "Sara Ali",
                        Email = "sara@example.com",
                        Bio = "Frontend Development Instructor",
                        JoinedDate = new DateTime(2024, 3, 15)
    }
);
        }
	}
}