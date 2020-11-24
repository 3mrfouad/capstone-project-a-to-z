using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AZLearn.Models;

namespace AZLearn.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApplicationController :Controller
    {
        /// <summary>     
        ///     GetHomeworkSummary
        ///     Request Type: GET
        ///     This End point takes in Cohort Id and Course Id from global store and return List of homeworks associated with that Course for specified Cohort.
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="cohortId"></param>
        /// <returns>List Of Homeworks related to specified Course and Cohort</returns>
        [HttpGet("HomeworkSummary")]
        public ActionResult<IEnumerable<Homework>> GetHomeworkSummary(string courseId, string cohortId)
        {
            return HomeworkController.GetHomeworksByCourseId(courseId, cohortId);
        }

        /// <summary>
        ///     GetHomeworkTimesheetForStudent
        ///     Request Type: GET
        ///     This End point takes in Homework Id from link clicked, Student Id from global store and return associated homework record and timesheet record.
        /// </summary>
        /// <param name="homeworkId"></param>
        /// <param name="studentId"></param>
        /// <returns>Tuple of homework record, timesheet record</returns>
        [HttpGet("HomeworkTimesheet")]
        public ActionResult<Tuple<Homework, Timesheet>> GetHomeworkTimesheetForStudent(string homeworkId, string studentId)
        {
            var homework = HomeworkController.GetHomeworkById(homeworkId);
            var timesheet = TimesheetController.GetTimesheetByHomeworkId(homeworkId, studentId);
            if (timesheet == null)
            {
                int parsedHomeworkId = int.Parse(homeworkId);
                int parsedStudentId = int.Parse(studentId);
                timesheet = new Timesheet()
                {
                    TimesheetId = 0,
                    HomeworkId = parsedHomeworkId,
                    StudentId = parsedStudentId,
                    SolvingTime = 0,
                    StudyTime = 0,
                    Archive = false
                };
            }
            return new Tuple<Homework, Timesheet>(homework, timesheet);
        }

        /// <summary>
        ///     CreateTimesheetByHomeworkId
        ///     Request Type: POST
        ///     Ths Endpoint takes in the below parameters and create a Timesheet record in the DB.
        /// </summary>
        /// <param name="homeworkId"></param>
        /// <param name="studentId"></param>
        /// <param name="solvingTime"></param>
        /// <param name="studyTime"></param>
        /// <returns>Success/Error message</returns>
        [HttpPost("CreateTimesheet")]
        public ActionResult CreateTimesheetByHomeworkId(string homeworkId, string studentId, string solvingTime, string studyTime)
        {
            ActionResult result;
            try
            {
                TimesheetController.CreateTimesheetByHomeworkId(homeworkId, studentId, solvingTime, studyTime);
                result = StatusCode(200, "Success Message");
            }
            catch
            {
                result = StatusCode(403, "Error Message");
            }
            return result;
        }
        
        /// <summary>
        ///     GetGradeSummaryForInstructor
        ///     Request Type: GET
        ///     This Endpoint returns Grade Summary and Timesheet Summary for all students in a cohort for a specified Homework.
        /// </summary>
        /// <param name="cohortId"></param>
        /// <param name="homeworkId"></param>
        /// <returns>custom object contains GradeSummery and Timesheet Summary for all students for a specified Homework</returns>
        [HttpGet("InstructorGradeSummary")]
        public ActionResult<List<GradeSummaryTypeForInstructor>> GetGradeSummaryForInstructor(string cohortId, string homeworkId)
        {
            return GradeController.GetGradeSummaryForInstructor(cohortId, homeworkId);
        }

        /// <summary>
        ///     GetGrades
        ///     Request Type: GET
        /// This Endpoint returns Grades of a specified student for a specified course.
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="homeworkId"></param>
        /// <returns>Grades for one student for one course</returns>
        [HttpGet("Grades")]
        public ActionResult<List<Grade>> GetGrades(string studentId, string homeworkId)
        {
            return GradeController.GetGradesByStudentId(studentId, homeworkId);
        }
    }
}
