using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using AZLearn.Models;

namespace AZLearn.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApplicationController : Controller
    {

        #region /application/updatestudentfeedback

        [HttpPatch(nameof(UpdateStudentFeedback))]
        public ActionResult UpdateStudentFeedback(string studentId, [FromBody] Dictionary<string, string> studentComment)
        {
            ActionResult result;
            try
            {
                GradeController.UpdateGradingByStudentId(studentId, studentComment);
                result = StatusCode(200, "Success Message");
            }
            catch
            {
                result = StatusCode(403, "Error Message");
            }
            return result;
        }

        #endregion

        #region /application/creategrading

        [HttpPost(nameof(CreateGrading))]
        public ActionResult CreateGrading(string studentId,
            [FromBody] Dictionary<string, Tuple<string, string>> gradings)
        {
            ActionResult result;
            try
            {
                GradeController.CreateGradingByStudentId(studentId, gradings);
                result = StatusCode(200, "Success Message");
            }
            catch
            {
                result = StatusCode(403, "Error Message");
            }
            return result;
        }

        #endregion

        #region /application/updategrading

        [HttpPatch(nameof(UpdateGrading))]
        public ActionResult UpdateGrading(string studentId,
            Dictionary<string, Tuple<string, string>> gradings)
        {
            ActionResult result;
            try
            {
                GradeController.UpdateGradingByStudentId(studentId, gradings);
                result = StatusCode(200, "Success Message");
            }
            catch
            {
                result = StatusCode(403, "Error Message");
            }
            return result;
        }

        #endregion

        #region /application/getcohorts

        [HttpGet(nameof(GetCohorts))]
        public ActionResult<List<Cohort>> GetCohorts()
        {
            ActionResult<List<Cohort>> result;
            try
            {
                result = CohortController.GetCohorts();
            }
            catch
            {
                result = StatusCode(403, "Error Message");
            }
            return result;
        }

        #endregion

        #region /application/createhomework

        [HttpPost(nameof(CreateHomework))]
        public ActionResult CreateHomework(string courseId, string instructorId, string cohortId,
            string isAssignment, string title, string avgCompletionTime, string dueDate, string releaseDate,
            string documentLink, string gitHubClassRoomLink)
        {
            ActionResult result;
            try
            {
                HomeworkController.CreateHomeworkByCourseId(courseId, instructorId, cohortId,
                    isAssignment, title, avgCompletionTime, dueDate, releaseDate,
                    documentLink, gitHubClassRoomLink);
                result = StatusCode(200, "Success Message");
            }
            catch
            {
                result = StatusCode(403, "Error Message");
            }
            return result;
        }

        #endregion

        #region /application/updatehomework

        [HttpPatch(nameof(UpdateHomework))]
        public ActionResult UpdateHomework(string homeworkId, string courseId, string instructorId, string cohortId,
            string isAssignment, string title, string avgCompletionTime, string dueDate, string releaseDate,
            string documentLink, string gitHubClassRoomLink)
        {
            ActionResult result;
            try
            {
                HomeworkController.UpdateHomeworkById(homeworkId, courseId, instructorId, cohortId,
                 isAssignment, title, avgCompletionTime, dueDate, releaseDate,
                 documentLink, gitHubClassRoomLink);
                result = StatusCode(200, "Success Message");
            }
            catch
            {
                result = StatusCode(403, "Error Message");
            }
            return result;
        }

        /*UpdateHomeworkById(string homeworkId, string courseId, string instructorId, string cohortId,
                    string isAssignment, string title, string avgCompletionTime, string dueDate, string releaseDate,
                    string documentLink, string gitHubClassRoomLink)*/

        #endregion

        #region /application/createcohort

        [HttpPost(nameof(CreateCohort))]
        public ActionResult CreateCohort(string name, string capacity, string city,
            string modeOfTeaching, string startDate, string endDate)
        {
            ActionResult result;
            try
            {
                CohortController.CreateCohort( name,  capacity,  city,
                 modeOfTeaching,  startDate,  endDate);
                result = StatusCode(200, "Success Message");
            }
            catch
            {
                result = StatusCode(403, "Error Message");
            }
            return result;
        }

        #endregion

        #region /application/updatecohort

        [HttpPatch(nameof(UpdateCohort))]
        public ActionResult UpdateCohort(string cohortId, string name, string capacity, string city,
            string modeOfTeaching, string startDate, string endDate)
        {
            ActionResult result;
            try
            {
                CohortController.UpdateCohortById(cohortId,name, capacity, city,
                 modeOfTeaching, startDate, endDate);
                result = StatusCode(200, "Success Message");
            }
            catch
            {
                result = StatusCode(403, "Error Message");
            }
            return result;
        }


        #endregion

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
