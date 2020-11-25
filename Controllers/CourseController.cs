using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using AZLearn.Data;
using AZLearn.Models;
using AZLearn.Models.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AZLearn.Controllers
{
    public class CourseController :ControllerBase
    {
        /// <summary>
        ///     CreateCourse
        ///     Description: Controller action that creates new course
        ///     It expects below parameters, and would populate the same as new course in the database
        /// </summary>
        /// <param name="name">string provided from frontend</param>
        /// <param name="description">string provided from frontend</param>
        /// <param name="durationHrs">string provided from frontend, and parsed to float to match model property data type</param>
        public static void CreateCourse(string name, string description,
            string durationHrs)
        {
            var parsedDurationHrs = float.Parse(durationHrs);

            using var context = new AppDbContext();
            ValidationException exception = new ValidationException();
            
            if (string.IsNullOrWhiteSpace(description))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(description), nameof(description) + " is null."));
            }
            else if (description.Length > 250)
            {
                exception.ValidationExceptions.Add(new Exception("Course Description can only be 250 characters long."));
            }
            if (string.IsNullOrWhiteSpace(durationHrs))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(durationHrs), nameof(durationHrs) + " is null."));
            }
            else
            {
                if (!float.TryParse(durationHrs, out parsedDurationHrs))
                {
                    exception.ValidationExceptions.Add(new Exception("Invalid value for DurationHrs"));
                }
                else if (parsedDurationHrs > 999.99 || parsedDurationHrs < 0)
                {
                    exception.ValidationExceptions.Add(new Exception("DurationHrs value should be between 0 & 999.99 inclusive."));
                }
            }
            
           
            //Cohort cohortExists = context.Cohorts.Include(key => key.CohortCourses).SingleOrDefault(key => key.CohortId == parsedCohortId);
           /* if (cohortExists != null)
            {
                if (context.Cohorts.Include(key => key.CohortCourses).SingleOrDefault(key => key.CohortId == parsedCohortId).CohortCourses.Any(key => key.Course.Name.ToLower() == name.ToLower()))
                {
                    exception.ValidationExceptions.Add(
                        new Exception("Course with same name already exists for this cohort."));
                }
            }*/

           if (exception.ValidationExceptions.Count > 0)
            {
                throw exception;
            }

            var newCourse = new Course
            {
                /*  Create a Course*/
                Name = name,
                Description = description,
                DurationHrs = parsedDurationHrs,
            };

            context.Courses.Add(newCourse);
            context.SaveChanges();
        }

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
        public static void UpdateCourseById(string courseId,string name,string description,
            string durationHrs)
        {
            var parsedCourseId = int.Parse(courseId);
            var parsedDurationHrs = float.Parse(durationHrs);
            using var context = new AppDbContext();
            {
                var course = context.Courses.SingleOrDefault(key => key.CourseId==parsedCourseId);
                course.Name=name;
                course.Description=description;
                course.DurationHrs=parsedDurationHrs;
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
        public static List<Course> GetCoursesByCohortId(string cohortId, string includeInactive)
        {
            var parsedCohortId = int.Parse(cohortId);
            var parsedIncludeInactive = bool.Parse(includeInactive);
            using var context = new AppDbContext();

            /*Retrieve all list of courses of specific Cohort by Filtering it by CohortId*/
            //includeInactive - false - active courses
            //includeInactive - true - inactive courses
            var coursesListByCohortId =
                context.Courses.Where(key => key.Archive == parsedIncludeInactive).Include(key => key.CohortCourses)
                    .Where(key => key.CohortCourses
                        .Any(subKey => subKey.CohortId==parsedCohortId)).ToList();

            /* @Amr Fouad, demo on how to get to the instructor name on same above context call
             foreach (var course in coursesListByCohortId)
            {
                var name = course.CohortCourses.Where(key => key.CourseId == course.CourseId).SingleOrDefault().Instructor.Name;
            }*/
            return coursesListByCohortId;
        }
    }
}