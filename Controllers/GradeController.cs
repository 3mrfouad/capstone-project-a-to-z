using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AZLearn.Data;
using AZLearn.Models;
using Microsoft.EntityFrameworkCore;

namespace AZLearn.Controllers
{
    public class GradeController : ControllerBase
    {

        public List<Grade> GetGrades(string homeworkId)
        {
            return new List<Grade>();
        }
        public void GetGradeByRubricId(string rubricId)
        {
        }
        /*public static List<Grade> GetGradesByStudentId(string studentId, string homeworkId)
        {
            using var context = new AppDbContext();
            return context.Grades.Include("Rubric.Homework")
                .Where(key => key.Rubric.HomeworkId == int.Parse(homeworkId) && key.StudentId == int.Parse(studentId))
                .ToList();
        }
        public static List<Homework> GetHomeworksByCourseId(string courseId, string cohortId)
        {
            using AppDbContext context = new AppDbContext();
            return context.Homeworks
                .Where(key => key.CourseId == int.Parse(courseId) && key.CohortId == int.Parse(cohortId)).ToList();
        }*/
        /// <summary>
        ///     CreateGradingByStudentId
        ///     Description: Controller action that creates new gradings for a student
        ///     It expects below parameters, and would populate the students grades for a specific rubric / homework
        /// </summary>
        /// <param name="studentId">string provided from frontend, and parsed to int to match model property data type</param>
        /// <param name="gradings">Dictionary with rubricId as key and Tuple as value, the Tuple has two parameters i.e., mark and instructor comment</param>
        public static void CreateGradingByStudentId(string studentId, Dictionary<string, Tuple<string, string>> gradings)
        {
            using var context = new AppDbContext();
            foreach (var (rubricId, (mark, instructorComment)) in gradings)
            {
                context.Grades.Add(new Grade()
                {
                    RubricId = int.Parse(rubricId),
                    StudentId = int.Parse(studentId),
                    Mark = int.Parse(mark),
                    InstructorComment = instructorComment
                });
            }
            context.SaveChanges();
        }
        /// <summary>
        ///     UpdateGradingByStudentId
        ///     Description: Controller action that updates new gradings for a student i.e., grades and instructor comments
        ///     It expects below parameters, and would populate the updated students grades for a specific rubric / homework
        /// </summary>
        /// <param name="studentId">string provided from frontend, and parsed to int to match model property data type</param>
        /// <param name="gradings">Dictionary with rubricId as key and Tuple as value, the Tuple has two parameters i.e., mark and instructor comment</param>
        public static void UpdateGradingByStudentId(string studentId, Dictionary<string, Tuple<string, string>> gradings)
        {
            using var context = new AppDbContext();
            var grade = new Grade(); ;
            foreach (var (rubricId, (mark, instructorComment)) in gradings)
            {
                grade = context.Grades.Find(int.Parse(rubricId), int.Parse(studentId));
                grade.Mark = int.Parse(mark);
                grade.InstructorComment = instructorComment;
            }
            context.SaveChanges();
        }
        /// <summary>
        /// UpdateGradingByStudentId
        ///     Description: Controller action that updates new gradings for a student i.e., student comments
        ///     It expects below parameters, and would populate the updated student comments for a specific rubric / homework
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="studentComment">Dictionary with rubricId as key and studentComment as value</param>
        public static void UpdateGradingByStudentId(string studentId, Dictionary<string, string> studentComment)
        {
            using var context = new AppDbContext();
            var grade = new Grade(); ;
            foreach (var (rubricId, comment) in studentComment)
            {
                grade = context.Grades.Find(int.Parse(rubricId), int.Parse(studentId));
                grade.StudentComment = comment;
            }
            context.SaveChanges();
        }


    }
}