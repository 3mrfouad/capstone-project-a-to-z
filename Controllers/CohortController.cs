using System;
using System.Collections.Generic;
using System.Linq;
using AZLearn.Data;
using AZLearn.Models;
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
            var parsedStartDate = DateTime.Parse(startDate);
            var parsedEndDate = DateTime.Parse(endDate);
            int parsedCapacity = int.Parse(capacity);

            using var context = new AppDbContext();
            context.Add(new Cohort
            {
                Name = name,
                Capacity =parsedCapacity,
                City = city,
                ModeOfTeaching = modeOfTeaching,
                StartDate =parsedStartDate,
                EndDate =parsedEndDate
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
            var parsedStartDate = DateTime.Parse(startDate);
            var parsedEndDate = DateTime.Parse(endDate);
            int parsedCapacity = int.Parse(capacity);

            using var context = new AppDbContext();
            var cohort = context.Cohorts.Find(int.Parse(cohortId));
            cohort.Name = name;
            cohort.Capacity=parsedCapacity;
            cohort.City = city;
            cohort.ModeOfTeaching = modeOfTeaching;
            cohort.StartDate =parsedStartDate;
            cohort.EndDate =parsedEndDate;
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
    }
}