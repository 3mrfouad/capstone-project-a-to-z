
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AZLearn.Models
{
        [Table(nameof(Homework))]
        public class Homework
        {
            public Homework()
            {
                /*Constructor to Initialize ICollections defined below*/
                ShoutOuts = new HashSet<ShoutOut>();
                Timesheets = new HashSet<Timesheet>();
                Rubrics = new HashSet<Rubric>();
            }

            [Key]
            [Column(TypeName = "int(10)")]

            /*Auto generates unique id number*/
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int HomeworkId { get; set; }

            /*Foreign Keys*/
            [Required]
            [Column(TypeName = "int(10)")] 
            public int CourseId { get; set; } = 0;

            [Required]
            [Column(TypeName = "int(10)")]
            public int InstructorId { get; set; }

            /*General Properties:*/
            [Column(TypeName = "boolean")]
            public bool IsAssignment { get; set; } = false;

            [Required]
            [Column(TypeName = "varchar(100)")]
            public string Title { get; set; }

            [Column(TypeName = "float(5,2)")]
            public float AvgCompletionTime { get; set; }

            /* DateTime is used here since we require time as well to be displayed along with Date E.g: Due at 9:00am on Monday 08/12/2020 */
            [Column(TypeName = "datetime")]
            public DateTime? DueDate { get; set; }

            [Column(TypeName = "datetime")]
            public DateTime? ReleaseDate { get; set; }

            [Column(TypeName = "varchar(250)")]
            public string DocumentLink { get; set; }

            [Column(TypeName = "varchar(250)")]
            public string GitHubClassRoomLink { get; set; }

            [Column(TypeName = "boolean")]
            public bool Archive { get; set; } = false;

            /*   Navigation properties:*/

            [ForeignKey(nameof(CourseId))]
            [InverseProperty(nameof(Models.Course.Homeworks))]
            public virtual Course Course { get; set; }

            [ForeignKey(nameof(InstructorId))]
            [InverseProperty(nameof(Models.User.Homeworks))]
            public virtual User Instructor { get; set; }

            /*Creating and inverse property of Shoutout to Homework*/
            [InverseProperty(nameof(Models.ShoutOut.Homework))]
            public virtual ICollection<ShoutOut> ShoutOuts { get; set; }

            /*Creating and inverse property of Timesheet to Homework*/

            [InverseProperty(nameof(Models.Timesheet.Homework))]
            public virtual ICollection<Timesheet> Timesheets { get; set; }

            /*Creating and inverse property of Rubrics to Homework*/

            [InverseProperty(nameof(Models.Rubric.Homework))]
            public virtual ICollection<Rubric> Rubrics { get; set; }

        }
}

