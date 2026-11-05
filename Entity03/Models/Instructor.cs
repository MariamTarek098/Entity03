using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity03.Models
{
    public class Instructor
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string? Bio { get; set; }

        public DateTime JoinedDate { get; set; }

        public ICollection<Course> Courses { get; set; } = new HashSet<Course>();
    }
}
