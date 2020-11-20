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
        public User GetUserById(string UserId){ }

   public void CreateUserByCohortId(string CohortId){
   }
   public  List<User> GetStudentsByCohortId(string CohortId){}
    public void UpdateUserById(string UserId){}


    public void ArchiveUserById(string UserId){}

    }
}
