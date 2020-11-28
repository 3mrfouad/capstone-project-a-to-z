using AZLearn.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AZLearn.Models;
using AZLearn.Models.Exceptions;

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
            int parsedCohortId = 0;
            int parsedCourseId = 0;
            int parsedInstructorId = 0;
            DateTime parsedStartDate = new DateTime();
            DateTime parsedEndDate = new DateTime();

            #region Validation
            ValidationException exception = new ValidationException();

            cohortId = (string.IsNullOrEmpty(cohortId) || string.IsNullOrWhiteSpace(cohortId)) ? null : cohortId.Trim();
            courseId = (string.IsNullOrEmpty(courseId) || string.IsNullOrWhiteSpace(courseId)) ? null : courseId.Trim();
            instructorId = (string.IsNullOrEmpty(instructorId) || string.IsNullOrWhiteSpace(instructorId)) ? null : instructorId.Trim();
            startDate = (string.IsNullOrEmpty(startDate) || string.IsNullOrWhiteSpace(startDate)) ? null : startDate.Trim();
            endDate = (string.IsNullOrEmpty(endDate) || string.IsNullOrWhiteSpace(endDate)) ? null : endDate.Trim();
            resourcesLink = (string.IsNullOrEmpty(resourcesLink) || string.IsNullOrWhiteSpace(resourcesLink)) ? null : resourcesLink.Trim();

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
                else if ( context.Cohorts.Any(key => key.CohortId==parsedCohortId&&key.Archive==true) )
                {
                    exception.ValidationExceptions.Add(new Exception("Cohort is archived"));
                }

            }
            if (string.IsNullOrWhiteSpace(courseId))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(courseId), nameof(courseId) + " is null."));
            }
            else
            {
                if (!int.TryParse(courseId, out parsedCourseId))
                {
                    exception.ValidationExceptions.Add(new Exception("Invalid value for Course Id"));
                }
                else
                {
                    if (!context.Courses.Any(key => key.CourseId == parsedCourseId))
                    {
                        exception.ValidationExceptions.Add(new Exception("Course Id does not exist"));
                    }
                    else if ( context.Courses.Any(key => key.CourseId==parsedCourseId&&key.Archive==true) )
                    {
                        exception.ValidationExceptions.Add(new Exception("Course is archived"));
                    }
                    else
                    {
                        if ((!string.IsNullOrWhiteSpace(cohortId)) && int.TryParse(cohortId, out parsedCohortId) && (context.CohortCourses.Any(key =>
                            key.CohortId == parsedCohortId && key.CourseId == parsedCourseId)))
                        {
                            exception.ValidationExceptions.Add(new Exception("Course is already assigned to this Cohort"));
                        }
                    }

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
            if (!string.IsNullOrWhiteSpace(resourcesLink))
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
                    if (!(Uri.TryCreate(resourcesLink, UriKind.Absolute, out uri) &&
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
            else if (!DateTime.TryParse(startDate, out parsedStartDate))
            {
                exception.ValidationExceptions.Add(new Exception("Invalid value for startDate"));
            }
            if (string.IsNullOrWhiteSpace(endDate))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(endDate), nameof(endDate) + " is null."));
            }
            else if (!DateTime.TryParse(endDate, out parsedEndDate))
            {
                exception.ValidationExceptions.Add(new Exception("Invalid value for endDate"));
            }
            /* Business Logic*/
            if ((DateTime.TryParse(startDate, out parsedStartDate) && DateTime.TryParse(endDate, out parsedEndDate)))
            {
                if (parsedEndDate < parsedStartDate)
                {
                    exception.ValidationExceptions.Add(new Exception("End date can not be before Start date."));
                }
            }
            if (exception.ValidationExceptions.Count > 0)
            {
                throw exception;
            }

            #endregion

            var AddCourseByCohortId = new CohortCourse
            {
                CohortId = parsedCohortId,
                CourseId = parsedCourseId,
                InstructorId = parsedInstructorId,
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
            int parsedCohortId = 0;
            int parsedCourseId = 0;
            int parsedInstructorId = 0;
            DateTime parsedStartDate = new DateTime();
            DateTime parsedEndDate = new DateTime();

            #region Validation
            ValidationException exception = new ValidationException();

            cohortId = (string.IsNullOrEmpty(cohortId) || string.IsNullOrWhiteSpace(cohortId)) ? null : cohortId.Trim();
            courseId = (string.IsNullOrEmpty(courseId) || string.IsNullOrWhiteSpace(courseId)) ? null : courseId.Trim();
            instructorId = (string.IsNullOrEmpty(instructorId) || string.IsNullOrWhiteSpace(instructorId)) ? null : instructorId.Trim();
            startDate = (string.IsNullOrEmpty(startDate) || string.IsNullOrWhiteSpace(startDate)) ? null : startDate.Trim();
            endDate = (string.IsNullOrEmpty(endDate) || string.IsNullOrWhiteSpace(endDate)) ? null : endDate.Trim();
            resourcesLink = (string.IsNullOrEmpty(resourcesLink) || string.IsNullOrWhiteSpace(resourcesLink)) ? null : resourcesLink.Trim();

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
                else if (!context.Cohorts.Any(key => key.CohortId == parsedCohortId && key.Archive == false))
                {
                    exception.ValidationExceptions.Add(new Exception("Cohort is archived"));
                }
            }
            if (string.IsNullOrWhiteSpace(courseId))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(courseId), nameof(courseId) + " is null."));
            }
            else
            {
                if (!int.TryParse(courseId, out parsedCourseId))
                {
                    exception.ValidationExceptions.Add(new Exception("Invalid value for Course Id"));
                }
                else if (!context.Courses.Any(key => key.CourseId == parsedCourseId))
                {
                    exception.ValidationExceptions.Add(new Exception("Course Id does not exist"));
                }
                else if (!context.Courses.Any(key => key.CourseId == parsedCourseId && key.Archive == false))
                {
                    exception.ValidationExceptions.Add(new Exception("Course is archived"));
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
                else if (!context.Users.Any(key => key.UserId == parsedInstructorId && key.IsInstructor == true && key.Archive == false))
                {
                    exception.ValidationExceptions.Add(new Exception("Instructor is archived"));
                }
            }
            if (!string.IsNullOrWhiteSpace(resourcesLink))
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
                    if (!(Uri.TryCreate(resourcesLink, UriKind.Absolute, out uri) &&
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
            else if (!DateTime.TryParse(startDate, out parsedStartDate))
            {
                exception.ValidationExceptions.Add(new Exception("Invalid value for startDate"));
            }
            if (string.IsNullOrWhiteSpace(endDate))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(endDate), nameof(endDate) + " is null."));
            }
            else if (!DateTime.TryParse(endDate, out parsedEndDate))
            {
                exception.ValidationExceptions.Add(new Exception("Invalid value for endDate"));
            }
            /* Business Logic*/
            if ((DateTime.TryParse(startDate, out parsedStartDate) && DateTime.TryParse(endDate, out parsedEndDate)))
            {
                if (parsedEndDate < parsedStartDate)
                {
                    exception.ValidationExceptions.Add(new Exception("End date can not be before Start date."));
                }
            }
            if (exception.ValidationExceptions.Count > 0)
            {
                throw exception;
            }

            #endregion

            var course = context.CohortCourses.Find(parsedCohortId, parsedCourseId);

            course.CohortId = parsedCohortId;
            course.CourseId = parsedCourseId;
            course.InstructorId = parsedInstructorId;
            course.StartDate = parsedStartDate;
            course.EndDate = parsedEndDate;
            course.ResourcesLink = resourcesLink;

            context.SaveChanges();
        }
    }
}
