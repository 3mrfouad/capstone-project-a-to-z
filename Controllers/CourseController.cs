﻿using System;
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

            #region Validation

            name = string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name) ? null : name.Trim();
            description = string.IsNullOrEmpty(description) || string.IsNullOrWhiteSpace(description) ? null : description.Trim();
            durationHrs = string.IsNullOrEmpty(durationHrs) || string.IsNullOrWhiteSpace(durationHrs) ? null : durationHrs.Trim();

            using var context = new AppDbContext();
            ValidationException exception = new ValidationException();

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
                else if(context.Courses.Any(key => key.Name.ToLower() == name.ToLower() && key.Archive == false))
                {
                        exception.ValidationExceptions.Add(new Exception("Course with this name already exists."));
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
            if (exception.ValidationExceptions.Count > 0)
            {
                throw exception;
            }

            #endregion

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
        ///     Update a CourseById
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
        public static void UpdateCourseById(string courseId, string name, string description,
            string durationHrs)
        {
            int parsedCourseId = 0;
            float parsedDurationHrs = 0;

            #region Validation

            courseId = string.IsNullOrEmpty(courseId) || string.IsNullOrWhiteSpace(courseId) ? null : courseId.Trim();
            name = string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name) ? null : name.Trim();
            description = string.IsNullOrEmpty(description) || string.IsNullOrWhiteSpace(description) ? null : description.Trim();
            durationHrs = string.IsNullOrEmpty(durationHrs) || string.IsNullOrWhiteSpace(durationHrs) ? null : durationHrs.Trim();

            using var context = new AppDbContext();
            ValidationException exception = new ValidationException();

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
                    if ((!string.IsNullOrWhiteSpace(courseId)) && int.TryParse(courseId, out parsedCourseId))
                    {
                        /* Two courses with same name should not be allowed */
                        if (context.Courses.Any(key => key.Name.ToLower() == name.ToLower() && key.CourseId != parsedCourseId))
                        {
                            exception.ValidationExceptions.Add(new Exception("A Course with this name already exists."));
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
            if (exception.ValidationExceptions.Count > 0)
            {
                throw exception;
            }

            #endregion

            var course = context.Courses.SingleOrDefault(key => key.CourseId == parsedCourseId);
            course.Name = name;
            course.Description = description;
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
                        .Any(subKey => subKey.CohortId == parsedCohortId)).ToList();

            /* @Amr Fouad, demo on how to get to the instructor name on same above context call
             foreach (var course in coursesListByCohortId)
            {
                var name = course.CohortCourses.Where(key => key.CourseId == course.CourseId).SingleOrDefault().Instructor.Name;
            }*/
            return coursesListByCohortId;
        }

        /// <summary>
        /// GetCourseByCohortId
        /// Description: Controller action that returns a Courses by CohortId
        /// It expects below parameters, and would return a course by cohort id from the database.
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="cohortId"></param>
        /// <returns></returns>
        public static Course GetCourseByCohortId(string courseId, string cohortId)
        {
            var parsedCohortId = int.Parse(cohortId);
            var parsedCourseId = int.Parse(courseId);
            using var context = new AppDbContext();
            var courseByCohortId =
                context.Courses.Include(key => key.CohortCourses).SingleOrDefault(key => key.CohortCourses.Any(subKey => subKey.CohortId == parsedCohortId && subKey.CourseId == parsedCourseId));
            return courseByCohortId;
        }
    }
}