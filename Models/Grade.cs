using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AZLearn.Models
{
    [Table(nameof(Grade))]
    public class Grade
    {
        [Key]
        [Column(Order = 0, TypeName = "int(10)")]
        public int RubricId { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "int(10)")]
        public int StudentId { get; set; }

        [Required]
        [Column(TypeName = "int(3)")]
        public int Mark { get; set; }

        [Column(TypeName = "varchar(250)")] public string InstructorComment { get; set; }

        [Column(TypeName = "varchar(250)")] public string StudentComment { get; set; }

        [Column(TypeName = "boolean")] public bool Archive { get; set; } = false;

        /* Navigation Properties */

        [InverseProperty(nameof(Models.Rubric.Grades))]
        public virtual Rubric Rubric { get; set; }

        [InverseProperty(nameof(User.Grades))] public virtual User Student { get; set; }
    }
}