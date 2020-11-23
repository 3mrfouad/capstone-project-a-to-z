using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AZLearn.Data;
using AZLearn.Models;

namespace AZLearn.Controllers
{
   
    public class UserController :ControllerBase
    {
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

    }
}
