using Entity03.Enums;

namespace Entity03.Models
{
    public class Enrollment
    {
        public int StudentId { get; set; }

        public int CourseId { get; set; }

        public DateTime EnrollmentDate { get; set; }

        public decimal ProgressPercentage { get; set; }

        public EnrollmentStatus Status { get; set; }

        public decimal? FinalGrade { get; set; }

        public Student Student { get; set; }

        public Course Course { get; set; }
    }
}