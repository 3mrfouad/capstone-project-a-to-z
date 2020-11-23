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
