using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
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
            var parsedCohortId = 0;
            var exception = new ValidationException();
            using var context = new AppDbContext();

            #region Validation

            cohortId = string.IsNullOrEmpty(cohortId) || string.IsNullOrWhiteSpace(cohortId) ? null : cohortId.Trim();

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
                        new Exception("cohortId value should be between 1 & 2147483647 inclusive"));
                else if (!context.Cohorts.Any(key => key.CohortId == parsedCohortId))
                    exception.ValidationExceptions.Add(new Exception("cohortId does not exist"));
            }

            if (exception.ValidationExceptions.Count > 0) throw exception;

            #endregion

            var students = new List<User>();
            students = context.Users.Where(key => key.CohortId == parsedCohortId && key.IsInstructor == false).ToList();

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
            var exception = new ValidationException();
            using var context = new AppDbContext();

            #region Validation

            userId = string.IsNullOrEmpty(userId) || string.IsNullOrWhiteSpace(userId) ? null : userId.Trim();

            if (string.IsNullOrWhiteSpace(userId))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(userId),
                    nameof(userId) + " is null."));
            }
            else
            {
                if (!int.TryParse(userId, out parsedUserId))
                    exception.ValidationExceptions.Add(new Exception("Invalid value for userId"));

                if (parsedUserId > 2147483647 || parsedUserId < 1)
                    exception.ValidationExceptions.Add(
                        new Exception("userId value should be between 1 & 2147483647 inclusive"));
                else if (!context.Users.Any(key => key.UserId == parsedUserId))
                    exception.ValidationExceptions.Add(new Exception("userId does not exist"));
                else if (!context.Users.Any(key => key.UserId == parsedUserId && key.Archive == false))
                    exception.ValidationExceptions.Add(new Exception("Selected userId is Archived"));
            }

            if (exception.ValidationExceptions.Count > 0) throw exception;

            #endregion

            result = context.Users.Single(key => key.UserId == parsedUserId);

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
            /*To avoid complexity at the Frontend, We are filtering the Archived Instructors -inconsistent with rest of the end points.*/

            var instructors = context.Users.Where(key => key.IsInstructor && key.Archive == false).ToList();
            return instructors;
        }

        /// <summary>
        ///     CreateUser
        ///     Description: Controller action that creates a user
        ///     It expects below parameters, and would register a user the user information according to the parameter specified
        /// </summary>
        /// <param name="cohortId"></param>
        /// <param name="name"></param>
        /// <param name="passwordHash"></param>
        /// <param name="email"></param>
        /// <param name="isInstructor"></param>
        public static void CreateUser(string cohortId, string name, string passwordHash, string email,
            string isInstructor)
        {
            var parsedCohortId = 0;
            var parsedIsInstructor = false;
            var exception = new ValidationException();
            using var context = new AppDbContext();

            #region Validation

            cohortId = string.IsNullOrEmpty(cohortId) || string.IsNullOrWhiteSpace(cohortId) ? null : cohortId.Trim();
            name = string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name) ? null : name.Trim().ToLower();
            passwordHash = string.IsNullOrEmpty(passwordHash) || string.IsNullOrWhiteSpace(passwordHash)
                ? null
                : passwordHash.Trim();
            email = string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email) ? null : email.Trim().ToLower();
            isInstructor = string.IsNullOrEmpty(isInstructor) || string.IsNullOrWhiteSpace(isInstructor)
                ? null
                : isInstructor.Trim();

            /*@Link email Regex: https://stackoverflow.com/questions/16167983/best-regular-expression-for-email-validation-in-c-sharp/16168118 */
            /*@Link Password Regex : https://stackoverflow.com/questions/5859632/regular-expression-for-password-validation */

            var emailRegex =
                new Regex(
                    @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");

            var passwordHashRegex = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");

            if (!string.IsNullOrWhiteSpace(cohortId))
            {
                if (!int.TryParse(cohortId, out parsedCohortId))
                    exception.ValidationExceptions.Add(new Exception("Invalid value for cohortId"));

                if (parsedCohortId > 2147483647 || parsedCohortId < 1)
                {
                    exception.ValidationExceptions.Add(
                        new Exception("cohortId value should be between 1 & 2147483647 inclusive"));
                }
                else
                {
                    if (!context.Cohorts.Any(key => key.CohortId == parsedCohortId))
                        exception.ValidationExceptions.Add(new Exception("cohortId does not exist."));
                    else if (!context.Cohorts.Any(key => key.CohortId == parsedCohortId && key.Archive == false))
                        exception.ValidationExceptions.Add(new Exception("Cohort has been archived"));
                }
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(name), nameof(name) + " is null."));
            }
            else
            {
                if (name.Length > 50)
                    exception.ValidationExceptions.Add(new Exception("Name can only be 50 characters long."));

                else if (context.Users.Any(key => key.Name.ToLower() == name.ToLower()))
                    exception.ValidationExceptions.Add(new Exception("User with this name already exists."));
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                exception.ValidationExceptions.Add(
                    new ArgumentNullException(nameof(email), nameof(email) + " is null."));
            }

            else if (email.Length > 50)
            {
                exception.ValidationExceptions.Add(new Exception("Email can only be 50 characters long."));
            }

            else if (context.Users.Any(key => key.Email.ToLower() == email.ToLower()))
            {
                exception.ValidationExceptions.Add(
                    new Exception("Email already exists,please try to use a different email address."));
            }

            else
            {
                if (!emailRegex.IsMatch(email))

                    exception.ValidationExceptions.Add(new Exception("Email is not in appropriate format"));
            }

            if (string.IsNullOrWhiteSpace(passwordHash))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(passwordHash),
                    nameof(passwordHash) + " is null."));
            }

            else if (passwordHash.Length > 250)
            {
                exception.ValidationExceptions.Add(new Exception("Password can only be 250 characters long."));
            }

            else
            {
                if (!passwordHashRegex.IsMatch(passwordHash))
                    exception.ValidationExceptions.Add(new Exception(
                        "Password is in inappropriate format: Password must be: at least one upper case letter, at least one lower case letter, at least one digit , at least one special character, minimum 8 characters in length"));
            }

            if (!string.IsNullOrWhiteSpace(isInstructor))
                if (!bool.TryParse(isInstructor, out parsedIsInstructor))
                    exception.ValidationExceptions.Add(new Exception("Invalid value for isInstructor."));

            if (exception.ValidationExceptions.Count > 0) throw exception;

            #endregion

            var newUser = new User
            {
                CohortId = parsedCohortId,
                Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name),
                PasswordHash = passwordHash,
                Email = email,
                IsInstructor = parsedIsInstructor
            };
            context.Users.Add(newUser);

            context.SaveChanges();
        }

        /// <summary>
        ///     GetUserOnLogin
        ///     Description:This action takes below parameters and retrieves the user information if input fields are correct
        ///     otherwise displays error
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="password"></param>
        /// <returns>Sucess/Error message</returns>
        public static User GetUserOnLogin(string userEmail, string password)
        {
            var exception = new ValidationException();
            using var context = new AppDbContext();

            #region Validation

            userEmail = string.IsNullOrEmpty(userEmail) || string.IsNullOrWhiteSpace(userEmail)
                ? null
                : userEmail.Trim().ToLower();
            password = string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(password) ? null : password.Trim();

            if (string.IsNullOrWhiteSpace(userEmail))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(userEmail),
                    nameof(userEmail) + " is null."));
            }
            else
            {
                if (!context.Users.Any(key => key.Email == userEmail))
                {
                    exception.ValidationExceptions.Add(new Exception("User account with this email does not exist."));
                }

                else if (!context.Users.Any(key => key.Email == userEmail && key.Archive == false))
                {
                    exception.ValidationExceptions.Add(
                        new Exception("This user account has been archived in the system."));
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(password))
                    {
                        exception.ValidationExceptions.Add(new ArgumentNullException(nameof(password),
                            nameof(password) + " is null."));
                    }
                    else
                    {
                        if (!context.Users.Any(key => key.PasswordHash == password && key.Email == userEmail))
                            exception.ValidationExceptions.Add(new Exception("Invalid Password for this account."));
                    }
                }
            }

            if (exception.ValidationExceptions.Count > 0) throw exception;

            #endregion

            var userInfo = context.Users.Single(key =>
                key.PasswordHash == password && key.Email == userEmail && key.Archive == false);

            return userInfo;
        }
    }
}