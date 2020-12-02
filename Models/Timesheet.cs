using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AZLearn.Models
{
    [Table(nameof(Timesheet))]
    public class Timesheet
    {
        [Key]
        [Column(TypeName = "int(10)")]

        /* Auto generates unique id number*/

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TimesheetId { get; set; }

        /*Foreign Keys*/

        [Required]
        [Column(TypeName = "int(10)")]
        public int HomeworkId { get; set; }

        [Required]
        [Column(TypeName = "int(10)")]
        public int StudentId { get; set; }

        [Required]
        [Column(TypeName = "float(5,2)")]
        public float SolvingTime { get; set; }

        [Column(TypeName = "float(5,2)")] public float StudyTime { get; set; } = 0;

        [Column(TypeName = "boolean")] public bool Archive { get; set; } = false;

        /*Navigation properties:*/

        [ForeignKey(nameof(HomeworkId))]
        [InverseProperty(nameof(Models.Homework.Timesheets))]
        public virtual Homework Homework { get; set; }

        [ForeignKey(nameof(StudentId))]
        [InverseProperty(nameof(User.Timesheets))]
        public virtual User Student { get; set; }
    }
}