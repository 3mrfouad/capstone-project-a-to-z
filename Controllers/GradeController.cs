using System;
using System.Collections.Generic;
using System.Linq;
using AZLearn.Data;
using AZLearn.Models;
using AZLearn.Models.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AZLearn.Controllers
{
    public class GradeController : ControllerBase
    {
        /// <summary>
        ///     CreateGradingByStudentId
        ///     Description: Controller action that creates new gradings for a student
        ///     It expects below parameters, and would populate the students grades for a specific rubric / homework
        /// </summary>
        /// <param name="studentId">string provided from frontend, and parsed to int to match model property data type</param>
        /// <param name="gradings">
        ///     Dictionary with rubricId as key and Tuple as value, the Tuple has two parameters i.e., mark and
        ///     instructor comment
        /// </param>
        public static void CreateGradingByStudentId(string studentId,
            Dictionary<string, Tuple<string, string>> gradings)
        {
            /*rubricid | mark | Instructor comment*/
            /* 1       |  0    |    GoodJob    */
            using var context = new AppDbContext();
            var parsedRubricId = 0;
            var parsedStudentId = 0;
            var parsedMark = 0;
            var exception = new ValidationException();


            #region Validation

            if (string.IsNullOrWhiteSpace(studentId))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(studentId),
                    nameof(studentId) + " is null."));
            }
            else
            {
                if (!int.TryParse(studentId, out parsedStudentId))
                    exception.ValidationExceptions.Add(new Exception("Invalid value for Student Id"));

                else if (!context.Users.Any(key => key.UserId == parsedStudentId && key.IsInstructor == false))
                    exception.ValidationExceptions.Add(new Exception("Student Id does not exist"));
                else if (!context.Users.Any(key => key.UserId == parsedStudentId && key.Archive == false))
                    exception.ValidationExceptions.Add(new Exception("Student Id is Archived"));
            }
            
            if (gradings.Count == 0)
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(gradings),
                    nameof(gradings) + " is null."));

            foreach (var (tempRubricId, (tempMark, tempInstructorComment)) in gradings)
            {
                studentId = string.IsNullOrEmpty(studentId) || string.IsNullOrWhiteSpace(studentId)
                    ? null
                    : studentId.Trim();
                var rubricId = string.IsNullOrEmpty(tempRubricId) || string.IsNullOrWhiteSpace(tempRubricId)
                    ? null
                    : tempRubricId.Trim();
                var mark = string.IsNullOrEmpty(tempMark) || string.IsNullOrWhiteSpace(tempMark)
                    ? null
                    : tempMark.Trim();

                var instructorComment =
                    string.IsNullOrEmpty(tempInstructorComment) || string.IsNullOrWhiteSpace(tempInstructorComment)
                        ? null
                        : tempInstructorComment.Trim();

                if (string.IsNullOrWhiteSpace(rubricId))
                {
                    exception.ValidationExceptions.Add(new ArgumentNullException(nameof(rubricId),
                        nameof(rubricId) + " is null."));
                }
                else
                {
                    if (!int.TryParse(rubricId, out parsedRubricId))
                        exception.ValidationExceptions.Add(new Exception("Invalid value for Rubric Id"));

                    else if (!context.Rubrics.Any(key => key.RubricId == parsedRubricId))
                        exception.ValidationExceptions.Add(new Exception("Rubric Id does not exist"));

                    else if (!context.Rubrics.Any(key => key.RubricId == parsedRubricId && key.Archive == false))
                        exception.ValidationExceptions.Add(new Exception("Rubric Id is Archived"));
                }
                
                /*To check whether Grade for  given Rubric id and student id already exists*/

                if (context.Grades.Any(key => key.StudentId == parsedStudentId && key.RubricId == parsedRubricId))
                    exception.ValidationExceptions.Add(new Exception(
                        "Grade Already Exists for this Rubric Id and Student Id"));

                if (string.IsNullOrWhiteSpace(mark))
                {
                    exception.ValidationExceptions.Add(new ArgumentNullException(nameof(mark),
                        nameof(mark) + " is null."));
                }
                else
                {
                    if (!int.TryParse(mark, out parsedMark))
                    {
                        exception.ValidationExceptions.Add(new Exception("Invalid value for Mark"));
                    }
                    else
                    {
                        if (parsedMark > 999 || parsedMark < 0)
                            exception.ValidationExceptions.Add(
                                new Exception("Marks should be between 0 & 999 inclusive."));
                    }
                }

                if (!string.IsNullOrEmpty(instructorComment))
                    if (instructorComment.Length > 250)
                        exception.ValidationExceptions.Add(new Exception("Comment can only be 250 characters long."));

                if (exception.ValidationExceptions.Count > 0) throw exception;

                #endregion

                context.Grades.Add(new Grade
                {
                    RubricId = parsedRubricId,
                    StudentId = parsedStudentId,
                    Mark = parsedMark,
                    InstructorComment = instructorComment
                });
            }

            if (exception.ValidationExceptions.Count > 0) throw exception;


            context.SaveChanges();
        }

        /// <summary>
        ///     UpdateGradingByStudentId
        ///     Description: Controller action that updates new gradings for a student i.e., grades and instructor comments
        ///     It expects below parameters, and would populate the updated students grades for a specific rubric / homework
        /// </summary>
        /// <param name="studentId">string provided from frontend, and parsed to int to match model property data type</param>
        /// <param name="gradings">
        ///     Dictionary with rubricId as key and Tuple as value, the Tuple has two parameters i.e., mark and
        ///     instructor comment
        /// </param>
        /// Test on Postman for Tuple: {
        /*  "1":{
              "Item1":"50",
              "Item2":"Very Good"
          }
      }*/
        public static void UpdateGradingByStudentId(string studentId,
            Dictionary<string, Tuple<string, string>> gradings)
        {
            var parsedRubricId = 0;
            var parsedStudentId = 0;
            var parsedMark = 0;
            var exception = new ValidationException();

            using var context = new AppDbContext();

            #region Validation

            if (gradings.Count == 0)
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(gradings),
                    nameof(gradings) + " is null."));
            foreach (var (tempRubricId, (tempMark, tempInstructorComment)) in gradings)
            {
                studentId = string.IsNullOrEmpty(studentId) || string.IsNullOrWhiteSpace(studentId)
                    ? null
                    : studentId.Trim();
                var rubricId = string.IsNullOrEmpty(tempRubricId) || string.IsNullOrWhiteSpace(tempRubricId)
                    ? null
                    : tempRubricId.Trim();
                var mark = string.IsNullOrEmpty(tempMark) || string.IsNullOrWhiteSpace(tempMark)
                    ? null
                    : tempMark.Trim();

                var instructorComment =
                    string.IsNullOrEmpty(tempInstructorComment) || string.IsNullOrWhiteSpace(tempInstructorComment)
                        ? null
                        : tempInstructorComment.Trim();
                if (string.IsNullOrWhiteSpace(rubricId))
                {
                    exception.ValidationExceptions.Add(new ArgumentNullException(nameof(rubricId),
                        nameof(rubricId) + " is null."));
                }
                else
                {
                    if (!int.TryParse(rubricId, out parsedRubricId))
                        exception.ValidationExceptions.Add(new Exception("Invalid value for Rubric Id"));

                    else if (!context.Rubrics.Any(key => key.RubricId == parsedRubricId))
                        exception.ValidationExceptions.Add(new Exception("Rubric Id does not exist"));

                    else if (!context.Rubrics.Any(key => key.RubricId == parsedRubricId && key.Archive == false))
                        exception.ValidationExceptions.Add(new Exception("Rubric Id is Archived"));
                }

                if (string.IsNullOrWhiteSpace(studentId))
                {
                    exception.ValidationExceptions.Add(new ArgumentNullException(nameof(studentId),
                        nameof(studentId) + " is null."));
                }
                else
                {
                    if (!int.TryParse(studentId, out parsedStudentId))
                        exception.ValidationExceptions.Add(new Exception("Invalid value for Student Id"));

                    else if (!context.Users.Any(key => key.UserId == parsedStudentId && key.IsInstructor == false))
                        exception.ValidationExceptions.Add(new Exception("Student Id does not exist"));
                    else if (!context.Users.Any(key => key.UserId == parsedStudentId && key.Archive == false))
                        exception.ValidationExceptions.Add(new Exception("Student Id is Archived"));
                }
                /*To check whether Grade for  given Rubric id and student id already exists*/
                //*****************************************************************
                if (!context.Grades.Any(key => key.StudentId == parsedStudentId && key.RubricId == parsedRubricId))
                    exception.ValidationExceptions.Add(new Exception(
                        "Grade that you are trying to Update doesn't Exists for this Rubric Id and Student Id"));
                //*****************************************************************
                if (string.IsNullOrWhiteSpace(mark))
                {
                    exception.ValidationExceptions.Add(new ArgumentNullException(nameof(mark),
                        nameof(mark) + " is null."));
                }
                else
                {
                    if (!int.TryParse(mark, out parsedMark))
                    {
                        exception.ValidationExceptions.Add(new Exception("Invalid value for Mark"));
                    }
                    else
                    {
                        if (parsedMark > 999 || parsedMark < 0)
                            exception.ValidationExceptions.Add(
                                new Exception("Marks should be between 0 & 999 inclusive."));
                    }
                }

                if (!string.IsNullOrEmpty(instructorComment))
                    if (instructorComment.Length > 250)
                        exception.ValidationExceptions.Add(new Exception("Comment can only be 250 characters long."));

                if (exception.ValidationExceptions.Count > 0) throw exception;

                #endregion

                var grade = context.Grades.Find(parsedRubricId, parsedStudentId);
                grade.Mark = parsedMark;
                grade.InstructorComment = instructorComment;
            }

            if (exception.ValidationExceptions.Count > 0) throw exception;

            context.SaveChanges();
        }

        /// <summary>
        ///     UpdateGradingByStudentId
        ///     Description: Controller action that updates new gradings for a student i.e., student comments
        ///     It expects below parameters, and would populate the updated student comments for a specific rubric / homework
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="studentComment">Dictionary with rubricId as key and studentComment as value</param>
        /// Testing fpr Dictonary on Postman:  {
        /* "1":"Thankyou for feedback"
     }*/
        public static void UpdateGradingByStudentId(string studentId, Dictionary<string, string> studentComment)
        {
            /*studentID, rubricId ,StudentComment*/
            var parsedRubricId = 0;
            var parsedStudentId = 0;
            var exception = new ValidationException();

            using var context = new AppDbContext();
            var grade = new Grade();

            #region Validation

            if (studentComment.Count == 0)
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(studentComment),
                    nameof(studentComment) + " is null."));

            foreach (var (tempRubricId, tempStudentComment) in studentComment)
            {
                studentId = string.IsNullOrEmpty(studentId) || string.IsNullOrWhiteSpace(studentId)
                    ? null
                    : studentId.Trim();
                var rubricId = string.IsNullOrEmpty(tempRubricId) || string.IsNullOrWhiteSpace(tempRubricId)
                    ? null
                    : tempRubricId.Trim();

                var comment = string.IsNullOrEmpty(tempStudentComment) || string.IsNullOrWhiteSpace(tempStudentComment)
                    ? null
                    : tempStudentComment.Trim();


                if (string.IsNullOrWhiteSpace(rubricId))
                {
                    exception.ValidationExceptions.Add(new ArgumentNullException(nameof(rubricId),
                        nameof(rubricId) + " is null."));
                }
                else
                {
                    if (!int.TryParse(rubricId, out parsedRubricId))
                        exception.ValidationExceptions.Add(new Exception("Invalid value for Rubric Id"));

                    else if (!context.Rubrics.Any(key => key.RubricId == parsedRubricId))
                        exception.ValidationExceptions.Add(new Exception("Rubric Id does not exist"));

                    else if (!context.Rubrics.Any(key => key.RubricId == parsedRubricId && key.Archive == false))
                        exception.ValidationExceptions.Add(new Exception("Rubric Id is Archived"));
                }

                if (string.IsNullOrWhiteSpace(studentId))
                {
                    exception.ValidationExceptions.Add(new ArgumentNullException(nameof(studentId),
                        nameof(studentId) + " is null."));
                }
                else
                {
                    if (!int.TryParse(studentId, out parsedStudentId))
                        exception.ValidationExceptions.Add(new Exception("Invalid value for Student Id"));
                    /*To check if user is an Instructor or Student*/
                    else if (!context.Users.Any(key => key.UserId == parsedStudentId && key.IsInstructor == false))
                        exception.ValidationExceptions.Add(new Exception("Student Id does not exist"));
                    else if (!context.Users.Any(key => key.UserId == parsedStudentId && key.Archive == false))
                        exception.ValidationExceptions.Add(new Exception("Student Id is Archived"));
                }
                /*To check whether Grade for  given Rubric id and student id already exists*/

                if (!context.Grades.Any(key => key.StudentId == parsedStudentId && key.RubricId == parsedRubricId))
                    exception.ValidationExceptions.Add(new Exception(
                        "Grade that you are trying to Update doesn't Exists for this Rubric Id and Student Id"));

                if (!string.IsNullOrEmpty(comment))
                    if (comment.Length > 250)
                        exception.ValidationExceptions.Add(new Exception("Comment can only be 250 characters long."));

                #endregion

                if (exception.ValidationExceptions.Count > 0) throw exception;

                grade = context.Grades.Find(int.Parse(rubricId), int.Parse(studentId));
                grade.StudentComment = comment;
            }

            if (exception.ValidationExceptions.Count > 0) throw exception;

            context.SaveChanges();
        }

        /// <summary>
        ///     GetGradesByStudentId
        ///     This Action takes in Student Id and Homework Id and returns List of Grades associated to that student in the
        ///     specified Homework.
        /// </summary>
        /// <param name="studentId">Student Id</param>
        /// <param name="homeworkId">Homework Id</param>
        /// <returns>List of Grades associated with specified student for specified Homework</returns>
        public static List<Grade> GetGradesByStudentId(string studentId, string homeworkId)
        {
            var parsedStudentId = 0;
            var parsedHomeworkId = 0;

            #region Validation

            var exception = new ValidationException();

            studentId = string.IsNullOrEmpty(studentId) || string.IsNullOrWhiteSpace(studentId)
                ? null
                : studentId.Trim();
            homeworkId = string.IsNullOrEmpty(homeworkId) || string.IsNullOrWhiteSpace(homeworkId)
                ? null
                : homeworkId.Trim();
            using var context = new AppDbContext();
            if (string.IsNullOrWhiteSpace(studentId))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(studentId),
                    nameof(studentId) + " is null."));
            }
            else
            {
                if (!int.TryParse(studentId, out parsedStudentId))
                    exception.ValidationExceptions.Add(new Exception("Invalid value for Student Id"));
                else if (!context.Users.Any(key => key.UserId == parsedStudentId && key.IsInstructor == false))
                    exception.ValidationExceptions.Add(new Exception("Student Id does not exist"));
            }

            if (string.IsNullOrWhiteSpace(homeworkId))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(homeworkId),
                    nameof(homeworkId) + " is null."));
            }
            else
            {
                if (!int.TryParse(homeworkId, out parsedHomeworkId))
                    exception.ValidationExceptions.Add(new Exception("Invalid value for Homework Id"));
                else if (!context.Homeworks.Any(key => key.HomeworkId == parsedHomeworkId))
                    exception.ValidationExceptions.Add(new Exception("Homework Id does not exist"));
            }

            if (exception.ValidationExceptions.Count > 0) throw exception;

            #endregion

            var grades = context.Grades.Include("Rubric.Homework")
                .Where(key => key.Rubric.HomeworkId == parsedHomeworkId && key.StudentId == parsedStudentId)
                .ToList();
            return grades;
        }

        /// <summary>
        ///     GetGradeSummaryForInstructor
        ///     This action returns List of custom objects of data (related to a Homework and grades for all students in specified
        ///     cohort) required in Grade Summary Screen for instructor.
        ///     The screen needs data as per the following Format:
        ///     Student Name, Total Marks, Marks Obtained in all requirements/ Total Requirement Marks, Marks obtained in all
        ///     challenges/ Total Challenge Marks
        /// </summary>
        /// <param name="cohortId">Cohort Id</param>
        /// <param name="homeworkId">Homework Id</param>
        /// <returns></returns>
        public static List<GradeSummaryTypeForInstructor> GetGradeSummaryForInstructor(string cohortId,
            string homeworkId)
        {
            var gradeSummaries = new List<GradeSummaryTypeForInstructor>();
            var parsedCohortId = 0;
            var parsedHomeworkId = 0;

            #region Validation

            var exception = new ValidationException();

            cohortId = string.IsNullOrEmpty(cohortId) || string.IsNullOrWhiteSpace(cohortId) ? null : cohortId.Trim();
            homeworkId = string.IsNullOrEmpty(homeworkId) || string.IsNullOrWhiteSpace(homeworkId)
                ? null
                : homeworkId.Trim();

            using var context = new AppDbContext();
            if (string.IsNullOrWhiteSpace(cohortId))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(cohortId),
                    nameof(cohortId) + " is null."));
            }
            else
            {
                if (!int.TryParse(cohortId, out parsedCohortId))
                    exception.ValidationExceptions.Add(new Exception("Invalid value for Cohort Id"));
                else if (!context.Cohorts.Any(key => key.CohortId == parsedCohortId))
                    exception.ValidationExceptions.Add(new Exception("Cohort Id does not exist"));
            }

            if (string.IsNullOrWhiteSpace(homeworkId))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(homeworkId),
                    nameof(homeworkId) + " is null."));
            }
            else
            {
                if (!int.TryParse(homeworkId, out parsedHomeworkId))
                    exception.ValidationExceptions.Add(new Exception("Invalid value for Homework Id"));
                else if (!context.Homeworks.Any(key => key.HomeworkId == parsedHomeworkId))
                    exception.ValidationExceptions.Add(new Exception("Homework Id does not exist"));
            }

            if (exception.ValidationExceptions.Count > 0) throw exception;

            #endregion

            var studentsByCohort = UserController.GetStudentsByCohortId(cohortId);

            //rubricWeightByGroup is an array with first element- total weight of requirements, second element- total weight of challenges for a specified Homework
            var rubricWeightByGroup = RubricController.GetRubricsByHomeworkId(homeworkId)
                .GroupBy(key => key.IsChallenge).Select(key => key.Sum(s => s.Weight)).ToArray();

            //If there are no challenges, then weight of challenge = 0 to avoid NullValueException
            if (rubricWeightByGroup.Length == 1) rubricWeightByGroup[1] = 0;
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
                        $" /{rubricWeightByGroup[1]}", totalTimeSpentOnHomework, studentName, student.UserId);
                }
                else
                {
                    var total = gradesOfStudent.Sum(key => key.Mark);
                    //Group marks of student in a homework by requirements and challenges and store them in array
                    var marksByGroup = gradesOfStudent.GroupBy(key => key.Rubric.IsChallenge)
                        .Select(key => key.Sum(s => s.Mark))
                        .ToArray();
                    //In case there are no challenges, we will show 0/0 for challenges' marks
                    if (marksByGroup.Length == 1) marksByGroup[1] = 0;

                    gradeSummary = new GradeSummaryTypeForInstructor($"{total}",
                        $"{marksByGroup[0]}/{rubricWeightByGroup[0]}",
                        $"{marksByGroup[1]}/{rubricWeightByGroup[1]}", totalTimeSpentOnHomework, studentName,
                        student.UserId);
                }

                gradeSummaries.Add(gradeSummary);
            }

            return gradeSummaries;

        }
    }
}