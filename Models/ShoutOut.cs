using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AZLearn.Models
{
    [Table(nameof(ShoutOut))]
    public class ShoutOut
    {
        [Key]
        [Column(TypeName = "int(10)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ShoutOutId { get; set; }

        [Required]
        [Column(TypeName = "int(10)")]
        public int StudentId { get; set; }


        [Required]
        [Column(TypeName = "int(10)")]
        public int PeerId { get; set; }

        [Required]
        [Column(TypeName = "int(10)")]
        public int HomeworkId { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [Required]
        [Column(TypeName = "float(5,2)")]
        public float DurationMins { get; set; }

        [Required]
        [Column(TypeName = "varchar(250)")]
        public string Topic { get; set; }

        [Column(TypeName = "varchar(250)")]
        public string Comment { get; set; }

        [Column(TypeName = "boolean")] 
        public bool Archive { get; set; } = false;

        /* Navigation Properties */
        [ForeignKey(nameof(HomeworkId))]
        [InverseProperty(nameof(Models.Homework.ShoutOuts))]
        public virtual Homework Homework { get; set; }

        [ForeignKey(nameof(StudentId))]
        [InverseProperty(nameof(Models.User.ShoutOutsStudent))]
        public virtual User Student { get; set; }

        [ForeignKey(nameof(PeerId))]
        [InverseProperty(nameof(Models.User.ShoutOutsPeer))]
        public virtual User Peer { get; set; }
    }
}
