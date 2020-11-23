using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AZLearn.Data;
using AZLearn.Models;

namespace AZLearn.Controllers
{
    public class HomeworkController : ControllerBase
    {
        /// <summary>
        /// This action takes in Course Id and Cohort Id and returns List of Homeworks for specified course under the specified Cohort Id
        /// </summary>
        /// <param name="courseId">Course Id</param>
        /// <param name="cohortId">Cohort Id</param>
        /// <returns>List of Homeworks for specified course under the specified Cohort Id</returns>
        public static List<Homework> GetHomeworksByCourseId(string courseId, string cohortId)
        {
            List<Homework> homeworks;
            int parsedCohortId = int.Parse(cohortId);
            int parsedCourseId = int.Parse(courseId);
            using (AppDbContext context = new AppDbContext())
            {
                homeworks = context.Homeworks
                    .Where(key => key.CourseId == parsedCourseId && key.CohortId == parsedCohortId).ToList();
            }
            return homeworks;
        }
    }
}
