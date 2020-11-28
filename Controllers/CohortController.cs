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

            #region Validation

            var exception = new ValidationException();
            name = string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name) ? null : name.Trim();
            capacity = string.IsNullOrEmpty(capacity) || string.IsNullOrWhiteSpace(capacity) ? null : capacity.Trim();
            modeOfTeaching = string.IsNullOrEmpty(modeOfTeaching) || string.IsNullOrWhiteSpace(modeOfTeaching) ? null : modeOfTeaching.Trim();
            startDate = string.IsNullOrEmpty(startDate) || string.IsNullOrWhiteSpace(startDate) ? null : startDate.Trim();
            endDate = string.IsNullOrEmpty(endDate) || string.IsNullOrWhiteSpace(endDate) ? null : endDate.Trim();
            city = string.IsNullOrEmpty(city) || string.IsNullOrWhiteSpace(city) ? null : city.Trim();

            using var context = new AppDbContext();

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
                if ( context.Cohorts.Any(key => key.Name.ToLower()==name.ToLower()))
                {
                    exception.ValidationExceptions.Add(
                        new Exception("Cohort with same name already exists."));
                }

                if ( context.Cohorts.Any(key => key.Name.ToLower()==name.ToLower() && key.Archive==true))
                    exception.ValidationExceptions.Add(
                        new Exception("Selected Cohort is Archived."));
            }

            /*To Check for Null or Empty*/
            if (!string.IsNullOrEmpty(capacity))
            {
                if (!int.TryParse(capacity, out parsedCapacity))
                    exception.ValidationExceptions.Add(new Exception("Invalid value for Capacity"));
                else if (parsedCapacity > 999 || parsedCapacity < 0)
                    exception.ValidationExceptions.Add(
                        new Exception("Capacity value should be between 0 & 999 inclusive."));
            }

            if (string.IsNullOrWhiteSpace(modeOfTeaching))
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(modeOfTeaching),
                    nameof(modeOfTeaching) + " is null."));
            else if (modeOfTeaching.Length > 50)
                exception.ValidationExceptions.Add(new Exception("Mode of Teaching can only be 50 characters long."));
            if (string.IsNullOrWhiteSpace(startDate))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(startDate),
                    nameof(startDate) + " is null."));
            }
            else
            {
                if (!DateTime.TryParse(startDate, out parsedStartDate))
                    exception.ValidationExceptions.Add(new Exception("Invalid value for startDate"));
                /*else if (parsedStartDate < DateTime.Now.Date)
                    exception.ValidationExceptions.Add(
                        new Exception("This Cohort can not have start date in the past."));*/
                //NOT REQUIRED AS WARNING IS GIVEN AT FRONT END
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
                /*  else if (parsedEndDate < DateTime.Now.Date)
                      exception.ValidationExceptions.Add(new Exception("This Cohort can not have end date in the past."));*/
                //NOT REQUIRED AS WARNING IS GIVEN AT FRONT END
            }

            /* Business Logic*/
            if (DateTime.TryParse(startDate, out parsedStartDate) && DateTime.TryParse(endDate, out parsedEndDate))
                if (parsedEndDate < parsedStartDate)
                    exception.ValidationExceptions.Add(new Exception("End date can not be before Start date."));

            if (string.IsNullOrWhiteSpace(city))
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(city), nameof(city) + " is null."));
            else if (city.Length > 50)
                exception.ValidationExceptions.Add(new Exception("City can only be 50 characters long."));

            if (exception.ValidationExceptions.Count > 0) throw exception;

            #endregion

            if (capacity == null)
            {
                context.Add(new Cohort
                {
                    Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name),
                    City = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(city),
                    ModeOfTeaching = modeOfTeaching,
                    StartDate = parsedStartDate,
                    EndDate = parsedEndDate
                });
            }
            else if (capacity != null)
            {
                context.Add(new Cohort
                {
                    Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name),
                    Capacity = parsedCapacity,
                    City = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(city),
                    ModeOfTeaching = modeOfTeaching,
                    StartDate = parsedStartDate,
                    EndDate = parsedEndDate
                });
            }

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
        /// <param name="cohortId"></param>
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

            #region Validation

            var exception = new ValidationException();

            cohortId = (string.IsNullOrEmpty(cohortId) || string.IsNullOrWhiteSpace(cohortId)) ? null : cohortId.Trim();
            name = string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name) ? null : name.Trim();
            capacity = string.IsNullOrEmpty(capacity) || string.IsNullOrWhiteSpace(capacity) ? null : capacity.Trim();
            modeOfTeaching = string.IsNullOrEmpty(modeOfTeaching) || string.IsNullOrWhiteSpace(modeOfTeaching)
                ? null
                : modeOfTeaching.Trim();
            startDate = string.IsNullOrEmpty(startDate) || string.IsNullOrWhiteSpace(startDate)
                ? null
                : startDate.Trim();
            endDate = string.IsNullOrEmpty(endDate) || string.IsNullOrWhiteSpace(endDate) ? null : endDate.Trim();
            city = string.IsNullOrEmpty(city) || string.IsNullOrWhiteSpace(city) ? null : city.Trim();

            using var context = new AppDbContext();

            if (string.IsNullOrWhiteSpace(cohortId) )
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(cohortId),
                    nameof(cohortId) + " is null."));
            }
            else
            {
                if (!int.TryParse(cohortId, out parsedCohortId))
                {
                    exception.ValidationExceptions.Add(new Exception("Invalid value for Cohort Id"));
                }
                if(parsedCohortId > 2147483647 || parsedCohortId < 1)
                {
                    exception.ValidationExceptions.Add(new Exception("Cohort Id value should be between 1 & 2147483647 inclusive"));
                }
                /*If the Cohort Exists or not If cohort is Archived you cannot update the course*/
                else if ( !context.Cohorts.Any(key => key.CohortId==parsedCohortId)  )
                {
                    exception.ValidationExceptions.Add(new Exception("Cohort Id does not exist"));

                }
                else if (!context.Cohorts.Any(key => key.CohortId == parsedCohortId && key.Archive == false))
                {
                    exception.ValidationExceptions.Add(new Exception("Cohort Id is Archived"));
                }

            }
            if ( string.IsNullOrWhiteSpace(name) )
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(name),nameof(name)+" is null."));
            else if ( name.Length>50 )
                exception.ValidationExceptions.Add(new Exception("Cohort name can only be 50 characters long."));
            else
            {
                if ((!string.IsNullOrWhiteSpace(cohortId)) && int.TryParse(cohortId, out parsedCohortId))
                {
                    /* Two cohorts with same name should not be allowed */
                    if (context.Cohorts.Any(key => key.Name.ToLower() == name.ToLower() && key.CohortId != parsedCohortId))
                    {
                        exception.ValidationExceptions.Add(new Exception("A cohort with this name already exists."));
                    }
                }
            }

            if (!string.IsNullOrEmpty(capacity))
            {
                if (!int.TryParse(capacity, out parsedCapacity))
                    exception.ValidationExceptions.Add(new Exception("Invalid value for Capacity"));
                else if (parsedCapacity > 999 || parsedCapacity < 0)
                    exception.ValidationExceptions.Add(
                        new Exception("Capacity value should be between 0 & 999 inclusive."));
            }

            if (string.IsNullOrWhiteSpace(modeOfTeaching))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(modeOfTeaching),
                    nameof(modeOfTeaching) + " is null."));
            }
            else if (modeOfTeaching.Length > 50)
                {
                    exception.ValidationExceptions.Add(
                        new Exception("Mode of Teaching can only be 50 characters long."));
                }         
            if (string.IsNullOrWhiteSpace(startDate))
                {
                    exception.ValidationExceptions.Add(new ArgumentNullException(nameof(startDate),
                        nameof(startDate) + " is null."));
                }
                else if (!DateTime.TryParse(startDate, out parsedStartDate))
                    exception.ValidationExceptions.Add(new Exception("Invalid value for startDate"));
                /* else if ( parsedStartDate<DateTime.Now.Date )
                     exception.ValidationExceptions.Add(
                         new Exception("This Cohort can not have start date in the past."));*/
                //NOT REQUIRED AS WARNING IS GIVEN AT FRONT END          

            if ( string.IsNullOrWhiteSpace(endDate) )
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(endDate),
                    nameof(endDate) + " is null."));}
            else if (!DateTime.TryParse(endDate, out parsedEndDate))
                exception.ValidationExceptions.Add(new Exception("Invalid value for endDate"));
            /*  else if ( parsedEndDate<DateTime.Now.Date )
                  exception.ValidationExceptions.Add(new Exception("This Cohort can not have end date in the past."));*/
            //NOT REQUIRED AS WARNING IS GIVEN AT FRONT END

            /* Business Logic*/
            if (DateTime.TryParse(startDate, out parsedStartDate) && DateTime.TryParse(endDate, out parsedEndDate))
            {
                if (parsedEndDate < parsedStartDate)
                    exception.ValidationExceptions.Add(new Exception("End date can not be before Start date."));
            }

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
                cohort.ModeOfTeaching = modeOfTeaching;
                cohort.StartDate = parsedStartDate;
                cohort.EndDate = parsedEndDate;
            }
            else if (capacity != null)
            {
                cohort.Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name);
                cohort.Capacity = parsedCapacity;
                cohort.City = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(city);
                cohort.ModeOfTeaching = modeOfTeaching;
                cohort.StartDate = parsedStartDate;
                cohort.EndDate = parsedEndDate;
            }
           
            context.SaveChanges();
           
        }

        /// <summary>
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
        ///     ArchiveCohortById
        ///     Description: This action archives a cohort by cohortId PK
        /// </summary>
        /// <param name="cohortId"></param>
        public static void ArchiveCohortById(string cohortId)
        {

            var exception = new ValidationException();
            using var context = new AppDbContext();
            var parsedCohortId = 0;

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
                {
                    exception.ValidationExceptions.Add(new Exception("Invalid value for Cohort Id"));
                }
                if (parsedCohortId > 2147483647 || parsedCohortId < 1)
                {
                    exception.ValidationExceptions.Add(new Exception("Cohort Id value should be between 1 & 2147483647 inclusive"));
                }
                else if (!context.Cohorts.Any(key => key.CohortId == parsedCohortId))
                    exception.ValidationExceptions.Add(new Exception("Cohort Id does not exist"));
                else if (context.Cohorts.Any(key => key.CohortId == parsedCohortId && key.Archive == true))
                    exception.ValidationExceptions.Add(new Exception("Cohort Id is already archived"));
            }

            #endregion

            if (exception.ValidationExceptions.Count > 0) throw exception;
            

            var assignedCourses = context.CohortCourses.Where(key => key.CohortId == parsedCohortId).ToList();
                foreach (var course in assignedCourses)
                {
                    course.Archive = true;
                }
                
            var homeworks = context.Homeworks.Where(key => key.CohortId == parsedCohortId).ToList();

            foreach (var homework in homeworks)
            {
                homework.Archive = true;
            }

            var cohort = context.Cohorts.Find(parsedCohortId);
            cohort.Archive = true;
            
            context.SaveChanges();
        }

    }
}