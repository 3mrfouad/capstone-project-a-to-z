using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AZLearn.Models
{
    [Table(nameof(CohortCourse))]
    public class CohortCourse
    {
        /* Composite Key with CohortId, CourseId */
        [Key, Column(Order = 0, TypeName = "int(10)")]
        public int CohortId { get; set; }

        [Key, Column(Order = 1, TypeName = "int(10)")]
        public int CourseId { get; set; }


        [Required]
        [Column(TypeName = "int(10)")]
        public int InstructorId { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }

        [Column(TypeName = "varchar(250)")]
        public string ResourcesLink { get; set; }

        [Column(TypeName = "boolean")]
        public bool Archive { get; set; } = false;

        /* Navigation Properties */
        [InverseProperty(nameof(Models.Cohort.CohortCourses))]
        public virtual Cohort Cohort { get; set; }

        [InverseProperty(nameof(Models.Course.CohortCourses))]
        public virtual Course Course { get; set; }

        [ForeignKey(nameof(InstructorId))]
        [InverseProperty(nameof(Models.User.CohortCourses))]
        public virtual User Instructor { get; set; }
    }
}
