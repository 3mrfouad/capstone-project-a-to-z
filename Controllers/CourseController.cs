using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AZLearn.Data;
using AZLearn.Models;
using AZLearn.Models.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AZLearn.Controllers
{
    public class CourseController : ControllerBase
    {
        /// <summary>
        ///     CreateCourse
        ///     Description: Controller action that creates new course
        ///     It expects below parameters, and would populate the same as new course in the database
        /// </summary>
        /// <param name="name">string provided from frontend</param>
        /// <param name="description">string provided from frontend</param>
        /// <param name="durationHrs">string provided from frontend, and parsed to float to match model property data type</param>
        public static void CreateCourse(string name, string description, string durationHrs)
        {
            float parsedDurationHrs = 0;
            using var context = new AppDbContext();
            var exception = new ValidationException();

            #region Validation

            name = string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name) ? null : name.Trim().ToLower();
            description = string.IsNullOrEmpty(description) || string.IsNullOrWhiteSpace(description)
                ? null
                : description.Trim();
            durationHrs = string.IsNullOrEmpty(durationHrs) || string.IsNullOrWhiteSpace(durationHrs)
                ? null
                : durationHrs.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(name), nameof(name) + " is null."));
            }
            else
            {
                if (name.Length > 50)
                    exception.ValidationExceptions.Add(new Exception("Course name can only be 50 characters long."));
                else if (context.Courses.Any(key => key.Name.ToLower() == name.ToLower() && key.Archive == false))
                    exception.ValidationExceptions.Add(new Exception("Course with this name already exists."));
            }

            if (string.IsNullOrWhiteSpace(description))
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(description),
                    nameof(description) + " is null."));
            else if (description.Length > 250)
                exception.ValidationExceptions.Add(
                    new Exception("Course description can only be 250 characters long."));
            if (string.IsNullOrWhiteSpace(durationHrs))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(durationHrs),
                    nameof(durationHrs) + " is null."));
            }
            else
            {
                if (!float.TryParse(durationHrs, out parsedDurationHrs))
                    exception.ValidationExceptions.Add(new Exception("Invalid value for durationHrs"));
                else if (parsedDurationHrs > 999.99 || parsedDurationHrs < 0)
                    exception.ValidationExceptions.Add(
                        new Exception("durationHrs value should be between 0 & 999.99 inclusive."));
            }

            if (exception.ValidationExceptions.Count > 0) throw exception;

            #endregion

            description = description.ToLower();
            var newCourse = new Course
            {
                Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name),
                Description = char.ToUpper(description[0]) + description.Substring(1),
                DurationHrs = parsedDurationHrs
            };

            context.Courses.Add(newCourse);

            context.SaveChanges();
        }

        /// <summary>
        ///     Update a CourseById
        ///     Description: Controller action that updates existing course by courseId
        ///     It expects below parameters, and would populate the course by cohort id in the database.
        ///     Assumption:
        ///     The frontend view would populate the course information first through API call
        ///     User will edit as needed
        ///     Frontend will send update API call to backend with all keys to update database
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="name">>string provided from frontend</param>
        /// <param name="description">string provided from frontend</param>
        /// <param name="durationHrs">>string provided from frontend,, and parsed to float to match model property data type </param>
        public static void UpdateCourseById(string courseId, string name, string description,
            string durationHrs)
        {
            var parsedCourseId = 0;
            float parsedDurationHrs = 0;
            using var context = new AppDbContext();
            var exception = new ValidationException();

            #region Validation

            courseId = string.IsNullOrEmpty(courseId) || string.IsNullOrWhiteSpace(courseId) ? null : courseId.Trim();
            name = string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name) ? null : name.Trim().ToLower();
            description = string.IsNullOrEmpty(description) || string.IsNullOrWhiteSpace(description)
                ? null
                : description.Trim();
            durationHrs = string.IsNullOrEmpty(durationHrs) || string.IsNullOrWhiteSpace(durationHrs)
                ? null
                : durationHrs.Trim();

            if (string.IsNullOrWhiteSpace(courseId))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(courseId),
                    nameof(courseId) + " is null."));
            }
            else
            {
                if (!int.TryParse(courseId, out parsedCourseId))
                    exception.ValidationExceptions.Add(new Exception("Invalid value for courseId"));
                if (parsedCourseId > 2147483647 || parsedCourseId < 1)
                    exception.ValidationExceptions.Add(
                        new Exception("courseId value should be between 1 & 2147483647 inclusive"));
                else if (!context.Courses.Any(key => key.CourseId == parsedCourseId))
                    exception.ValidationExceptions.Add(new Exception("courseId does not exist"));
                else if (!context.Courses.Any(key => key.CourseId == parsedCourseId && key.Archive == false))
                    exception.ValidationExceptions.Add(new Exception("Course is archived"));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(name), nameof(name) + " is null."));
            }
            else
            {
                if (name.Length > 50)
                {
                    exception.ValidationExceptions.Add(new Exception("Course name can only be 50 characters long."));
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(courseId) && int.TryParse(courseId, out parsedCourseId))
                        /* Two courses with same name should not be allowed */
                        if (context.Courses.Any(key =>
                            key.Name.ToLower() == name.ToLower() && key.CourseId != parsedCourseId))
                            exception.ValidationExceptions.Add(
                                new Exception("A Course with this name already exists."));
                }
            }

            if (string.IsNullOrWhiteSpace(description))
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(description),
                    nameof(description) + " is null."));
            else if (description.Length > 250)
                exception.ValidationExceptions.Add(
                    new Exception("Course description can only be 250 characters long."));
            if (string.IsNullOrWhiteSpace(durationHrs))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(durationHrs),
                    nameof(durationHrs) + " is null."));
            }
            else
            {
                if (!float.TryParse(durationHrs, out parsedDurationHrs))
                    exception.ValidationExceptions.Add(new Exception("Invalid value for durationHrs"));
                else if (parsedDurationHrs > 999.99 || parsedDurationHrs < 0)
                    exception.ValidationExceptions.Add(
                        new Exception("durationHrs value should be between 0 & 999.99 inclusive."));
            }

            if (exception.ValidationExceptions.Count > 0) throw exception;

            #endregion

            description = description.ToLower();
            var course = context.Courses.SingleOrDefault(key => key.CourseId == parsedCourseId);
            course.Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name);
            course.Description = char.ToUpper(description[0]) + description.Substring(1);
            course.DurationHrs = parsedDurationHrs;

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
        ///     It expects below parameters, and would retrieve courses list from the database.
        /// </summary>
        /// <param name="cohortId"></param>
        /// <returns>List of Courses by Cohort Id</returns>
        public static List<Course> GetCoursesByCohortId(string cohortId)
        {
            var parsedCohortId = 0;
            using var context = new AppDbContext();
            var exception = new ValidationException();

            #region Validation

            if (string.IsNullOrWhiteSpace(cohortId))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(cohortId),
                    nameof(cohortId) + " is null."));
            }
            else
            {
                if (!int.TryParse(cohortId, out parsedCohortId))
                    exception.ValidationExceptions.Add(new Exception("Invalid value for cohortId"));
                if (parsedCohortId > 2147483647 || parsedCohortId < 1)
                    exception.ValidationExceptions.Add(
                        new Exception("cohortId value should be between 1 & 2147483647 inclusive"));
                else if (!context.Cohorts.Any(key => key.CohortId == parsedCohortId))
                    exception.ValidationExceptions.Add(new Exception("cohortId does not exist"));
            }

            if (exception.ValidationExceptions.Count > 0) throw exception;

            #endregion

            /*Retrieve all list of courses of specific Cohort by Filtering it by CohortId*/
            var coursesListByCohortId =
                context.Courses.Include(key => key.CohortCourses)
                    .Where(key => key.CohortCourses
                        .Any(subKey => subKey.CohortId == parsedCohortId)).ToList();

            return coursesListByCohortId;
        }

        /// <summary>
        ///     GetCourseByCohortId
        ///     Description: Controller action that returns a Courses by CohortId
        ///     It expects below parameters, and would return a course by cohort id from the database.
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="cohortId"></param>
        /// <returns></returns>
        public static Course GetCourseByCohortId(string courseId, string cohortId)
        {
            var parsedCohortId = 0;
            var parsedCourseId = 0;
            using var context = new AppDbContext();
            var exception = new ValidationException();

            #region Validation

            if (string.IsNullOrWhiteSpace(cohortId))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(cohortId),
                    nameof(cohortId) + " is null."));
            }
            else
            {
                if (!int.TryParse(cohortId, out parsedCohortId))
                    exception.ValidationExceptions.Add(new Exception("Invalid value for cohortId"));
                if (parsedCohortId > 2147483647 || parsedCohortId < 1)
                    exception.ValidationExceptions.Add(
                        new Exception("cohortId value should be between 1 & 2147483647 inclusive"));
                else if (!context.Cohorts.Any(key => key.CohortId == parsedCohortId))
                    exception.ValidationExceptions.Add(new Exception("cohortId does not exist"));
            }

            if (string.IsNullOrWhiteSpace(courseId))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(courseId),
                    nameof(courseId) + " is null."));
            }
            else
            {
                if (!int.TryParse(courseId, out parsedCourseId))
                    exception.ValidationExceptions.Add(new Exception("Invalid value for courseId"));
                if (parsedCourseId > 2147483647 || parsedCourseId < 1)
                    exception.ValidationExceptions.Add(
                        new Exception("courseId value should be between 1 & 2147483647 inclusive"));
                else if (!context.Courses.Any(key => key.CourseId == parsedCourseId))
                    exception.ValidationExceptions.Add(new Exception("courseId does not exist"));
            }

            if (exception.ValidationExceptions.Count > 0) throw exception;

            #endregion

            var courseByCohortId =
                context.Courses.Include(key => key.CohortCourses).SingleOrDefault(key =>
                    key.CohortCourses.Any(subKey =>
                        subKey.CohortId == parsedCohortId && subKey.CourseId == parsedCourseId));
            return courseByCohortId;
        }

        /// <summary>
        ///     This Action takes in CourseId and returns respective Course record
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns>Single Course record</returns>
        public static Course GetCourseById(string courseId)
        {
            var parsedCourseId = 0;
            var exception = new ValidationException();
            using var context = new AppDbContext();

            #region Validation

            courseId = string.IsNullOrEmpty(courseId) || string.IsNullOrWhiteSpace(courseId) ? null : courseId.Trim();

            if (courseId == null)
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(courseId),
                    nameof(courseId) + " is null."));
            }
            else
            {
                if (!int.TryParse(courseId, out parsedCourseId))
                    exception.ValidationExceptions.Add(new Exception("Invalid value for courseId"));
                if (parsedCourseId > 2147483647 || parsedCourseId < 1)
                    exception.ValidationExceptions.Add(
                        new Exception("courseId value should be between 1 & 2147483647 inclusive"));
                else if (!context.Courses.Any(key => key.CourseId == parsedCourseId))
                    exception.ValidationExceptions.Add(new Exception("courseId does not exist"));
            }

            if (exception.ValidationExceptions.Count > 0) throw exception;

            #endregion

            return context.Courses.SingleOrDefault(key => key.CourseId == parsedCourseId);
        }

        /// <summary>
        ///     ArchiveCourseById
        ///     Description: This action archives a course by courseId PK
        /// </summary>
        /// <param name="courseId"></param>
        public static void ArchiveCourseById(string courseId)
        {
            var parsedCourseId = 0;
            var exception = new ValidationException();
            using var context = new AppDbContext();

            #region Validation

            courseId = string.IsNullOrEmpty(courseId) || string.IsNullOrWhiteSpace(courseId) ? null : courseId.Trim();

            if (courseId == null)
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(courseId),
                    nameof(courseId) + " is null."));
            }
            else
            {
                if (!int.TryParse(courseId, out parsedCourseId))
                    exception.ValidationExceptions.Add(new Exception("Invalid value for courseId"));
                if (parsedCourseId > 2147483647 || parsedCourseId < 1)
                    exception.ValidationExceptions.Add(
                        new Exception("courseId value should be between 1 & 2147483647 inclusive"));
                else if (!context.Courses.Any(key => key.CourseId == parsedCourseId))
                    exception.ValidationExceptions.Add(new Exception("courseId does not exist"));
                else if (!context.Courses.Any(key => key.CourseId == parsedCourseId && key.Archive == false))
                    exception.ValidationExceptions.Add(new Exception("Course is already archived"));
            }

            if (exception.ValidationExceptions.Count > 0) throw exception;

            #endregion

            var homeworks = context.Homeworks.Where(key => key.CourseId == parsedCourseId).ToList();

            foreach (var homework in homeworks)
            {
                var rubrics = context.Rubrics.Where(key => key.HomeworkId == homework.HomeworkId).ToList();
                foreach (var rubric in rubrics)
                {
                    var grades = context.Grades.Where(key => key.RubricId == rubric.RubricId).ToList();
                    foreach (var grade in grades) grade.Archive = true;

                    rubric.Archive = true;
                }

                var timesheets = context.Timesheets.Where(key => key.HomeworkId == homework.HomeworkId).ToList();
                foreach (var timesheet in timesheets) timesheet.Archive = true;

                homework.Archive = true;
            }

            var assignedCourses = context.CohortCourses.Where(key => key.CourseId == parsedCourseId).ToList();
            foreach (var cohortCourse in assignedCourses) cohortCourse.Archive = true;

            var cohort = context.Courses.Find(parsedCourseId);
            cohort.Archive = true;

            context.SaveChanges();
        }
    }
}