using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AZLearn.Models
{
    [Table(nameof(Rubric))]
    public class Rubric
    {
        public Rubric()
        {
            /* Initializing the ICollection Navigation Properties */
            Grades = new HashSet<Grade>();
        }
        [Key]
        [Column(TypeName = "int(10)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RubricId { get; set; }

        [Required]
        [Column(TypeName = "int(10)")]
        public int HomeworkId { get; set; }
        
        [Column(TypeName = "boolean")]
        public bool IsChallenge { get; set; } = false;

        [Required]
        [Column(TypeName = "varchar(250)")]
        public string Criteria { get; set; }

        [Required]
        [Column(TypeName = "int(3)")]
        public int Weight { get; set; }

        [Column(TypeName = "boolean")] 
        public bool Archive { get; set; } = false;

        /* Navigation Properties */
        [ForeignKey(nameof(HomeworkId))]
        [InverseProperty(nameof(Models.Homework.Rubrics))]
        public virtual Homework Homework { get; set; }

        [InverseProperty(nameof(Models.Grade.Rubric))]
        public virtual ICollection<Grade> Grades { get; set; }
    }
}
