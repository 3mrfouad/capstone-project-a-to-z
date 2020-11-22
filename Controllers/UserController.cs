using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AZLearn.Models;


namespace AZLearn.Controllers
{
    public class UserController :ControllerBase
    {
        public User GetUserById(string userId){ }

   public void CreateUserByCohortId(string cohortId){
   }
   public  List<User> GetStudentsByCohortId(string cohortId){}
    public void UpdateUserById(string userId){}


    public void ArchiveUserById(string userId){}

    }
}
