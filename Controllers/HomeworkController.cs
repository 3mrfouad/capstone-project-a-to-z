using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AZLearn.Data;
using AZLearn.Models;

namespace AZLearn.Controllers
{
    public class HomeworkController :Controller
    {
        /// <summary>
        /// GetHomeworkById
        /// Description: Controller action that gets Homework information by the associated HomeworkId
        /// It expects below parameters, and would populate the user information according to the parameter specified
        /// </summary>
        /// <param name="homeworkId"></param>
        /// <returns>It returns the Homework Information based on the homework id </returns>
        public static Homework GetHomeworkById(string homeworkId)
        {
            Homework result;
            var parsedHomeworkId = int.Parse(homeworkId);
            using var context = new AppDbContext();
            {
                result=context.Homeworks.Single(key => key.HomeworkId==parsedHomeworkId);
            }
            return result;
        }
    }
}
