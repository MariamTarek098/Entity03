using Entity03.Enums;

namespace Entity03.Models
{
    public class Course
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public CourseLevel Level { get; set; }

        public bool IsPublished { get; set; }

        public DateTime? PublishedDate { get; set; }

        public int InstructorId { get; set; }

        public int CategoryId { get; set; }

        public Instructor Instructor { get; set; }

        public Category Category { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; } = new HashSet<Enrollment>();
    }
}