﻿using Microsoft.AspNetCore.Http;
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
    public class ApplicationController_Atinder :Controller
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
        ///     GetHomeworkForStudent
        ///     Request Type: GET
        ///     This End point takes in Homework Id from link clicked and return homework record associated with that Id.
        /// </summary>
        /// <param name="homeworkId"></param>
        /// <returns>Single homework record</returns>
        [HttpGet("Homework")]
        public ActionResult<Homework> GetHomeworkForStudent(string homeworkId)
        {
            return HomeworkController.GetHomeworkById(homeworkId);
        }

        /// <summary>
        ///     GetTimesheetForStudent
        ///     Request Type: GET
        ///     This End point takes in Homework Id from link clicked, Student Id from global store and return associated timesheet record.
        /// </summary>
        /// <param name="homeworkId"></param>
        /// <param name="studentId"></param>
        /// <returns>Single timesheet record</returns>
        [HttpGet("Timesheet")]
        public ActionResult<Timesheet> GetTimesheetForStudent(string homeworkId, string studentId)
        {
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
            return timesheet;
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
        [HttpPost("Timesheet")]
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
    }
}
