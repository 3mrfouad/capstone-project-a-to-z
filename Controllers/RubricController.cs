using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AZLearn.Data;
using AZLearn.Models;

namespace AZLearn.Controllers
{
    public class RubricController :ControllerBase
    {
        /// <summary>
        /// This action takes in Homework Id and returns List of Rubrics associated with that Homework Id
        /// </summary>
        /// <param name="homeworkId">Homework Id</param>
        /// <returns>List of Rubrics associated with specified Homework Id</returns>
        public static List<Rubric> GetRubricsByHomeworkId(string homeworkId)
        {
            int parsedHomeworkId = int.Parse(homeworkId);
            List<Rubric> rubrics = new List<Rubric>();
            using var context = new AppDbContext();
            rubrics = context.Rubrics.Where(key => key.HomeworkId == parsedHomeworkId).ToList();
            return rubrics;
        }
    }
}
