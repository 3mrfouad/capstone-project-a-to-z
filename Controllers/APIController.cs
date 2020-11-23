using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using AZLearn.Models;

namespace AZLearn.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class APIController : ControllerBase
    {
       /* [HttpPost("CreateCourse")]
        public ActionResult<Course> CreateCourse_POST(string cohortId, string instructorId, string name,
            string description, string durationHrs, string resourcesLink, string startDate, string endDate)
        {
            ActionResult<Course> result;
            result = CourseController.CreateCourseByCohortId(cohortId, instructorId, name, description, durationHrs,
                resourcesLink, startDate, endDate);
            return result;
        }

        [HttpPatch("Patch")]
        public ActionResult<Course> UpdateCourse_POST(string courseId, string instructorId, string name,
            string description,
            string durationHrs, string resourcesLink)
        {
            ActionResult<Course> result;

            result = CourseController.UpdateCourseById(cohortId, instructorId, name, description,
                durationHrs, resourcesLink, startDate, endDate);

            return result;
        }*/

        [HttpGet("All")]
        public ActionResult<List<Course>> AllCourses_GET()
        {
            return CourseController.GetCourses();
        }



    }
}