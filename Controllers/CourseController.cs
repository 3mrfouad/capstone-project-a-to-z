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
        public static void CreateCourseByCohortId(string cohortId,string instructorId,string name,string description,
            string durationHrs,string resourcesLink,string startDate,string endDate)
        {
            float parsedDurationHrs =0;
            int parsedCohortId = 0;
            int parsedInstructorId =0;
            DateTime parsedStartDate = new DateTime();
            DateTime parsedEndDate = new DateTime();

            #region Validation
            ValidationException exception = new ValidationException();
            
            cohortId = (string.IsNullOrEmpty(cohortId) || string.IsNullOrWhiteSpace(cohortId)) ? null : cohortId.Trim();
            instructorId = (string.IsNullOrEmpty(instructorId) || string.IsNullOrWhiteSpace(instructorId)) ? null : instructorId.Trim();
            name = (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name)) ? null : name.Trim();
            description = (string.IsNullOrEmpty(description) || string.IsNullOrWhiteSpace(description)) ? null : description.Trim();
            durationHrs = (string.IsNullOrEmpty(durationHrs) || string.IsNullOrWhiteSpace(durationHrs)) ? null : durationHrs.Trim();
            resourcesLink = (string.IsNullOrEmpty(resourcesLink) || string.IsNullOrWhiteSpace(resourcesLink)) ? null : resourcesLink.Trim();
            startDate = (string.IsNullOrEmpty(startDate) || string.IsNullOrWhiteSpace(startDate)) ? null : startDate.Trim();
            endDate = (string.IsNullOrEmpty(endDate) || string.IsNullOrWhiteSpace(endDate)) ? null : endDate.Trim();

            using var context = new AppDbContext();

            if (string.IsNullOrWhiteSpace(cohortId))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(cohortId), nameof(cohortId) + " is null."));
            }
            else
            {
                if (!int.TryParse(cohortId, out parsedCohortId))
                {
                    exception.ValidationExceptions.Add(new Exception("Invalid value for Cohort Id"));
                }
                else if (!context.Cohorts.Any(key => key.CohortId == parsedCohortId))
                {
                    exception.ValidationExceptions.Add(new Exception("Cohort Id does not exist"));
                }
            }
            if (string.IsNullOrWhiteSpace(instructorId))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(instructorId), nameof(instructorId) + " is null."));
            }
            else
            {
                if (!int.TryParse(instructorId, out parsedInstructorId))
                {
                    exception.ValidationExceptions.Add(new Exception("Invalid value for Instructor Id"));
                }
                else if (!context.Users.Any(key => key.UserId == parsedInstructorId && key.IsInstructor == true))
                {
                    exception.ValidationExceptions.Add(new Exception("Instructor Id does not exist"));
                }
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
                    if (string.IsNullOrEmpty(cohortId) && int.TryParse(cohortId, out parsedCohortId) && (context.Courses.Any(key => key.Name.ToLower() == name.ToLower() && key.Archive == false)))
                    {
                        int matchingCourseId = context.Courses
                            .SingleOrDefault(key => key.Name.ToLower() == name.ToLower() && key.Archive == false).CourseId;
                        if (context.CohortCourses.Any(key =>
                            key.CohortId == parsedCohortId && key.CourseId == matchingCourseId))
                        {
                            exception.ValidationExceptions.Add(new Exception("Course with this name already exists for this cohort "));
                        }
                    }
                }
            }
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
            if (string.IsNullOrWhiteSpace(resourcesLink))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(resourcesLink), nameof(resourcesLink) + " is null."));
            }
            else
            {
                if (resourcesLink.Length > 250)
                {
                    exception.ValidationExceptions.Add(new Exception("ResourcesLink can only be 250 characters long."));
                }
                else
                {
                    /** Citation
                     *  https://stackoverflow.com/questions/161738/what-is-the-best-regular-expression-to-check-if-a-string-is-a-valid-url
                     *  Referenced above source to validate the incoming Resources Link (URL) bafire saving to DB.
                     */
                    Uri uri;
                    if(!(Uri.TryCreate(resourcesLink, UriKind.Absolute, out uri) && 
                       (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps || uri.Scheme == Uri.UriSchemeFtp)))
                    {
                        exception.ValidationExceptions.Add(new Exception("ResourcesLink is not valid."));
                    }
                    /*End Citation*/
                }
            }
                
            if (string.IsNullOrWhiteSpace(startDate))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(startDate), nameof(startDate) + " is null."));
            }
            else
            {
                if (!DateTime.TryParse(startDate, out parsedStartDate))
                {
                    exception.ValidationExceptions.Add(new Exception("Invalid value for startDate"));
                }
                else if (parsedStartDate < DateTime.Now.Date)
                {
                    exception.ValidationExceptions.Add(new Exception("This Course can not have start date in the past."));
                }
            }
            if (string.IsNullOrWhiteSpace(endDate))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(endDate), nameof(endDate) + " is null."));
            }
            else
            {
                if (!DateTime.TryParse(endDate, out parsedEndDate))
                {
                    exception.ValidationExceptions.Add(new Exception("Invalid value for endDate"));
                }
                else if (parsedEndDate < DateTime.Now.Date)
                {
                    exception.ValidationExceptions.Add(new Exception("This Course can not have end date in the past."));
                }
            }
            /* Business Logic*/
            if ((DateTime.TryParse(startDate, out parsedStartDate) && DateTime.TryParse(endDate, out parsedEndDate)))
            {
                if (parsedEndDate < parsedStartDate)
                {
                    exception.ValidationExceptions.Add(new Exception("End date can not be before Start date."));
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

            #endregion

            var newCourse = new Course
            {
                /*  Create a Course*/
                InstructorId = parsedInstructorId,
                Name = name,
                Description = description,
                DurationHrs = parsedDurationHrs,
                ResourcesLink = resourcesLink
            };

            context.Courses.Add(newCourse);
            context.SaveChanges();
            /*Creates a Join between Course and Cohort by Creating an object*/
            var newCohortCourse = new CohortCourse
            {
                CohortId = parsedCohortId,
                CourseId = newCourse.CourseId,
                StartDate = parsedStartDate,
                EndDate = parsedEndDate
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
        public static void AssignCourseByCohortId(string cohortId, string courseId, string startDate, string endDate)
        {
            var parsedCohortId = int.Parse(cohortId);
            var parsedCourseId = int.Parse(courseId);
            var parsedStartDate = DateTime.Parse(startDate);
            var parsedEndDate = DateTime.Parse(endDate);
            using var context = new AppDbContext();
            var AddCourseByCohortId = new CohortCourse
            {
                CohortId = parsedCohortId,
                CourseId = parsedCourseId,
                StartDate = parsedStartDate,
                EndDate = parsedEndDate
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
        public static void UpdateCourseById(string courseId,string instructorId,string name,string description,
            string durationHrs,string resourcesLink)
        {
            var parsedCourseId = int.Parse(courseId);
            var parsedInstructorId = int.Parse(instructorId);
            var parsedDurationHrs = float.Parse(durationHrs);
            using var context = new AppDbContext();
            {
                var course = context.Courses.SingleOrDefault(key => key.CourseId==parsedCourseId);

                course.InstructorId=parsedInstructorId;
                course.Name=name;
                course.Description=description;
                course.DurationHrs=parsedDurationHrs;
                course.ResourcesLink=resourcesLink;
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
                        .Any(subKey => subKey.CohortId==parsedCohortId))
                    .ToList();

            return coursesListByCohortId;
        }
    }
}