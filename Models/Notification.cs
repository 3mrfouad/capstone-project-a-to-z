
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AZLearn.Models
{
    [Table(nameof(Notification))]
    public class Notification
    {
        [Key]
        [Column(TypeName = "int(10)")]

        /*Auto generates unique id number*/
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NotificationId { get; set; }

        /*Foreign Key*/
        [Required]
        [Column(TypeName = "int(10)")]
        public int StudentId { get; set; }

        /*General Properties:*/
        [Required]
        [Column(TypeName = "varchar(250)")]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [Column(TypeName = "boolean")]
        public bool Archive { get; set; } = false;

        /*   Navigation property:*/

        [ForeignKey(nameof(StudentId))]
        [InverseProperty(nameof(Models.User.Notifications))]
        public virtual User Student { get; set; }
    }
}