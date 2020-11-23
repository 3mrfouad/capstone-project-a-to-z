using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AZLearn.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AZLearn.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApplicationController :ControllerBase
    {
        /// <summary>
        /// GetCourseSummary
        /// Description:The API End Point looks for action GetCoursesByCohortID in CourseController and retrieves the information of all courses based on the Cohort id  from database.
        /// EndPoint Testing : localhost:xxxxx/application/getcoursesummary?cohortId=1
        /// /*Test Passed*/
        /// </summary>
        /// <param name="cohortId"></param>
        /// <returns></returns>
        [HttpGet(nameof(GetCourseSummary))]
        public ActionResult< List< Course>> GetCourseSummary(string cohortId)
        {
            var coursesList= CourseController.GetCoursesByCohortId(cohortId);
            return coursesList;
        }

        /// <summary>
        /// GetCourses
        /// Description:The API End Point looks for action GetCourses and retrieves the information of all courses from database.
        /// EndPoint Testing : localhost:xxxxx/application/GetCourses
        /// /*Test Passed*/
        /// </summary>
        /// <returns>The API End Point returns list of all Courses in database.Tested successfully in Postman</returns>
        [HttpGet(nameof(GetCourses))]
        public ActionResult<List<Course>>GetCourses()
        {
            return CourseController.GetCourses();
        }

        /// <summary>
        /// AssignCourseByCohortId  *********TEST FAILED ERROR MESSAGE IN POSTMAN*******
        /// Description:he API End Point looks for action AssignCourseByCohortId in CourseController and retrieves the information of the course from database according to specified Cohortid.
        /// EndPoint Testing : localhost:xxxxx/application/AssignCourseByCohortId
        /// </summary>
        /// <param name="cohortId"></param>
        /// <param name="courseId"></param>
        /// <returns>The End Point returns the Course according to the specified cohort id </returns>
        [HttpPost(nameof(AssignCourseByCohortId))]
        public ActionResult<Course> AssignCourseByCohortId(string cohortId,string courseId)
        {
            ActionResult<Course> result;
            try
            {
                CourseController.AssignCourseByCohortId(cohortId,courseId);
                result=StatusCode(200,"Success Message");
            }
            catch
            {
                result=StatusCode(403,"Error Message");
            }
            return result;
        }

        /// <summary>
        /// UpdateTimesheetById
        /// Description:The API End Point looks for action UpdateTimesheetById in TimesheetController and updates the information of the timesheet on database as per specified requested edit parameters.
        /// EndPoint Testing : localhost:xxxxx/application/UpdateTimesheetById
        /// Test Passed
        /// </summary>
        /// <param name="timesheetId"></param>
        /// <param name="solvingTime"></param>
        /// <param name="studyTime"></param>
        /// <returns>The End Point returns Success Message and Updates the Timesheet according to parameters specified </returns>
        [HttpPatch(nameof(UpdateTimesheetById))]
        public ActionResult UpdateTimesheetById(string timesheetId,string solvingTime,string studyTime)
        {
            ActionResult result;
            try
            {
                TimesheetController.UpdateTimesheetById(timesheetId,solvingTime,studyTime);
                result=StatusCode(200,"Success Message");
            }
            catch
            {
                result=StatusCode(403,"Error Message");
            }
            return result;
        }


        /// <summary>
        ///CreateCourseByCohortId
        /// Description:The API End Point looks for action CreateCourseByCohortId in CourseController and creates the course information on database with specified parameters as defined below.
        ///  EndPoint Testing : localhost:xxxxx/application/CreateCourseByCohortId?cohortId=2&instructorId=1&name=PHP&description=Basics of PHP&durationHrs=5&resourcesLink=www.php.com&startDate=2020-10-04&endDate=2020-10-15
        /// Test Passed
        /// </summary>
        /// <param name="cohortId"></param>
        /// <param name="instructorId"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="durationHrs"></param>
        /// <param name="resourcesLink"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [HttpPost(nameof(CreateCourseByCohortId))]
        public ActionResult CreateCourseByCohortId
            (string cohortId,string instructorId,string name,string description,
            string durationHrs,string resourcesLink,string startDate,string endDate)
        {
            ActionResult result;
            try
            {
                CourseController.CreateCourseByCohortId(cohortId,instructorId, name,description,
            durationHrs, resourcesLink,startDate,endDate);
                result=StatusCode(200,"Success Message");
            }
            catch
            {
                result=StatusCode(403,"Error Message");
            }
            return result;
        }







    }
}
