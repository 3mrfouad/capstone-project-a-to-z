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

    }
}
