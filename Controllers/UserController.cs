using System;
using System.Collections.Generic;
using System.Linq;
using AZLearn.Data;
using AZLearn.Models;
using AZLearn.Models.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace AZLearn.Controllers
{
    public class UserController : ControllerBase
    {
        /// <summary>
        ///     GetStudentsByCohortId
        ///     This Action takes in Cohort id and returns List of students enrolled in that Cohort.
        /// </summary>
        /// <param name="cohortId">Cohort Id</param>
        /// <returns>List of students enrolled in specified Cohort</returns>
        /// /application/InstructorGradeSummary
        public static List<User> GetStudentsByCohortId(string cohortId)
        {
            int parsedCohortId = 0;

            #region Validation

            ValidationException exception = new ValidationException();

            cohortId=(string.IsNullOrEmpty(cohortId)||string.IsNullOrWhiteSpace(cohortId)) ? null : cohortId.Trim();

            using var context = new AppDbContext();
            if ( string.IsNullOrWhiteSpace(cohortId) )
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(cohortId),nameof(cohortId)+" is null."));
            }
            else
            {
                if ( !int.TryParse(cohortId,out parsedCohortId) )
                {
                    exception.ValidationExceptions.Add(new Exception("Invalid value for Cohort Id"));
                }
                else if ( !context.Cohorts.Any(key => key.CohortId==parsedCohortId) )
                {
                    exception.ValidationExceptions.Add(new Exception("Cohort Id does not exist"));
                }
            }
            if ( exception.ValidationExceptions.Count>0 )
            {
                throw exception;
            }
            #endregion
            var students = new List<User>();
            students= context.Users.Where(key => key.CohortId == parsedCohortId).ToList();
            return students;
        }

        /// <summary>
        ///     GetUserById
        ///     Description: Controller action that gets user information by the userId
        ///     It expects below parameters, and would populate the user information according to the parameter specified
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>It returns the User Information based on the user id </returns>
        public static User GetUserById(string userId)
        {

            User result;
            var parsedUserId = 0;

            #region Validation
             ValidationException exception = new ValidationException();
             using var context = new AppDbContext();

            userId=(string.IsNullOrEmpty(userId)||string.IsNullOrWhiteSpace(userId)) ? null : userId.Trim();

            if ( string.IsNullOrWhiteSpace(userId) )
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(userId),nameof(userId)+" is null."));
            }
            else
            {
                if ( !int.TryParse(userId, out parsedUserId) )
                {
                    exception.ValidationExceptions.Add(new Exception("Invalid value for Homework Id"));
                }
                else if ( !context.Users.Any(key => key.UserId==parsedUserId) )
                {
                    exception.ValidationExceptions.Add(new Exception("User Id does not exist"));
                }
            }
            if ( exception.ValidationExceptions.Count>0 )
            {
                throw exception;
            }

            #endregion
            result= context.Users.Single(key => key.UserId == parsedUserId);
            
            return result;
        }

        /// <summary>
        ///     GetInstructors
        ///     Description: Controller action that returns list of existing Instructors
        /// </summary>
        /// <returns>List of Instructors</returns>
        public static List<User> GetInstructors()
        {
            using var context = new AppDbContext();
            var instructors = context.Users.Where(key => key.IsInstructor).ToList();
            return instructors;
        }
    }
}