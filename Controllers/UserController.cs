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
        ///     GetStudentsByCohortId
        ///     This Action takes in Cohort id and returns List of students enrolled in that Cohort.
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
        /// <summary>
        /// GetUserById
        /// Description: Controller action that gets user information by the userId
        /// It expects below parameters, and would populate the user information according to the parameter specified
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>It returns the User Information based on the user id </returns>
        public static User GetUserById(string userId)
        {
            User result;
            var parsedUserId = int.Parse(userId);
            using var context = new AppDbContext();
            {
                result=context.Users.Single(key => key.UserId==parsedUserId);
            }
            return result;
        }
        /// <summary>
        /// GetInstructors
        ///  Description: Controller action that returns list of existing Instructors
        /// </summary>
        /// <returns>List of Instructors</returns>
        public static List<User> GetInstructors()
        {
            using var context = new AppDbContext();
            var instructors = context.Users.Where(key=>key.IsInstructor==true).ToList();
            return instructors ;
        }



    }
}
