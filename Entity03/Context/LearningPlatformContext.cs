using Entity03.Models;
using Microsoft.EntityFrameworkCore;

namespace Entity03.Context
{
    public class LearningPlatformContext : DbContext
    {
        public LearningPlatformContext()
        {
        }

        public LearningPlatformContext(DbContextOptions<LearningPlatformContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=localhost;Database=LearningPlatformDB;Trusted_Connection=True;TrustServerCertificate=True");
            }
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(LearningPlatformContext).Assembly);
        }
    }
}