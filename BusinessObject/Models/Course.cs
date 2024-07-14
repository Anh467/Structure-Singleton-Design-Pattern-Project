using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class Course
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseId { get; set; }
        public string CourseName { get; set; } = string.Empty;
        [Range(0, int.MaxValue, ErrorMessage = "This field can't be negative")]
        public int Credit {  get; set; }    

        public virtual ICollection<Enrollment>? Enrollments { get; set; }
    }
}
