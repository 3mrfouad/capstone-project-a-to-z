using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AZLearn.Models
{
    [Table(nameof(Course))]
    public class Course
    {
        public Course()
        {
            /* Initializing the Navigation Properties */

            Homeworks = new HashSet<Homework>();
            CohortCourses = new HashSet<CohortCourse>();
        }

        [Key]
        [Column(TypeName = "int(10)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseId { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "varchar(250)")]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "float(5,2)")]
        public float DurationHrs { get; set; }

        [Column(TypeName = "boolean")] public bool Archive { get; set; } = false;

        /*Navigation Properties*/

        [InverseProperty(nameof(Homework.Course))]
        public virtual ICollection<Homework> Homeworks { get; set; }

        [InverseProperty(nameof(CohortCourse.Course))]
        public virtual ICollection<CohortCourse> CohortCourses { get; set; }
    }
}