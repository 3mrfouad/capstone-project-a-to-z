using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace sometest.Models
{
    [Table(nameof(Grade))]
    public class Grade
    {
        public Grade()
        {
            //Grades = new HashSet<Grade>();
        }
        //[Key]
        //[Column(TypeName = "int(10)")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int GradeId { get; set; }

        //[Required]
        //[Column(TypeName = "int(10)")]
        [Key, Column(Order = 0)]
        public int RubricId { get; set; }

        //[Required]
        //[Column(TypeName = "int(10)")]
        [Key, Column(Order = 1)]
        public int StudentId { get; set; }

        [Required]
        [Column(TypeName = "int(3)")]
        public int Mark { get; set; }

        [Column(TypeName = "varchar(250)")]
        public string InstructorComment { get; set; }

        [Column(TypeName = "varchar(250)")]
        public string StudentComment { get; set; }

        [Column(TypeName = "boolean")]
        public bool Archive { get; set; } = false;

        //[ForeignKey(nameof(RubricId))]
        [InverseProperty(nameof(Models.Rubric.Grades))]
        public virtual Rubric Rubric { get; set; }

        //[ForeignKey(nameof(StudentId))]
        [InverseProperty(nameof(Models.User.Grades))]
        public virtual User Student { get; set; }
    }
}
