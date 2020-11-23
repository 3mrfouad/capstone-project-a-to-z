using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AZLearn.Data;
using AZLearn.Models;

namespace AZLearn.Controllers
{
    public class TimesheetController :Controller
    {
        /// <summary>
        ///CreateTimesheetByHomeworkId
        /// Description: Controller action that creates new timesheet by HomeworkId
        /// It expects below parameters, and would populate the same as new timesheet in the database
        /// </summary>
        /// <param name="homeworkId"></param>
        /// <param name="studentId"></param>
        /// <param name="solvingTime">string provided from frontend, and parsed to float to match model property data type</param>
        /// <param name="studyTime">string provided from frontend, and parsed to float to match model property data type</param>
        public static void CreateTimesheetByHomeworkId(string homeworkId,string studentId,string solvingTime,string studyTime)
        {
            using var context = new AppDbContext();
            var newTimesheet = new Timesheet
            {
                HomeworkId=int.Parse(homeworkId),
                StudentId=int.Parse(studentId),
                SolvingTime=float.Parse(solvingTime),
                StudyTime=float.Parse(studyTime)
            };

            context.Timesheets.Add(newTimesheet);
            context.SaveChanges();

        }
        /*    public List<Timesheet> GetTimesheets(string homeworkId)
            {
            }*/
    }
}
