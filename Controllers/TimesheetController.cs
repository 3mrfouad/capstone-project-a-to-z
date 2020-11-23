using System.Linq;
using AZLearn.Data;
using AZLearn.Models;
using Microsoft.AspNetCore.Mvc;

namespace AZLearn.Controllers
{
    public class TimesheetController : ControllerBase
    {
        /// <summary>
        ///     CreateTimesheetByHomeworkId
        ///     Description: Controller action that creates new time-sheet by HomeworkId
        ///     It expects below parameters, and would populate the same as new time-sheet in the database
        /// </summary>
        /// <param name="homeworkId"></param>
        /// <param name="studentId"></param>
        /// <param name="solvingTime">string provided from frontend, and parsed to float to match model property data type</param>
        /// <param name="studyTime">string provided from frontend, and parsed to float to match model property data type</param>
        public static void CreateTimesheetByHomeworkId(string homeworkId, string studentId, string solvingTime,
            string studyTime)
        {
            using var context = new AppDbContext();
            var newTimesheet = new Timesheet
            {
                HomeworkId = int.Parse(homeworkId),
                StudentId = int.Parse(studentId),
                SolvingTime = float.Parse(solvingTime),
                StudyTime = float.Parse(studyTime)
            };

            context.Timesheets.Add(newTimesheet);
            context.SaveChanges();
        }

        /// <summary>
        ///     Update Time-sheet by TimesheetId
        ///     Description: Controller action that updates existing time-sheet by TimesheetId
        ///     It expects below parameters, and would populate the time-sheet by given homework id in the database.
        ///     Assumption:
        ///     The frontend view would populate the time-sheet information first through API call
        ///     User will edit as needed
        ///     Frontend will send update API call to backend with all keys to update database
        /// </summary>
        /// <param name="timesheetId"></param>
        /// <param name="solvingTime"></param>
        /// <param name="studyTime"></param>
        public static void UpdateTimesheetById(string timesheetId, string solvingTime, string studyTime)
        {
            var parsedTimesheetId = int.Parse(timesheetId);
            using var context = new AppDbContext();
            var timesheet = context.Timesheets.Find(int.Parse(timesheetId));
            timesheet.SolvingTime = float.Parse(solvingTime);
            timesheet.StudyTime = float.Parse(studyTime);
            context.SaveChanges();
        }

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
