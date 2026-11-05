
using Entity03.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entity03.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.Description)
                   .IsRequired(false)
                   .HasMaxLength(500);

            builder.HasMany(c => c.Courses)
                   .WithOne(course => course.Category)
                   .HasForeignKey(course => course.CategoryId);

 
            builder.HasOne(c => c.ParentCategory)
                   .WithMany(c => c.SubCategories)
                   .HasForeignKey(c => c.ParentCategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
    new Category
    {
        Id = 1,
        Name = "Programming",
        Description = "Programming courses",
        ParentCategoryId = null
    },
    new Category
    {
        Id = 2,
        Name = "C#",
        Description = "C# courses",
        ParentCategoryId = 1
    },
    new Category
    {
        Id = 3,
        Name = "Web Development",
        Description = "Web development courses",
        ParentCategoryId = null
    }
);
        }
    }
}
