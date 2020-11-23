using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AZLearn.Data;
using AZLearn.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.WebEncoders.Testing;

namespace AZLearn.Controllers
{
    public class GradeController :ControllerBase
    {
        /// <summary>
        /// This Action takes in Student Id and Homework Id and returns List of Grades associated to that student in the specified Homework.
        /// </summary>
        /// <param name="studentId">Student Id</param>
        /// <param name="homeworkId">Homework Id</param>
        /// <returns>List of Grades associated with specified student for specified Homework</returns>
        public static List<Grade> GetGradesByStudentId(string studentId, string homeworkId)
        {
            int parsedStudentId = int.Parse(studentId);
            int parsedHomeworkId = int.Parse(homeworkId);
            using var context = new AppDbContext();
            var grades = context.Grades.Include("Rubric.Homework")
                .Where(key => key.Rubric.HomeworkId == parsedHomeworkId && key.StudentId == parsedStudentId)
                .ToList();
            return grades;
        }
    }
}
