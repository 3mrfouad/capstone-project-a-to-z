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
