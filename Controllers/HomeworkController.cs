using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AZLearn.Data;
using AZLearn.Models;

namespace AZLearn.Controllers
{
    public class HomeworkController : ControllerBase
    {
        /// <summary>
        /// This action takes in Course Id and Cohort Id and returns List of Homeworks for specified course under the specified Cohort Id
        /// </summary>
        /// <param name="courseId">Course Id</param>
        /// <param name="cohortId">Cohort Id</param>
        /// <returns>List of Homeworks for specified course under the specified Cohort Id</returns>
        public static List<Homework> GetHomeworksByCourseId(string courseId, string cohortId)
        {
            List<Homework> homeworks;
            int parsedCohortId = int.Parse(cohortId);
            int parsedCourseId = int.Parse(courseId);
            using (AppDbContext context = new AppDbContext())
            {
                homeworks = context.Homeworks
                    .Where(key => key.CourseId == parsedCourseId && key.CohortId == parsedCohortId).ToList();
            }
            return homeworks;
        }

        /// <summary>
        /// This Action creates a new Homework for a specified Course under a specified Cohort and adds it to DB
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
            int parsedCourseId = int.Parse(courseId);
            int parsedInstructorId = int.Parse(instructorId);
            int parsedCohortId = int.Parse(cohortId);
            bool parsedIsAssignment = bool.Parse(isAssignment);
            float parsedAvgCompletionTime = float.Parse(avgCompletionTime);
            DateTime parsedDuedate = DateTime.Parse(dueDate);
            DateTime parsedReleasedate = DateTime.Parse(releaseDate);

            using (AppDbContext context = new AppDbContext())
            {
                Homework newHomework = new Homework()
                {
                    CourseId = parsedCourseId,
                    InstructorId = parsedInstructorId,
                    CohortId = parsedCohortId,
                    IsAssignment = parsedIsAssignment,
                    AvgCompletionTime = parsedAvgCompletionTime,
                    DueDate = parsedDuedate,
                    ReleaseDate = parsedReleasedate
                };
                context.Homeworks.Add(newHomework);
                context.SaveChanges();
            }
        }
    }
}
