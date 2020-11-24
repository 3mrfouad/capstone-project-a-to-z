using System;
using System.Collections.Generic;
using System.Linq;
using AZLearn.Data;
using AZLearn.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AZLearn.Controllers
{
    public class CourseController : ControllerBase
    {
        /// <summary>
        ///     CreateCourseByCohortId
        ///     Description: Controller action that creates new course by CohortId
        ///     It expects below parameters, and would populate the same as new course in the database
        /// </summary>
        /// <param name="cohortId"></param>
        /// <param name="instructorId"></param>
        /// <param name="name">string provided from frontend</param>
        /// <param name="description">string provided from frontend</param>
        /// <param name="durationHrs">string provided from frontend, and parsed to float to match model property data type</param>
        /// <param name="resourcesLink">string provided from frontend</param>
        /// <param name="startDate">string provided from frontend, and parsed to DateTime to match model property data type</param>
        /// <param name="endDate">string provided from frontend, and parsed to DateTime to match model property data type</param>
        public static void CreateCourseByCohortId(string cohortId, string instructorId, string name, string description,
            string durationHrs, string resourcesLink, string startDate, string endDate)
        {
          /*  int parsedCohortId;
            int parsedInstructorId;
            if ( string.IsNullOrWhiteSpace(cohortId)&&string.IsNullOrWhiteSpace(instructorId) )
            {
                throw new ArgumentNullException(nameof(cohortId)+" is null.");
            }
            if ( !int.TryParse(cohortId,out parsedCohortId) )
            {
                throw new ArgumentException(nameof(cohortId)+" is not valid.",nameof(cohortId));
            }*/

            using var context = new AppDbContext();
            var newCourse = new Course
            {
                /*  Create a Course*/
                InstructorId = int.Parse(instructorId),
                Name = name,
                Description = description,
                DurationHrs = float.Parse(durationHrs),
                ResourcesLink = resourcesLink
            };

            context.Courses.Add(newCourse);
            context.SaveChanges();

            /*Creates a Join between Course and Cohort by Creating an object*/

            var newCohortCourse = new CohortCourse
            {
                CohortId = int.Parse(cohortId),
                CourseId = newCourse.CourseId,
                StartDate = DateTime.Parse(startDate),
                EndDate = DateTime.Parse(endDate)
            };
            context.CohortCourses.Add(newCohortCourse);

            context.SaveChanges();
        }

        /// <summary>
        ///     AssignCourseByCohortId
        ///     Description: Controller action that creates/assigns the Course by CohortId
        ///     It expects below parameters, and would populate the course by cohort id in the database.
        /// </summary>
        /// <param name="cohortId"></param>
        /// <param name="courseId"></param>
        public static void AssignCourseByCohortId(string cohortId, string courseId)
        {
            using var context = new AppDbContext();
            var AddCourseByCohortId = new CohortCourse
            {
                CohortId = int.Parse(cohortId),
                CourseId = int.Parse(courseId)
            };
            context.CohortCourses.Add(AddCourseByCohortId);
            context.SaveChanges();
        }

        /// <summary>
        ///     Update a Course CourseById
        ///     Description: Controller action that updates existing course by courseId
        ///     It expects below parameters, and would populate the course by cohort id in the database.
        ///     Assumption:
        ///     The frontend view would populate the course information first through API call
        ///     User will edit as needed
        ///     Frontend will send update API call to backend with all keys to update database
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="instructorId"></param>
        /// <param name="name">>string provided from frontend</param>
        /// <param name="description">string provided from frontend</param>
        /// <param name="durationHrs">>string provided from frontend,, and parsed to float to match model property data type </param>
        /// <param name="resourcesLink">string provided from frontend</param>
        public static void UpdateCourseById(string courseId, string instructorId, string name, string description,
            string durationHrs, string resourcesLink)
        {
            var parsedCourseId = int.Parse(courseId);
            using var context = new AppDbContext();
            {
                var course = context.Courses.SingleOrDefault(key => key.CourseId == parsedCourseId);

                course.InstructorId = int.Parse(instructorId);
                course.Name = name;
                course.Description = description;
                course.DurationHrs = float.Parse(durationHrs);
                course.ResourcesLink = resourcesLink;
            }
            context.SaveChanges();
        }

        /// <summary>
        ///     Get Courses
        ///     Description: Controller action that returns list of existing courses
        /// </summary>
        /// <returns>List  of Courses</returns>
        public static List<Course> GetCourses()
        {
            using var context = new AppDbContext();
            var courses = context.Courses.ToList();
            return courses;
        }

        /// <summary>
        ///     GetCoursesByCohortId
        ///     Description: Controller action that returns list of existing coursesByCohortId
        ///     It expects below parameters, and would populate the course by cohort id in the database.
        /// </summary>
        /// <param name="cohortId"></param>
        /// <returns>List of Courses by Cohort Id</returns>
        public static List<Course> GetCoursesByCohortId(string cohortId)
        {
            var parsedCohortId = int.Parse(cohortId);
            using var context = new AppDbContext();

            /*Retrieve all list of courses of specific Cohort by Filtering it by CohortId*/

            var coursesListByCohortId =
                context.Courses.Include(key => key.CohortCourses)
                    .Where(key => key.CohortCourses
                        .Any(subKey => subKey.CohortId == parsedCohortId))
                    .ToList();

            return coursesListByCohortId;
        }
    }
}