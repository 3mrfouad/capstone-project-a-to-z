using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AZLearn.Data;
using AZLearn.Models;
using AZLearn.Models.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace AZLearn.Controllers
{
    public class HomeworkController : Controller
    {
        /// <summary>
        ///     GetHomeworksByCourseId
        ///     This action takes in Course Id and Cohort Id and returns List of Homeworks for specified course under the specified Cohort Id
        /// </summary>
        /// <param name="courseId">Course Id</param>
        /// <param name="cohortId">Cohort Id</param>
        /// <returns>List of Homeworks for specified course under the specified Cohort Id</returns>
        public static List<Homework> GetHomeworksByCourseId(string courseId, string cohortId)
        {
            int parsedCohortId = 0;
            int parsedCourseId = 0;

            #region Validation

            ValidationException exception = new ValidationException();

            courseId = (string.IsNullOrEmpty(courseId) || string.IsNullOrWhiteSpace(courseId)) ? null : courseId.Trim();
            cohortId = (string.IsNullOrEmpty(cohortId) || string.IsNullOrWhiteSpace(cohortId)) ? null : cohortId.Trim();

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
                if (parsedCohortId > 2147483647 || parsedCohortId < 1)
                {
                    exception.ValidationExceptions.Add(new Exception("Cohort Id value should be between 1 & 2147483647 inclusive"));
                }
                else if (!context.Cohorts.Any(key => key.CohortId == parsedCohortId))
                {
                    exception.ValidationExceptions.Add(new Exception("Cohort Id does not exist"));
                }
                else
                {
                    if (int.TryParse(courseId, out parsedCourseId))
                    {
                        if (!context.CohortCourses.Any(key => key.CohortId == parsedCohortId && key.CourseId == parsedCourseId))
                        {
                            exception.ValidationExceptions.Add(new Exception("This course is not assigned to this cohort."));
                        }
                    }
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
                if (parsedCourseId > 2147483647 || parsedCourseId < 1)
                {
                    exception.ValidationExceptions.Add(new Exception("Course Id value should be between 1 & 2147483647 inclusive"));
                }
                else if (!context.Courses.Any(key => key.CourseId == parsedCourseId))
                {
                    exception.ValidationExceptions.Add(new Exception("Course Id does not exist"));
                }
            }
            if (exception.ValidationExceptions.Count > 0)
            {
                throw exception;
            }

            #endregion

            List<Homework> homeworks;
            homeworks = context.Homeworks
                .Where(key => key.CourseId == parsedCourseId && key.CohortId == parsedCohortId).ToList();

            return homeworks;
        }

        /// <summary>
        ///     CreateHomeworkByCourseId
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
            int parsedCourseId = 0;
            int parsedInstructorId = 0;
            int parsedCohortId = 0;
            bool parsedIsAssignment = false;
            float parsedAvgCompletionTime = 0;
            DateTime parsedDueDate = new DateTime();
            DateTime parsedReleaseDate = new DateTime();

            #region Validation
            ValidationException exception = new ValidationException();

            courseId = (string.IsNullOrEmpty(courseId) || string.IsNullOrWhiteSpace(courseId)) ? null : courseId.Trim();
            instructorId = (string.IsNullOrEmpty(instructorId) || string.IsNullOrWhiteSpace(instructorId)) ? null : instructorId.Trim();
            cohortId = (string.IsNullOrEmpty(cohortId) || string.IsNullOrWhiteSpace(cohortId)) ? null : cohortId.Trim();
            isAssignment = (string.IsNullOrEmpty(isAssignment) || string.IsNullOrWhiteSpace(isAssignment)) ? null : isAssignment.Trim();
            title = (string.IsNullOrEmpty(title) || string.IsNullOrWhiteSpace(title)) ? null : title.Trim().ToLower();
            avgCompletionTime = (string.IsNullOrEmpty(avgCompletionTime) || string.IsNullOrWhiteSpace(avgCompletionTime)) ? null : avgCompletionTime.Trim();
            dueDate = (string.IsNullOrEmpty(dueDate) || string.IsNullOrWhiteSpace(dueDate)) ? null : dueDate.Trim();
            releaseDate = (string.IsNullOrEmpty(releaseDate) || string.IsNullOrWhiteSpace(releaseDate)) ? null : releaseDate.Trim();
            documentLink = (string.IsNullOrEmpty(documentLink) || string.IsNullOrWhiteSpace(documentLink)) ? null : documentLink.Trim().ToLower();
            gitHubClassRoomLink = (string.IsNullOrEmpty(gitHubClassRoomLink) || string.IsNullOrWhiteSpace(gitHubClassRoomLink)) ? null : gitHubClassRoomLink.Trim().ToLower();

            using var context = new AppDbContext();
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
                if (parsedCourseId > 2147483647 || parsedCourseId < 1)
                {
                    exception.ValidationExceptions.Add(new Exception("Course Id value should be between 1 & 2147483647 inclusive"));
                }
                else
                {
                    if (!context.Courses.Any(key => key.CourseId == parsedCourseId))
                    {
                        exception.ValidationExceptions.Add(new Exception("Course Id does not exist."));
                    }
                    else if (!context.Courses.Any(key => key.CourseId == parsedCourseId && key.Archive == false))
                    {
                        exception.ValidationExceptions.Add(new Exception("Course has been archived"));
                    }
                    else
                    {
                        if ((!string.IsNullOrWhiteSpace(cohortId)) && int.TryParse(cohortId, out parsedCohortId))
                        {
                            if (!context.CohortCourses.Any(key =>
                                key.CohortId == parsedCohortId && key.CourseId == parsedCourseId))
                            {
                                exception.ValidationExceptions.Add(new Exception("This course is not assigned to this Cohort"));
                            }
                            else if (!context.CohortCourses.Any(key =>
                                key.CohortId == parsedCohortId && key.CourseId == parsedCourseId &&
                                key.Archive == false))
                            {
                                exception.ValidationExceptions.Add(new Exception("This course has been archived for this cohort."));
                            }
                            else
                            {
                                if (context.Homeworks.Any(key =>
                                    key.CohortId == parsedCohortId && key.CourseId == parsedCourseId &&
                                    String.Equals(key.Title, title, StringComparison.CurrentCultureIgnoreCase)))
                                {
                                    exception.ValidationExceptions.Add(new Exception("Homework with same name already exists under this course for this cohort."));
                                }
                            }
                        }
                    }


                }
            }
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
                if (parsedCohortId > 2147483647 || parsedCohortId < 1)
                {
                    exception.ValidationExceptions.Add(new Exception("Cohort Id value should be between 1 & 2147483647 inclusive"));
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
                if (parsedInstructorId > 2147483647 || parsedInstructorId < 1)
                {
                    exception.ValidationExceptions.Add(new Exception("Instructor Id value should be between 1 & 2147483647 inclusive"));
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
            if (!string.IsNullOrWhiteSpace(isAssignment))
            {
                if (!bool.TryParse(isAssignment, out parsedIsAssignment))
                {
                    exception.ValidationExceptions.Add(new Exception("Invalid value for isAssignment."));
                }
            }
            if ( string.IsNullOrWhiteSpace(title))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(title),nameof(title)+" is null."));
            }
            else if ( title.Length>100 )
            {
                exception.ValidationExceptions.Add(new Exception("Homework Title can only be 100 characters long."));
            }
            if (!string.IsNullOrWhiteSpace(avgCompletionTime))
            {
                if (!float.TryParse(avgCompletionTime, out parsedAvgCompletionTime))
                {
                    exception.ValidationExceptions.Add(new Exception("Invalid value for average Completion time"));
                }
                else if (parsedAvgCompletionTime > 999.99 || parsedAvgCompletionTime < 0)
                {
                    exception.ValidationExceptions.Add(new Exception("Average Completion Time should be between 0 and 999.99 inclusive"));
                }
            }
            if (!string.IsNullOrWhiteSpace(releaseDate))
            {
                if (!DateTime.TryParse(releaseDate, out parsedReleaseDate))
                {
                    exception.ValidationExceptions.Add(new Exception("Invalid value for release date"));
                }
            }
            if (!string.IsNullOrWhiteSpace(dueDate))
            {
                if (!DateTime.TryParse(dueDate, out parsedDueDate))
                {
                    exception.ValidationExceptions.Add(new Exception("Invalid value for due date"));
                }
                else if (!string.IsNullOrWhiteSpace(releaseDate) &&
                         DateTime.TryParse(releaseDate, out parsedReleaseDate) && parsedReleaseDate > parsedDueDate)
                {
                    exception.ValidationExceptions.Add(new Exception("Homework can not be due before it is released."));
                }
            }
            if (!string.IsNullOrWhiteSpace(documentLink))
            {
                if (documentLink.Length > 250)
                {
                    exception.ValidationExceptions.Add(new Exception("Document Link can only be 250 characters long."));
                }
                else
                {
                    /** Citation
                     *  https://stackoverflow.com/questions/161738/what-is-the-best-regular-expression-to-check-if-a-string-is-a-valid-url
                     *  Referenced above source to validate the incoming Resources Link (URL) bafire saving to DB.
                     */
                    Uri uri;
                    if (!(Uri.TryCreate(documentLink, UriKind.Absolute, out uri) &&
                          (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps || uri.Scheme == Uri.UriSchemeFtp)))
                    {
                        exception.ValidationExceptions.Add(new Exception("Document Link is not valid."));
                    }
                    /*End Citation*/
                }
            }
            if (!string.IsNullOrWhiteSpace(gitHubClassRoomLink))
            {
                if (gitHubClassRoomLink.Length > 250)
                {
                    exception.ValidationExceptions.Add(new Exception("Github Classroom Link can only be 250 characters long."));
                }
                else
                {
                    /** Citation
                     *  https://stackoverflow.com/questions/161738/what-is-the-best-regular-expression-to-check-if-a-string-is-a-valid-url
                     *  Referenced above source to validate the incoming Resources Link (URL) bafire saving to DB.
                     */
                    Uri uri;
                    if (!(Uri.TryCreate(gitHubClassRoomLink, UriKind.Absolute, out uri) &&
                          (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps || uri.Scheme == Uri.UriSchemeFtp)))
                    {
                        exception.ValidationExceptions.Add(new Exception("Github Classroom Link is not valid."));
                    }
                    /*End Citation*/
                }
            }
            
            if (exception.ValidationExceptions.Count > 0)
            {
                throw exception;
            }

            #endregion

            if (dueDate != null && releaseDate != null)
            {
                var newHomework = new Homework
                {
                    CourseId = parsedCourseId,
                    InstructorId = parsedInstructorId,
                    CohortId = parsedCohortId,
                    IsAssignment = parsedIsAssignment,
                    Title = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(title),
                    AvgCompletionTime = parsedAvgCompletionTime,
                    DueDate = parsedDueDate,
                    ReleaseDate = parsedReleaseDate,
                    DocumentLink = documentLink,
                    GitHubClassRoomLink = gitHubClassRoomLink
                };
                context.Homeworks.Add(newHomework);
            }
            else if (dueDate == null && releaseDate == null)
            {
                var newHomework = new Homework
                {
                    CourseId = parsedCourseId,
                    InstructorId = parsedInstructorId,
                    CohortId = parsedCohortId,
                    IsAssignment = parsedIsAssignment,
                    Title = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(title),
                    AvgCompletionTime = parsedAvgCompletionTime,
                    DocumentLink = documentLink,
                    GitHubClassRoomLink = gitHubClassRoomLink
                };
                context.Homeworks.Add(newHomework);
            }
            else if (dueDate == null)
            {
                var newHomework = new Homework
                {
                    CourseId = parsedCourseId,
                    InstructorId = parsedInstructorId,
                    CohortId = parsedCohortId,
                    IsAssignment = parsedIsAssignment,
                    Title = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(title),
                    AvgCompletionTime = parsedAvgCompletionTime,
                    ReleaseDate = parsedReleaseDate,
                    DocumentLink = documentLink,
                    GitHubClassRoomLink = gitHubClassRoomLink
                };
                context.Homeworks.Add(newHomework);
            }
            else if (releaseDate == null)
            {
                var newHomework = new Homework
                {
                    CourseId = parsedCourseId,
                    InstructorId = parsedInstructorId,
                    CohortId = parsedCohortId,
                    IsAssignment = parsedIsAssignment,
                    Title = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(title),
                    AvgCompletionTime = parsedAvgCompletionTime,
                    DueDate = parsedDueDate,
                    DocumentLink = documentLink,
                    GitHubClassRoomLink = gitHubClassRoomLink
                };
                context.Homeworks.Add(newHomework);
            }

            context.SaveChanges();
        }

        /// <summary>
        ///     UpdateHomeworkById
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
        public static void UpdateHomeworkById(string homeworkId, string courseId, string instructorId, string cohortId, string isAssignment, string title, string avgCompletionTime, string dueDate, string releaseDate,
            string documentLink, string gitHubClassRoomLink)
        {
            int parsedHomeworkId = 0;
            int parsedCourseId = 0;
            int parsedInstructorId = 0;
            int parsedCohortId = 0;
            bool parsedIsAssignment = false;
            float parsedAvgCompletionTime = 0;
            DateTime parsedDueDate = new DateTime();
            DateTime parsedReleaseDate = new DateTime();

            #region Validation

            homeworkId = (string.IsNullOrEmpty(homeworkId) || string.IsNullOrWhiteSpace(homeworkId)) ? null : homeworkId.Trim();
            courseId = (string.IsNullOrEmpty(courseId) || string.IsNullOrWhiteSpace(courseId)) ? null : courseId.Trim();
            instructorId = (string.IsNullOrEmpty(instructorId) || string.IsNullOrWhiteSpace(instructorId)) ? null : instructorId.Trim();
            cohortId = (string.IsNullOrEmpty(cohortId) || string.IsNullOrWhiteSpace(cohortId)) ? null : cohortId.Trim();
            isAssignment = (string.IsNullOrEmpty(isAssignment) || string.IsNullOrWhiteSpace(isAssignment)) ? null : isAssignment.Trim();
            title = (string.IsNullOrEmpty(title) || string.IsNullOrWhiteSpace(title)) ? null : title.Trim().ToLower();
            avgCompletionTime = (string.IsNullOrEmpty(avgCompletionTime) || string.IsNullOrWhiteSpace(avgCompletionTime)) ? null : avgCompletionTime.Trim();
            dueDate = (string.IsNullOrEmpty(dueDate) || string.IsNullOrWhiteSpace(dueDate)) ? null : dueDate.Trim();
            releaseDate = (string.IsNullOrEmpty(releaseDate) || string.IsNullOrWhiteSpace(releaseDate)) ? null : releaseDate.Trim();
            documentLink = (string.IsNullOrEmpty(documentLink) || string.IsNullOrWhiteSpace(documentLink)) ? null : documentLink.Trim().ToLower();
            gitHubClassRoomLink = (string.IsNullOrEmpty(gitHubClassRoomLink) || string.IsNullOrWhiteSpace(gitHubClassRoomLink)) ? null : gitHubClassRoomLink.Trim().ToLower();

            ValidationException exception = new ValidationException();
            using var context = new AppDbContext();

            if (string.IsNullOrWhiteSpace(homeworkId))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(homeworkId), nameof(homeworkId) + " is null."));
            }
            else
            {
                if (!int.TryParse(homeworkId, out parsedHomeworkId))
                {
                    exception.ValidationExceptions.Add(new Exception("Invalid value for Homework Id"));
                }
                if (parsedHomeworkId > 2147483647 || parsedHomeworkId < 1)
                {
                    exception.ValidationExceptions.Add(new Exception("Homework Id value should be between 1 & 2147483647 inclusive"));
                }
                else if (!context.Homeworks.Any(key => key.HomeworkId == parsedHomeworkId))
                {
                    exception.ValidationExceptions.Add(new Exception("Homework Id does not exist"));
                }
                else if (!context.Homeworks.Any(key => key.HomeworkId == parsedHomeworkId && key.Archive == false))
                {
                    exception.ValidationExceptions.Add(new Exception("Homework is archived"));
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
                if (parsedCourseId > 2147483647 || parsedCourseId < 1)
                {
                    exception.ValidationExceptions.Add(new Exception("Course Id value should be between 1 & 2147483647 inclusive"));
                }
                else
                {
                    if (!context.Courses.Any(key => key.CourseId == parsedCourseId))
                    {
                        exception.ValidationExceptions.Add(new Exception("Course Id does not exist"));
                    }
                    else
                    {
                        if (!context.Courses.Any(key => key.CourseId == parsedCourseId && key.Archive == false))
                        {
                            exception.ValidationExceptions.Add(new Exception("Course has been archived"));
                        }
                        else if ((!string.IsNullOrWhiteSpace(cohortId)) && int.TryParse(cohortId, out parsedCohortId))
                        {
                            if (!context.CohortCourses.Any(key =>
                                key.CohortId == parsedCohortId && key.CourseId == parsedCourseId))
                            {
                                exception.ValidationExceptions.Add(new Exception("This course is not assigned to this Cohort"));
                            }
                            else if (!context.CohortCourses.Any(key =>
                                key.CohortId == parsedCohortId && key.CourseId == parsedCourseId &&
                                key.Archive == false))
                            {
                                exception.ValidationExceptions.Add(new Exception("This course has been archived for this cohort."));
                            }

                            else
                            {
                                if ( context.Homeworks.Any(key =>
                                    key.CohortId==parsedCohortId&&key.CourseId==parsedCourseId&&
                                    String.Equals(key.Title,title,StringComparison.CurrentCultureIgnoreCase)) )
                                {
                                    exception.ValidationExceptions.Add(new Exception("Homework Title with same name already exists under this course for this cohort."));
                                }
                            }
                        }
                    }
                }
            }
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
                if (parsedCohortId > 2147483647 || parsedCohortId < 1)
                {
                    exception.ValidationExceptions.Add(new Exception("Cohort Id value should be between 1 & 2147483647 inclusive"));
                }
                else if (!context.Cohorts.Any(key => key.CohortId == parsedCohortId))
                {
                    exception.ValidationExceptions.Add(new Exception("Cohort Id does not exist"));
                }
                else if (!context.Cohorts.Any(key => key.CohortId == parsedCohortId && key.Archive == false))
                {
                    exception.ValidationExceptions.Add(new Exception("Cohort has been archived"));
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
                if (parsedInstructorId > 2147483647 || parsedInstructorId < 1)
                {
                    exception.ValidationExceptions.Add(new Exception("Instructor Id value should be between 1 & 2147483647 inclusive"));
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

            if (!string.IsNullOrWhiteSpace(isAssignment))
            {
                if (!bool.TryParse(isAssignment, out parsedIsAssignment))
                {
                    exception.ValidationExceptions.Add(new Exception("Invalid value for isAssignment."));
                }
            }

            if ( string.IsNullOrWhiteSpace(title) )
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(title),nameof(title)+" is null."));
            }
            else if ( title.Length>100 )
            {
                exception.ValidationExceptions.Add(new Exception("Homework Title can only be 100 characters long."));
            }

            if (!string.IsNullOrWhiteSpace(avgCompletionTime))
            {
                if (!float.TryParse(avgCompletionTime, out parsedAvgCompletionTime))
                {
                    exception.ValidationExceptions.Add(new Exception("Invalid value for average Completion time"));
                }
                else if (parsedAvgCompletionTime > 999.99 || parsedAvgCompletionTime < 0)
                {
                    exception.ValidationExceptions.Add(new Exception("Average Completion Time should be between 0 and 999.99 inclusive"));
                }
            }
            if (!string.IsNullOrWhiteSpace(releaseDate))
            {
                if (!DateTime.TryParse(releaseDate, out parsedReleaseDate))
                {
                    exception.ValidationExceptions.Add(new Exception("Invalid value for release date"));
                }
            }
            if (!string.IsNullOrWhiteSpace(dueDate))
            {
                if (!DateTime.TryParse(dueDate, out parsedDueDate))
                {
                    exception.ValidationExceptions.Add(new Exception("Invalid value for due date"));
                }
                else if (!string.IsNullOrWhiteSpace(releaseDate) &&
                         DateTime.TryParse(releaseDate, out parsedReleaseDate) && parsedReleaseDate > parsedDueDate)
                {
                    exception.ValidationExceptions.Add(new Exception("Homework can not be due before it is released."));
                }
            }
            if (!string.IsNullOrWhiteSpace(documentLink))
            {
                if (documentLink.Length > 250)
                {
                    exception.ValidationExceptions.Add(new Exception("Document Link can only be 250 characters long."));
                }
                else
                {
                    /** Citation
                     *  https://stackoverflow.com/questions/161738/what-is-the-best-regular-expression-to-check-if-a-string-is-a-valid-url
                     *  Referenced above source to validate the incoming Resources Link (URL) before saving to DB.
                     */
                    Uri uri;
                    if (!(Uri.TryCreate(documentLink, UriKind.Absolute, out uri) &&
                          (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps || uri.Scheme == Uri.UriSchemeFtp)))
                    {
                        exception.ValidationExceptions.Add(new Exception("Document Link is not valid."));
                    }
                    /*End Citation*/
                }
            }
            if (!string.IsNullOrWhiteSpace(gitHubClassRoomLink))
            {
                if (gitHubClassRoomLink.Length > 250)
                {
                    exception.ValidationExceptions.Add(new Exception("Github Classroom Link can only be 250 characters long."));
                }
                else
                {
                    /** Citation
                     *  https://stackoverflow.com/questions/161738/what-is-the-best-regular-expression-to-check-if-a-string-is-a-valid-url
                     *  Referenced above source to validate the incoming Resources Link (URL) bafire saving to DB.
                     */
                    Uri uri;
                    if (!(Uri.TryCreate(gitHubClassRoomLink, UriKind.Absolute, out uri) &&
                          (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps || uri.Scheme == Uri.UriSchemeFtp)))
                    {
                        exception.ValidationExceptions.Add(new Exception("Github Classroom Link is not valid."));
                    }
                    /*End Citation*/
                }
            }

            if (exception.ValidationExceptions.Count > 0)
            {
                throw exception;
            }

            #endregion

            var homework = context.Homeworks.SingleOrDefault(key => key.HomeworkId == parsedHomeworkId);
            if (dueDate != null && releaseDate != null)
            {
                homework.CourseId = parsedCourseId;
                homework.InstructorId = parsedInstructorId;
                homework.CohortId = parsedCohortId;
                homework.IsAssignment = parsedIsAssignment;
                homework.Title = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(title);
                homework.AvgCompletionTime = parsedAvgCompletionTime;
                homework.DueDate = parsedDueDate;
                homework.ReleaseDate = parsedReleaseDate;
                homework.DocumentLink = documentLink;
                homework.GitHubClassRoomLink = gitHubClassRoomLink;
            }
            else if (dueDate == null && releaseDate == null)
            {
                homework.CourseId = parsedCourseId;
                homework.InstructorId = parsedInstructorId;
                homework.CohortId = parsedCohortId;
                homework.IsAssignment = parsedIsAssignment;
                homework.Title = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(title);
                homework.AvgCompletionTime = parsedAvgCompletionTime;
                homework.DocumentLink = documentLink;
                homework.GitHubClassRoomLink = gitHubClassRoomLink;
            }
            else if (dueDate == null)
            {
                homework.CourseId = parsedCourseId;
                homework.InstructorId = parsedInstructorId;
                homework.CohortId = parsedCohortId;
                homework.IsAssignment = parsedIsAssignment;
                homework.Title = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(title);
                homework.AvgCompletionTime = parsedAvgCompletionTime;
                homework.ReleaseDate = parsedReleaseDate;
                homework.DocumentLink = documentLink;
                homework.GitHubClassRoomLink = gitHubClassRoomLink;
            }
            else if (releaseDate == null)
            {
                homework.CourseId = parsedCourseId;
                homework.InstructorId = parsedInstructorId;
                homework.CohortId = parsedCohortId;
                homework.IsAssignment = parsedIsAssignment;
                homework.Title = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(title);
                homework.AvgCompletionTime = parsedAvgCompletionTime;
                homework.DueDate = parsedDueDate;
                homework.DocumentLink = documentLink;
                homework.GitHubClassRoomLink = gitHubClassRoomLink;
            }
            
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
            int parsedHomeworkId = 0;

            #region Validation

            homeworkId = (string.IsNullOrEmpty(homeworkId) || string.IsNullOrWhiteSpace(homeworkId)) ? null : homeworkId.Trim();

            ValidationException exception = new ValidationException();
            using var context = new AppDbContext();

            if (string.IsNullOrWhiteSpace(homeworkId))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(homeworkId), nameof(homeworkId) + " is null."));
            }
            else
            {
                if (!int.TryParse(homeworkId, out parsedHomeworkId))
                {
                    exception.ValidationExceptions.Add(new Exception("Invalid value for Homework Id"));
                }
                if (parsedHomeworkId > 2147483647 || parsedHomeworkId < 1)
                {
                    exception.ValidationExceptions.Add(new Exception("Homework Id value should be between 1 & 2147483647 inclusive"));
                }
                else if (!context.Homeworks.Any(key => key.HomeworkId == parsedHomeworkId))
                {
                    exception.ValidationExceptions.Add(new Exception("Homework Id does not exist"));
                }
                
            }
            if (exception.ValidationExceptions.Count > 0)
            {
                throw exception;
            }

            #endregion

            Homework result = context.Homeworks.Single(key => key.HomeworkId == parsedHomeworkId);
            return result;
        }

        public static void ArchiveHomeworkById(string homeworkId)
        {
            int parsedHomeworkId = 0;

            #region Validation

            homeworkId = (string.IsNullOrEmpty(homeworkId) || string.IsNullOrWhiteSpace(homeworkId)) ? null : homeworkId.Trim();

            ValidationException exception = new ValidationException();
            using var context = new AppDbContext();

            if (string.IsNullOrWhiteSpace(homeworkId))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(homeworkId), nameof(homeworkId) + " is null."));
            }
            else
            {
                if (!int.TryParse(homeworkId, out parsedHomeworkId))
                {
                    exception.ValidationExceptions.Add(new Exception("Invalid value for Homework Id"));
                }
                if (parsedHomeworkId > 2147483647 || parsedHomeworkId < 1)
                {
                    exception.ValidationExceptions.Add(new Exception("HomeworkId Id value should be between 1 & 2147483647 inclusive"));
                }
                else if (!context.Homeworks.Any(key => key.HomeworkId == parsedHomeworkId))
                {
                    exception.ValidationExceptions.Add(new Exception("Homework Id does not exist"));
                }
                else if (!context.Homeworks.Any(key => key.HomeworkId == parsedHomeworkId && key.Archive == false))
                {
                    exception.ValidationExceptions.Add(new Exception("Homework is already archived"));
                }
            }
            
            if (exception.ValidationExceptions.Count > 0)
            {
                throw exception;
            }

            #endregion

            var rubrics = context.Rubrics.Where(key => key.HomeworkId == parsedHomeworkId).ToList();
            foreach (var rubric in rubrics)
            {
                rubric.Archive = true;

                var grades = context.Grades.Where(key => key.RubricId == rubric.RubricId).ToList();
                foreach (var grade in grades)
                {
                    grade.Archive = true;
                }
            }
            
            var timesheets = context.Timesheets.Where(key => key.HomeworkId == parsedHomeworkId).ToList();
            foreach (var timesheet in timesheets)
            {
                timesheet.Archive = true;
            }


            var homework = context.Homeworks.SingleOrDefault(key => key.HomeworkId == parsedHomeworkId);
            homework.Archive = true;

            context.SaveChanges();
        }

        public static void ArchiveHomeworkByCohortId(string cohortId)
        {
            var exception = new ValidationException();
            using var context = new AppDbContext();
            var parsedCohortId = 0;

            #region Validation

            cohortId = (string.IsNullOrEmpty(cohortId) || string.IsNullOrWhiteSpace(cohortId)) ? null : cohortId.Trim();
            
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
                if (parsedCohortId > 2147483647 || parsedCohortId < 1)
                {
                    exception.ValidationExceptions.Add(new Exception("Cohort Id value should be between 1 & 2147483647 inclusive"));
                }
                else if (!context.Cohorts.Any(key => key.CohortId == parsedCohortId))
                {
                    exception.ValidationExceptions.Add(new Exception("Cohort Id does not exist"));
                }
            }

            if (exception.ValidationExceptions.Count > 0)
            {
                throw exception;
            }
            else
            {
                var homeworks = context.Homeworks.Where(key => key.CohortId == parsedCohortId).ToList();

                foreach (var homework in homeworks)
                {
                    homework.Archive = true;
                }
                context.SaveChanges();
            }

            #endregion

        } //Extra as of now
    }
}