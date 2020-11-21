using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AZLearn.Models
{
    [Table(nameof(Cohort))]
    public class Cohort
    {
        public Cohort()
        {
            /* Initialising the Navigation Properties */
            Users = new HashSet<User>();
            CohortCourses = new HashSet<CohortCourse>();
        }
        [Key]
        [Column(TypeName = "int(10)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CohortId { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }

        [Column(TypeName = "int(3)")]
        public int? Capacity { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string ModeOfTeaching { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string City { get; set; }

        [Column(TypeName = "boolean")]
        public bool Archive { get; set; } = false;

        /*Navigation Properties*/
        [InverseProperty(nameof(Models.CohortCourse.Cohort))]
        public virtual ICollection<CohortCourse> CohortCourses { get; set; }

        [InverseProperty(nameof(Models.User.Cohort))]
        public virtual ICollection<User> Users { get; set; }
    }
}
