using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AZLearn.Data;
using AZLearn.Models;


namespace AZLearn.Controllers
{
    public class UserController :ControllerBase
    {
        /// <summary>
        /// This Action takes in Cohort id and returns List of students enrolled in that Cohort.
        /// </summary>
        /// <param name="cohortId">Cohort Id</param>
        /// <returns>List of students enrolled in specified Cohort</returns>
        public static List<User> GetStudentsByCohortId(string cohortId)
        {
            int parsedCohortId = int.Parse(cohortId);
            List<User> students = new List<User>();
            using var context = new AppDbContext();
            students = context.Users.Where(key => key.CohortId == parsedCohortId).ToList();
            return students;
        }
    }
}
