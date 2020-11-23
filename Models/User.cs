using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AZLearn.Models
{
    [Table(nameof(User))]
    public class User
    {
        public User()
        {
            /*Constructor to Initialize ICollections defined below*/
            Courses = new HashSet<Course>();
            Homeworks = new HashSet<Homework>();
            Notifications = new HashSet<Notification>();
            ShoutOutsStudent = new HashSet<ShoutOut>();
            ShoutOutsPeer = new HashSet<ShoutOut>();
            Timesheets = new HashSet<Timesheet>();
            Grades = new HashSet<Grade>();
        }

        [Key]
        [Column(TypeName = "int(10)")]
        /*Auto generates unique id number*/
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        /*Foreign Keys*/

        [Column(TypeName = "int(10)")] 
        public int? CohortId { get; set; } 

        /*General Properties:*/
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "varchar(250)")]
        public string PasswordHash { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Email { get; set; }

        /*For Security Reasons,the default value would be selected as Students in case someone forgot to specify who he is ,he will have access to Student privileges */

        [Required]
        [Column(TypeName = "boolean")]
        public bool IsInstructor { get; set; } = false;

        [Column(TypeName = "boolean")]
        public bool Archive { get; set; } = false;

        /*   Navigation properties:*/

        [ForeignKey(nameof(CohortId))]
        [InverseProperty(nameof(Models.Cohort.Users))]
        public virtual Cohort Cohort { get; set; }

        /*Creating and inverse property of Course to User*/
        [InverseProperty(nameof(Models.Course.Instructor))]
        public virtual ICollection<Course> Courses { get; set; }

        /*Creating and inverse property of Homework to User*/
        [InverseProperty(nameof(Models.Homework.Instructor))]
        public virtual ICollection<Homework> Homeworks { get; set; }

        /*Creating and inverse property of Notification to User*/
        [InverseProperty(nameof(Models.Notification.Student))]
        public virtual ICollection<Notification> Notifications { get; set; }

        /*Creating and inverse property of ShoutOutsStudent to User*/
        [InverseProperty(nameof(Models.ShoutOut.Student))]
        public virtual ICollection<ShoutOut> ShoutOutsStudent { get; set; }

        /*Creating and inverse property of ShoutOutsPeer to User*/
        [InverseProperty(nameof(Models.ShoutOut.Peer))]
        public virtual ICollection<ShoutOut> ShoutOutsPeer { get; set; }

        /*Creating and inverse property of Timesheets to User*/
        [InverseProperty(nameof(Models.Timesheet.Student))]
        public virtual ICollection<Timesheet> Timesheets { get; set; }

        /*Creating and inverse property of Grade to User*/
        [InverseProperty(nameof(Models.Grade.Student))]
        public virtual ICollection<Grade> Grades { get; set; }







    }
}