using AZLearn.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AZLearn.Models;

namespace AZLearn.Controllers
{
    public class CohortCourseController : Controller
    {
        /// <summary>
        ///     AssignCourseByCohortId
        ///     Description: Controller action that creates/assigns the Course by CohortId
        ///     It expects below parameters, and would populate the course by cohort id in the database.
        /// </summary>
        /// <param name="cohortId"></param>
        /// <param name="courseId"></param>
        public static void AssignCourseByCohortId(string cohortId, string courseId, string instructorId, string startDate, string endDate, string resourcesLink)
        {
            var parsedCohortId = int.Parse(cohortId);
            var parsedCourseId = int.Parse(courseId);
            var parsedinstructorId = int.Parse(instructorId);
            var parsedStartDate = DateTime.Parse(startDate);
            var parsedEndDate = DateTime.Parse(endDate);
            using var context = new AppDbContext();
            var AddCourseByCohortId = new CohortCourse
            {
                CohortId = parsedCohortId,
                CourseId = parsedCourseId,
                InstructorId = parsedinstructorId,
                StartDate = parsedStartDate,
                EndDate = parsedEndDate,
                ResourcesLink = resourcesLink
            };
            context.CohortCourses.Add(AddCourseByCohortId);
            context.SaveChanges();
        }

        /// <summary>
        /// UpdateAssignedCourse
        /// Description: This action updates a cohort assigned course details
        /// </summary>
        /// <param name="cohortId"></param>
        /// <param name="courseId"></param>
        /// <param name="instructorId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="resourcesLink"></param>
        public static void UpdateAssignedCourse(string cohortId, string courseId, string instructorId, string startDate, string endDate, string resourcesLink)
        {
            var parsedCohortId = int.Parse(cohortId);
            var parsedCourseId = int.Parse(courseId);
            var parsedinstructorId = int.Parse(instructorId);
            var parsedStartDate = DateTime.Parse(startDate);
            var parsedEndDate = DateTime.Parse(endDate);

            using var context = new AppDbContext();
            var course = context.CohortCourses.Find(parsedCohortId, parsedCourseId);

            course.CohortId = parsedCohortId;
            course.CourseId = parsedCourseId;
            course.InstructorId = parsedinstructorId;
            course.StartDate = parsedStartDate;
            course.EndDate = parsedEndDate;
            course.ResourcesLink = resourcesLink;

            context.SaveChanges();
        }
    }
}
