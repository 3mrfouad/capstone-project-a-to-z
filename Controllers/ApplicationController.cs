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
        /// </summary>
        /// <returns>The API End Point returns list of all Courses in database.Tested successfully in Postman</returns>
        [HttpGet("GetCourses")]
        public ActionResult<List<Course>>GetCourses()
        {
            return CourseController.GetCourses();
        }


    }
}
