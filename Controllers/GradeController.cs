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

        /// <summary>
        /// This action returns List of custom objects of data (related to a Homework and grades for all students in specified cohort) required in Grade Summary Screen for instructor.
        /// The screen needs data as per the following Format:
        /// Student Name, Total Marks, Marks Obtained in all requirements/ Total Requirement Marks, Marks obtained in all challenges/ Total Challenge Marks
        /// </summary>
        /// <param name="cohortId">Cohort Id</param>
        /// <param name="homeworkId">Homework Id</param>
        /// <returns></returns>
        public static List<GradeSummaryTypeForInstructor> GetGradeSummaryForInstructor(string cohortId,
            string homeworkId)
        {
            List<GradeSummaryTypeForInstructor> gradeSummaries = new List<GradeSummaryTypeForInstructor>();

            using var context = new AppDbContext();
            var studentsByCohort = UserController.GetStudentsByCohortId(cohortId);

            //rubricWeightByGroup is an array with first element- total weight of requirements, second element- total weight of challenges for a specified Homework
            var rubricWeightByGroup = RubricController.GetRubricsByHomeworkId(homeworkId)
                .GroupBy(key => key.IsChallenge).Select(key => key.Sum(s => s.Weight)).ToArray();

            //If there are no challenges, then weight of challenge = 0 to avoid NullValueException
            if (rubricWeightByGroup.Length == 1)
            {
                rubricWeightByGroup[1] = 0;
            }
            //Loop to get GradeSummary for all students in a Cohort
            foreach (var student in studentsByCohort)
            {
                string totalTimeSpentOnHomework;
                GradeSummaryTypeForInstructor gradeSummary;
                var studentName = student.Name;
                var timesheet = TimesheetController.GetTimesheetByHomeworkId(homeworkId, $"{student.UserId}");
                if (timesheet == null)
                    totalTimeSpentOnHomework = " ";
                else
                    totalTimeSpentOnHomework = (timesheet.SolvingTime + timesheet.StudyTime).ToString();

                var gradesOfStudent = GetGradesByStudentId($"{student.UserId}", homeworkId);
                //If grades do not exist for that student (in case Instructor has not added/marked grades for that student)- show empty string for grades 
                if (gradesOfStudent.Count == 0)
                {
                    gradeSummary = new GradeSummaryTypeForInstructor(" ", $" /{rubricWeightByGroup[0]}",
                        $" /{rubricWeightByGroup[1]}", totalTimeSpentOnHomework, studentName);
                }
                else
                {
                    var total = gradesOfStudent.Sum(key => key.Mark);
                    //Group marks of student in a homework by requirements and challenges and store them in array
                    var marksByGroup = gradesOfStudent.GroupBy(key => key.Rubric.IsChallenge).Select(key => key.Sum(s => s.Mark))
                        .ToArray();
                    //In case there are no challenges, we will show 0/0 for challenges' marks
                    if (marksByGroup.Length == 1)
                    {
                        marksByGroup[1] = 0;
                    }

                    gradeSummary = new GradeSummaryTypeForInstructor($"{total}",
                        $"{marksByGroup[0]}/{rubricWeightByGroup[0]}",
                        $"{marksByGroup[1]}/{rubricWeightByGroup[1]}", totalTimeSpentOnHomework, studentName);
                }
                gradeSummaries.Add(gradeSummary);
            }
            return gradeSummaries;
        }
    }
}
