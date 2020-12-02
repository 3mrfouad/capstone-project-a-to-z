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
    public class CohortController : ControllerBase
    {
        /// <summary>
        ///     CreateCohort
        ///     Description: Controller action that creates new cohort
        ///     It expects below parameters, and would populate the same as new cohort in the database
        /// </summary>
        /// <param name="name">string provided from frontend</param>
        /// <param name="capacity">string provided from frontend, and parsed to int to match model property data type</param>
        /// <param name="city">string provided from frontend</param>
        /// <param name="modeOfTeaching">string provided from frontend</param>
        /// <param name="startDate">string provided from frontend, and parsed to DateTime to match model property data type</param>
        /// <param name="endDate">string provided from frontend, and parsed to DateTime to match model property data type</param>
        public static void CreateCohort(string name, string capacity, string city,
            string modeOfTeaching, string startDate, string endDate)
        {
            var parsedCapacity = 0;
            var parsedStartDate = new DateTime();
            var parsedEndDate = new DateTime();
            var exception = new ValidationException();
            using var context = new AppDbContext();

            #region Validation

            name = string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name) ? null : name.Trim().ToLower();
            capacity = string.IsNullOrEmpty(capacity) || string.IsNullOrWhiteSpace(capacity) ? null : capacity.Trim();
            modeOfTeaching = string.IsNullOrEmpty(modeOfTeaching) || string.IsNullOrWhiteSpace(modeOfTeaching)
                ? null
                : modeOfTeaching.Trim().ToLower();
            startDate = string.IsNullOrEmpty(startDate) || string.IsNullOrWhiteSpace(startDate)
                ? null
                : startDate.Trim();
            endDate = string.IsNullOrEmpty(endDate) || string.IsNullOrWhiteSpace(endDate) ? null : endDate.Trim();
            city = string.IsNullOrEmpty(city) || string.IsNullOrWhiteSpace(city) ? null : city.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(name))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(name), nameof(name) + " is null."));
            }
            else if (name.Length > 50)
            {
                exception.ValidationExceptions.Add(new Exception("Cohort name can only be 50 characters long."));
            }
            else
            {
                /*To check if Cohort Name already Exists , and If the Cohort is not Archived  */
                if (context.Cohorts.Any(key => key.Name.ToLower() == name.ToLower()))
                    exception.ValidationExceptions.Add(
                        new Exception("Cohort with same name already exists."));

                if (context.Cohorts.Any(key => key.Name.ToLower() == name.ToLower() && key.Archive))
                    exception.ValidationExceptions.Add(
                        new Exception("Selected Cohort is Archived."));
            }

            /*To Check for Null or Empty*/
            if (!string.IsNullOrEmpty(capacity))
            {
                if (!int.TryParse(capacity, out parsedCapacity))
                    exception.ValidationExceptions.Add(new Exception("Invalid value for capacity"));
                else if (parsedCapacity > 999 || parsedCapacity < 0)
                    exception.ValidationExceptions.Add(
                        new Exception("Capacity value should be between 0 & 999 inclusive."));
            }

            if (string.IsNullOrWhiteSpace(modeOfTeaching))
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(modeOfTeaching),
                    nameof(modeOfTeaching) + " is null."));
            else if (modeOfTeaching.Length > 50)
                exception.ValidationExceptions.Add(new Exception("Mode of teaching can only be 50 characters long."));
            if (string.IsNullOrWhiteSpace(startDate))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(startDate),
                    nameof(startDate) + " is null."));
            }
            else
            {
                if (!DateTime.TryParse(startDate, out parsedStartDate))
                    exception.ValidationExceptions.Add(new Exception("Invalid value for startDate"));
            }

            if (string.IsNullOrWhiteSpace(endDate))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(endDate),
                    nameof(endDate) + " is null."));
            }
            else
            {
                if (!DateTime.TryParse(endDate, out parsedEndDate))
                    exception.ValidationExceptions.Add(new Exception("Invalid value for endDate"));
            }

            /* Business Logic*/
            if (DateTime.TryParse(startDate, out parsedStartDate) && DateTime.TryParse(endDate, out parsedEndDate))
                if (parsedEndDate < parsedStartDate)
                    exception.ValidationExceptions.Add(new Exception("End date can not be before start date."));

            if (string.IsNullOrWhiteSpace(city))
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(city), nameof(city) + " is null."));
            else if (city.Length > 50)
                exception.ValidationExceptions.Add(new Exception("City can only be 50 characters long."));

            if (exception.ValidationExceptions.Count > 0) throw exception;

            #endregion

            if (capacity == null)
                context.Add(new Cohort
                {
                    Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name),
                    City = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(city),
                    ModeOfTeaching = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(modeOfTeaching),
                    StartDate = parsedStartDate,
                    EndDate = parsedEndDate
                });
            else if (capacity != null)
                context.Add(new Cohort
                {
                    Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name),
                    Capacity = parsedCapacity,
                    City = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(city),
                    ModeOfTeaching = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(modeOfTeaching),

                    StartDate = parsedStartDate,
                    EndDate = parsedEndDate
                });

            context.SaveChanges();
        }

        /// <summary>
        ///     UpdateCohortById
        ///     Description: Controller action that updates existing cohort by cohortId
        ///     It expects below parameters, and would populate the same as new cohort in the database
        ///     Assumption:
        ///     The frontend view would populate the cohort information first through API call
        ///     User will edit as needed
        ///     Frontend will send update API call to backend with all keys to update database
        /// </summary>
        /// <param name="cohortId">string provided from frontend for cohort Id that will be updated</param>
        /// <param name="name">string provided from frontend</param>
        /// <param name="capacity">string provided from frontend, and parsed to int to match model property data type</param>
        /// <param name="city">string provided from frontend</param>
        /// <param name="modeOfTeaching">string provided from frontend</param>
        /// <param name="startDate">string provided from frontend, and parsed to DateTime to match model property data type</param>
        /// <param name="endDate">string provided from frontend, and parsed to DateTime to match model property data type</param>
        public static void UpdateCohortById(string cohortId, string name, string capacity, string city,
            string modeOfTeaching, string startDate, string endDate)
        {
            var parsedCohortId = 0;
            var parsedCapacity = 0;
            var parsedStartDate = new DateTime();
            var parsedEndDate = new DateTime();
            var exception = new ValidationException();
            using var context = new AppDbContext();

            #region Validation

            cohortId = string.IsNullOrEmpty(cohortId) || string.IsNullOrWhiteSpace(cohortId) ? null : cohortId.Trim();
            name = string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name) ? null : name.Trim().ToLower();
            capacity = string.IsNullOrEmpty(capacity) || string.IsNullOrWhiteSpace(capacity) ? null : capacity.Trim();
            modeOfTeaching = string.IsNullOrEmpty(modeOfTeaching) || string.IsNullOrWhiteSpace(modeOfTeaching)
                ? null
                : modeOfTeaching.Trim().ToLower();
            startDate = string.IsNullOrEmpty(startDate) || string.IsNullOrWhiteSpace(startDate)
                ? null
                : startDate.Trim();
            endDate = string.IsNullOrEmpty(endDate) || string.IsNullOrWhiteSpace(endDate) ? null : endDate.Trim();
            city = string.IsNullOrEmpty(city) || string.IsNullOrWhiteSpace(city) ? null : city.Trim().ToLower();

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
                        new Exception("Cohort Id value should be between 1 & 2147483647 inclusive"));
                /*If the Cohort Exists or not If cohort is Archived you cannot update the course*/
                else if (!context.Cohorts.Any(key => key.CohortId == parsedCohortId))
                    exception.ValidationExceptions.Add(new Exception("Cohort Id does not exist"));
                else if (!context.Cohorts.Any(key => key.CohortId == parsedCohortId && key.Archive == false))
                    exception.ValidationExceptions.Add(new Exception("Cohort Id is Archived"));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(name), nameof(name) + " is null."));
            }
            else if (name.Length > 50)
            {
                exception.ValidationExceptions.Add(new Exception("Cohort name can only be 50 characters long."));
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(cohortId) && int.TryParse(cohortId, out parsedCohortId))
                    /* Two cohorts with same name should not be allowed */
                    if (context.Cohorts.Any(key =>
                        key.Name.ToLower() == name.ToLower() && key.CohortId != parsedCohortId))
                        exception.ValidationExceptions.Add(new Exception("A cohort with this name already exists."));
            }

            if (!string.IsNullOrEmpty(capacity))
            {
                if (!int.TryParse(capacity, out parsedCapacity))
                    exception.ValidationExceptions.Add(new Exception("Invalid value for capacity"));
                else if (parsedCapacity > 999 || parsedCapacity < 0)
                    exception.ValidationExceptions.Add(
                        new Exception("Capacity value should be between 0 & 999 inclusive."));
            }

            if (string.IsNullOrWhiteSpace(modeOfTeaching))
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(modeOfTeaching),
                    nameof(modeOfTeaching) + " is null."));
            else if (modeOfTeaching.Length > 50)
                exception.ValidationExceptions.Add(
                    new Exception("Mode of teaching can only be 50 characters long."));
            if (string.IsNullOrWhiteSpace(startDate))
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(startDate),
                    nameof(startDate) + " is null."));
            else if (!DateTime.TryParse(startDate, out parsedStartDate))
                exception.ValidationExceptions.Add(new Exception("Invalid value for startDate"));

            if (string.IsNullOrWhiteSpace(endDate))
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(endDate),
                    nameof(endDate) + " is null."));
            else if (!DateTime.TryParse(endDate, out parsedEndDate))
                exception.ValidationExceptions.Add(new Exception("Invalid value for endDate"));

            /* Business Logic*/
            if (DateTime.TryParse(startDate, out parsedStartDate) && DateTime.TryParse(endDate, out parsedEndDate))
                if (parsedEndDate < parsedStartDate)
                    exception.ValidationExceptions.Add(new Exception("End date can not be before start date."));

            if (string.IsNullOrWhiteSpace(city))
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(city), nameof(city) + " is null."));
            else if (city.Length > 50)
                exception.ValidationExceptions.Add(new Exception("City can only be 50 characters long."));

            if (exception.ValidationExceptions.Count > 0) throw exception;

            #endregion

            var cohort = context.Cohorts.Find(parsedCohortId);
            if (capacity == null)
            {
                cohort.Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name);
                cohort.City = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(city);
                cohort.ModeOfTeaching = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(modeOfTeaching);
                cohort.StartDate = parsedStartDate;
                cohort.EndDate = parsedEndDate;
            }
            else if (capacity != null)
            {
                cohort.Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name);
                cohort.Capacity = parsedCapacity;
                cohort.City = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(city);
                cohort.ModeOfTeaching = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(modeOfTeaching);
                cohort.StartDate = parsedStartDate;
                cohort.EndDate = parsedEndDate;
            }

            context.SaveChanges();
        }

        /// <summary>
        ///     GetCohorts
        ///     Description: Controller action that returns list of existing cohorts
        /// </summary>
        /// <returns>
        ///     list of cohorts
        /// </returns>
        public static List<Cohort> GetCohorts()
        {
            return new AppDbContext().Cohorts.ToList();
        }

        /// <summary>
        ///     GetCohortById
        ///     This Action takes in cohortId and returns the respective Cohort record
        /// </summary>
        /// <param name="cohortId"></param>
        /// <returns> Single Cohort record</returns>
        public static Cohort GetCohortById(string cohortId)
        {
            var parsedCohortId = 0;
            var exception = new ValidationException();
            using var context = new AppDbContext();

            #region Validation

            cohortId = string.IsNullOrEmpty(cohortId) || string.IsNullOrWhiteSpace(cohortId) ? null : cohortId.Trim();
            if (cohortId == null)
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
                        new Exception("Cohort Id value should be between 1 & 2147483647 inclusive"));
                else if (!context.Cohorts.Any(key => key.CohortId == parsedCohortId))
                    exception.ValidationExceptions.Add(new Exception("cohortId does not exist"));
            }

            if (exception.ValidationExceptions.Count > 0) throw exception;

            #endregion

            return context.Cohorts.SingleOrDefault(key => key.CohortId == parsedCohortId);
        }

        /// <summary>
        ///     ArchiveCohortById
        ///     Description: This action archives a cohort by cohortId PK
        /// </summary>
        /// <param name="cohortId">string provided from frontend for cohort Id that will be archived</param>
        public static void ArchiveCohortById(string cohortId)
        {
            var parsedCohortId = 0;
            var exception = new ValidationException();
            using var context = new AppDbContext();

            #region Validation

            cohortId = string.IsNullOrEmpty(cohortId) || string.IsNullOrWhiteSpace(cohortId) ? null : cohortId.Trim();
            if (cohortId == null)
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(cohortId),
                    nameof(cohortId) + " is null."));
            }
            else
            {
                if (!int.TryParse(cohortId, out parsedCohortId))
                    exception.ValidationExceptions.Add(new Exception("Invalid value for Cohort Id"));
                if (parsedCohortId > 2147483647 || parsedCohortId < 1)
                    exception.ValidationExceptions.Add(
                        new Exception("cohortId value should be between 1 & 2147483647 inclusive"));
                else if (!context.Cohorts.Any(key => key.CohortId == parsedCohortId))
                    exception.ValidationExceptions.Add(new Exception("Cohort Id does not exist"));
                else if (context.Cohorts.Any(key => key.CohortId == parsedCohortId && key.Archive))
                    exception.ValidationExceptions.Add(new Exception("cohortId is already archived"));
            }

            if (exception.ValidationExceptions.Count > 0) throw exception;
            
            #endregion

            var assignedCourses = context.CohortCourses.Where(key => key.CohortId == parsedCohortId).ToList();
            foreach (var course in assignedCourses) course.Archive = true;

            var homeworks = context.Homeworks.Where(key => key.CohortId == parsedCohortId).ToList();
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

            var cohort = context.Cohorts.Find(parsedCohortId);
            cohort.Archive = true;

            context.SaveChanges();
        }
    }
}