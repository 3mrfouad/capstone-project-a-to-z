using System;
using System.Collections.Generic;
using System.Linq;
using AZLearn.Data;
using AZLearn.Models;
using Microsoft.AspNetCore.Mvc;

namespace AZLearn.Controllers
{
    public class HomeworkController : Controller
    {
        /// <summary>
        ///     This action takes in Course Id and Cohort Id and returns List of Homeworks for specified course under the specified
        ///     Cohort Id
        /// </summary>
        /// <param name="courseId">Course Id</param>
        /// <param name="cohortId">Cohort Id</param>
        /// <returns>List of Homeworks for specified course under the specified Cohort Id</returns>
        public static List<Homework> GetHomeworksByCourseId(string courseId, string cohortId)
        {
            List<Homework> homeworks;
            var parsedCohortId = int.Parse(cohortId);
            var parsedCourseId = int.Parse(courseId);
            using (var context = new AppDbContext())
            {
                homeworks = context.Homeworks
                    .Where(key => key.CourseId == parsedCourseId && key.CohortId == parsedCohortId).ToList();
            }

            return homeworks;
        }

        /// <summary>
        ///     This Action creates a new Homework for a specified Course under a specified Cohort and adds it to DB
        /// </summary>
        /// <param name="courseId">Course Id</param>
        /// <param name="instructorId">Instructor Id</param>
        /// <param name="cohortId">Cohort Id</param>
        /// <param name="isAssignment">A boolean to determine whether this Homework is Assignment or Practice</param>
        /// <param name="title">Title of the Homework</param>
        /// <param name="avgCompletionTime">Average Completion Time to complete the Homework (specified by Instructor)</param>
        /// <param name="dueDate">Due Date of this Homework</param>
        /// <param name="releaseDate">Release Date of this Homework</param>
        /// <param name="documentLink">Link To Google Drive where this Homework Document can be accessed</param>
        /// <param name="gitHubClassRoomLink">Link To GitHub Classroom where students can create repository to submit this Homework</param>
        public static void CreateHomeworkByCourseId(string courseId, string instructorId, string cohortId,
            string isAssignment, string title, string avgCompletionTime, string dueDate, string releaseDate,
            string documentLink, string gitHubClassRoomLink)
        {
            var parsedCourseId = int.Parse(courseId);
            var parsedInstructorId = int.Parse(instructorId);
            var parsedCohortId = int.Parse(cohortId);
            var parsedIsAssignment = bool.Parse(isAssignment);
            var parsedAvgCompletionTime = float.Parse(avgCompletionTime);
            var parsedDuedate = DateTime.Parse(dueDate);
            var parsedReleasedate = DateTime.Parse(releaseDate);

            using var context = new AppDbContext();

            var newHomework = new Homework
            {
                CourseId = parsedCourseId,
                InstructorId = parsedInstructorId,
                CohortId = parsedCohortId,
                IsAssignment = parsedIsAssignment,
                Title = title,
                AvgCompletionTime = parsedAvgCompletionTime,
                DueDate = parsedDuedate,
                ReleaseDate = parsedReleasedate,
                DocumentLink = documentLink,
                GitHubClassRoomLink = gitHubClassRoomLink
            };
            context.Homeworks.Add(newHomework);
            context.SaveChanges();
        }

        /// <summary>
        ///     This Action updates an existing Homework and saves the changes in DB.
        /// </summary>
        /// <param name="homeworkId">Homework Id</param>
        /// <param name="courseId">Course Id</param>
        /// <param name="instructorId">Instructor Id</param>
        /// <param name="cohortId">Cohort Id</param>
        /// <param name="isAssignment">Boolean to specify if Homework is Assignment ot Practice</param>
        /// <param name="title">Title of the Homework</param>
        /// <param name="avgCompletionTime">Title of the Homework</param>
        /// <param name="dueDate">Due Date of this Homework</param>
        /// <param name="releaseDate">Release Date of this Homework</param>
        /// <param name="documentLink">Link To Google Drive where this Homework Document can be accessed</param>
        /// <param name="gitHubClassRoomLink">Link To GitHub Classroom where students can create repository to submit this Homework</param>
        public static void UpdateHomeworkById(string homeworkId, string courseId, string instructorId, string cohortId,
            string isAssignment, string title, string avgCompletionTime, string dueDate, string releaseDate,
            string documentLink, string gitHubClassRoomLink)
        {
            var parsedHomeworkId = int.Parse(homeworkId);
            var parsedCourseId = int.Parse(courseId);
            var parsedInstructorId = int.Parse(instructorId);
            var parsedCohortId = int.Parse(cohortId);
            var parsedIsAssignment = bool.Parse(isAssignment);
            var parsedAvgCompletionTime = float.Parse(avgCompletionTime);
            var parsedDuedate = DateTime.Parse(dueDate);
            var parsedReleasedate = DateTime.Parse(releaseDate);

            using var context = new AppDbContext();

            var homework = context.Homeworks.SingleOrDefault(key => key.HomeworkId == parsedHomeworkId);
            homework.CourseId = parsedCourseId;
            homework.InstructorId = parsedInstructorId;
            homework.CohortId = parsedCohortId;
            homework.IsAssignment = parsedIsAssignment;
            homework.Title = title;
            homework.AvgCompletionTime = parsedAvgCompletionTime;
            homework.DueDate = parsedDuedate;
            homework.ReleaseDate = parsedReleasedate;
            homework.DocumentLink = documentLink;
            homework.GitHubClassRoomLink = gitHubClassRoomLink;

            context.SaveChanges();
        }

        /// <summary>
        ///     GetHomeworkById
        ///     Description: Controller action that gets Homework information by the associated HomeworkId
        ///     It expects below parameters, and would populate the user information according to the parameter specified
        /// </summary>
        /// <param name="homeworkId"></param>
        /// <returns>It returns the Homework Information based on the homework id </returns>
        public static Homework GetHomeworkById(string homeworkId)

        {
            Homework result;
            var parsedHomeworkId = int.Parse(homeworkId);
            using var context = new AppDbContext();
            {
                result = context.Homeworks.Single(key => key.HomeworkId == parsedHomeworkId);
            }
            return result;
        }
    }
}