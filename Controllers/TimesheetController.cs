using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AZLearn.Data;
using AZLearn.Models;

namespace AZLearn.Controllers
{

    public class TimesheetController :ControllerBase
    {
        /// <summary>
        /// This Action returns timesheet of a specified student for a specified Homework.
        /// </summary>
        /// <param name="homeworkId">Homework Id</param>
        /// <param name="studentId">Student Id</param>
        /// <returns></returns>
        public static Timesheet GetTimesheetByHomeworkId(string homeworkId, string studentId)
        {
            int parsedHomeworkId = int.Parse(homeworkId);
            int parsedStudentId = int.Parse(studentId);

            using var context = new AppDbContext();
            Timesheet timesheet = context.Timesheets.SingleOrDefault(key =>
                key.HomeworkId == parsedHomeworkId && key.StudentId == parsedStudentId);
            return timesheet;
        }
    }
}
