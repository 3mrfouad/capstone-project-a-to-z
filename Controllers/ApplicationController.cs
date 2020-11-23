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

       
        // EndPoint Testing : application/getcoursesummary
        [HttpGet(nameof(GetCourseSummary))]
        public ActionResult< List< Course>> GetCourseSummary(string cohortId)
        {
            var coursesList= CourseController.GetCoursesByCohortId(cohortId);
            return coursesList;
        }

        //Method 2 : Passing Userid as well  /*check with atinder why we had user id  for CourseSummary*/
        /// <summary>
        /// GetCourseSummary
        /// Description:The API End Point looks for action GetCoursesByCohortID and GetUserByID and retrieves the information of all courses and users from database.
        /// EndPoint Testing : localhost:xxxxx/application/getcourse_summary?cohortId=1&userId=2
        /// /*Test Passed*/
        /// </summary>
        /// <param name="cohortId"></param>
        /// <param name="userId"></param>
        /// <returns>It returns af list of courses and Users </returns>
        [HttpGet(nameof(GetCourse_Summary))]
        public ActionResult<Tuple<List<Course>,User>> GetCourse_Summary(string cohortId,string userId)
        {
            var coursesList = CourseController.GetCoursesByCohortId(cohortId);
            var user = UserController.GetUserById(userId);
            return new Tuple<List<Course>,User>(coursesList,user);
        }

        /// <summary>
        /// GetCourses
        /// Description:The API End Point looks for action GetCourses and retrieves the information of all courses from database.
        /// EndPoint Testing : localhost:xxxxx/application/GetCourses
        /// /*Test Passed*/
        /// </summary>
        /// <returns>The API End Point returns list of all Courses in database.Tested successfully in Postman</returns>
        [HttpGet("GetCourses")]
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
        [HttpPost("AssignCourseByCohortId")]
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
        [HttpPatch("UpdateTimesheetById")]
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





    }
}
