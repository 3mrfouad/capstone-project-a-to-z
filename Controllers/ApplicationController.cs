using System;
using System.Collections.Generic;
using System.Linq;
using AZLearn.Models;
using AZLearn.Models.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace AZLearn.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApplicationController : Controller
    {
        #region /application/updatestudentfeedback

        [HttpPatch(nameof(UpdateStudentFeedback))]
        public ActionResult UpdateStudentFeedback(string studentId,
            [FromBody] Dictionary<string, string> studentComment)
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
            catch (ValidationException e)
            {
                result = StatusCode(403, "Error while retrieving Cohorts");
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
            catch (ArgumentException e)
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
                CohortController.CreateCohort(name, capacity, city,
                    modeOfTeaching, startDate, endDate);
                result = StatusCode(200, "Successfully Created the Cohort");
            }
            catch (ValidationException e)
            {
                var error = "Error(s) During Creation: " +
                            e.ValidationExceptions.Select(x => x.Message)
                                .Aggregate((x, y) => x + ", " + y);

                result = BadRequest(error);
            }
            catch (Exception e)
            {
                result = StatusCode(500,
                    "Unknown error occurred while creating a Cohort, please try again later or contact Technical Support Team.");
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
                CohortController.UpdateCohortById(cohortId, name, capacity, city,
                    modeOfTeaching, startDate, endDate);
                result = StatusCode(200, "Successfully Updated the Cohort details");
            }
            catch (ValidationException e)
            {
                var error = "Error(s) During Creation: " +
                            e.ValidationExceptions.Select(x => x.Message)
                                .Aggregate((x, y) => x + ", " + y);
                result = BadRequest(error);
            }
            catch (Exception e)
            {
                result = StatusCode(500,
                    "Unknown error occurred while creating a Cohort, please try again later or contact Technical Support Team.");
            }

            return result;
        }

        #endregion

        #region /application/homeworksummary

        /// <summary>     
        ///     GetHomeworkSummary
        ///     Request Type: GET
        ///     This End point takes in Cohort Id and Course Id from global store and return List of homeworks associated with that
        ///     Course for specified Cohort.
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="cohortId"></param>
        /// <returns>List Of Homeworks related to specified Course and Cohort</returns>
        [HttpGet("HomeworkSummary")]
        public ActionResult<IEnumerable<Homework>> GetHomeworkSummary(string courseId, string cohortId)
        {
            return HomeworkController.GetHomeworksByCourseId(courseId, cohortId);
        }

        #endregion

        #region /application/homeworktimesheet

        /// <summary>
        ///     GetHomeworkTimesheetForStudent
        ///     Request Type: GET
        ///     This End point takes in Homework Id from link clicked, Student Id from global store and return associated homework
        ///     record and timesheet record.
        /// </summary>
        /// <param name="homeworkId"></param>
        /// <param name="studentId"></param>
        /// <returns>Tuple of homework record, timesheet record</returns>
        [HttpGet("HomeworkTimesheet")]
        public ActionResult<Tuple<Homework, Timesheet>> GetHomeworkTimesheetForStudent(string homeworkId,
            string studentId)
        {
            var homework = HomeworkController.GetHomeworkById(homeworkId);
            var timesheet = TimesheetController.GetTimesheetByHomeworkId(homeworkId, studentId);
            return new Tuple<Homework, Timesheet>(homework, timesheet);
        }

        #endregion

        #region /application/createtimesheet

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
        public ActionResult CreateTimesheetByHomeworkId(string homeworkId, string studentId, string solvingTime,
            string studyTime)
        {
            ActionResult result;
            try
            {
                TimesheetController.CreateTimesheetByHomeworkId(homeworkId, studentId, solvingTime, studyTime);
                result = StatusCode(200, "Successfully created TimeSheet");
            }
            catch ( ValidationException e )
            {
                var error = "Error(s) During Creation: "+
                            e.ValidationExceptions.Select(x => x.Message)
                                .Aggregate((x,y) => x+", "+y);

                result=BadRequest(error);
            }
            catch ( Exception e )
            {
                result=StatusCode(500,
                    "Unknown error occurred while creating a Timesheet, please try again later or contact Technical Support Team.");
            }

            return result;
        }

        #endregion

        #region /application/InstructorGradeSummary

        /// <summary>
        ///     GetGradeSummaryForInstructor
        ///     Request Type: GET
        ///     This Endpoint returns Grade Summary and Timesheet Summary for all students in a cohort for a specified Homework.
        /// </summary>
        /// <param name="cohortId"></param>
        /// <param name="homeworkId"></param>
        /// <returns>custom object contains GradeSummery and Timesheet Summary for all students for a specified Homework</returns>
        [HttpGet("InstructorGradeSummary")]
        public ActionResult<List<GradeSummaryTypeForInstructor>> GetGradeSummaryForInstructor(string cohortId,
            string homeworkId)
        {
            return GradeController.GetGradeSummaryForInstructor(cohortId, homeworkId);
        }

        #endregion

        #region /application/Grades

        /// <summary>
        ///     GetGrades
        ///     Request Type: GET
        ///     This Endpoint returns Grades of a specified student for a specified course.
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="homeworkId"></param>
        /// <returns>Grades for one student for one course</returns>
        [HttpGet("Grades")]
        public ActionResult<List<Grade>> GetGrades(string studentId, string homeworkId)
        {
            return GradeController.GetGradesByStudentId(studentId, homeworkId);
        }

        #endregion

        #region /application/GetCourseSummary

        /// <summary>
        ///     GetCourseSummary
        ///     Description:The API End Point looks for action GetCoursesByCohortID in CourseController and retrieves the
        ///     information of all courses based on the Cohort id  from database.
        ///     EndPoint Testing : localhost:xxxxx/application/getcoursesummary?cohortId=1
        ///     /*Test Passed*/
        /// </summary>
        /// <param name="cohortId"></param>
        /// <returns></returns>
        [HttpGet(nameof(GetCourseSummary))]
        public ActionResult<List<Course>> GetCourseSummary(string cohortId, string includeInactive)
        {
            var coursesList = CourseController.GetCoursesByCohortId(cohortId, includeInactive);

            return coursesList;
        }

        #endregion

        #region /application/GetCourses

        /// <summary>
        ///     GetCourses
        ///     Description:The API End Point looks for action GetCourses in CourseController and retrieves the information of all
        ///     courses from database.
        ///     EndPoint Testing : localhost:xxxxx/application/GetCourses
        ///     Test Passed
        /// </summary>
        /// <returns>The API End Point returns list of all Courses in database.</returns>
        [HttpGet(nameof(GetCourses))]
        public ActionResult<List<Course>> GetCourses()
        {
            return CourseController.GetCourses();
        }

        #endregion

        #region /application/GetInstructors

        /// <summary>
        ///     GetCourses
        ///     Description:The API End Point looks for action GetInstructors in UserController and retrieves the information of
        ///     all instructors from database.
        ///     EndPoint Testing : localhost:xxxxx/application/GetInstructors
        ///     /*Test Passed*/
        /// </summary>
        /// <returns>The API End Point returns list of all Instructors in database</returns>
        [HttpGet(nameof(GetInstructors))]
        public ActionResult<List<User>> GetInstructors()
        {
            return UserController.GetInstructors();
        }

        #endregion

        #region /application/GetHomeworkForInstructor

        /// <summary>
        ///     GetHomeworkForInstructor
        ///     Description:The API End Point looks for action GetHomeworkById in HomeworkController and GetRubricsByHomeworkId in
        ///     RubricController retrieves the information of the Homework with its rubrics from database.
        ///     https://localhost:xxxxx/application/GetHomeworkForInstructor?homeworkId=-1
        /// </summary>
        /// <param name="homeworkId"></param>
        /// <returns></returns>
        [HttpGet(nameof(GetHomeworkForInstructor))]
        public ActionResult<Tuple<Homework, List<Rubric>, List<User>, List<Course>>> GetHomeworkForInstructor(
            string homeworkId)

        {
            var homework = HomeworkController.GetHomeworkById(homeworkId);

            var rubricsList = RubricController.GetRubricsByHomeworkId(homeworkId);

            var coursesList = CourseController.GetCourses();

            var instructorsList = UserController.GetInstructors();

            return new Tuple<Homework, List<Rubric>, List<User>, List<Course>>(homework, rubricsList, instructorsList, coursesList);
        }
        #endregion

        #region /application/AssignCourseByCohortId

        /// <summary>
        ///     AssignCourseByCohortId
        ///     Description:The API End Point looks for action AssignCourseByCohortId in CourseController and assigns/creates a
        ///     course according to specified Course id and Cohort id .
        ///     EndPoint Testing : //localhost:xxxxx/application/AssignCourseByCohortId?cohortId=2&courseId=3
        ///     Test Passed
        /// </summary>
        /// <param name="cohortId"></param>
        /// <param name="courseId"></param>
        /// <returns>The End Point returns the Course according to the specified cohort id </returns>
        [HttpPost(nameof(AssignCourseByCohortId))]
        public ActionResult AssignCourseByCohortId(string cohortId, string courseId, string instructorId, string startDate, string endDate, string resourcesLink)
        {
            ActionResult result;
            try
            {
                CourseController.AssignCourseByCohortId(cohortId, courseId, instructorId, startDate, endDate, resourcesLink);
                result = StatusCode(200, "Successfully Assigned Course to Cohort");
            }
            catch (ValidationException e)
            {
                var error = "Error(s) During Creation: " +
                            e.ValidationExceptions.Select(x => x.Message)
                                .Aggregate((x, y) => x + ", " + y);

                result = BadRequest(error);
            }
            catch (Exception e)
            {
                result = StatusCode(500, "Unknown error occurred, please try again later."); //Need to add LINK here 
            }

            return result;
            ;
        }
        #endregion

        #region /application/UpdateAssignedCourse

        /// <summary>
        ///     UpdateAssignedCourse
        ///     Description:The API End Point looks for action UpdateAssignedCourse in CourseController and update a
        ///     course according to specified Course id and Cohort id .
        ///     EndPoint Testing : //localhost:xxxxx/application/UpdateAssignedCourse
        ///     Test Passed
        /// </summary>
        /// <param name="cohortId"></param>
        /// <param name="courseId"></param>
        /// <returns> </returns>
        [HttpPost(nameof(AssignCourseByCohortId))]
        public ActionResult UpdateAssignedCourse(string cohortId, string courseId, string instructorId, string startDate, string endDate, string resourcesLink)
        {
            ActionResult result;
            try
            {
                CourseController.UpdateAssignedCourse(cohortId, courseId, instructorId, startDate, endDate, resourcesLink);
                result = StatusCode(200, "Successfully Assigned Course to Cohort");
            }
            catch (ValidationException e)
            {
                var error = "Error(s) During Creation: " +
                            e.ValidationExceptions.Select(x => x.Message)
                                .Aggregate((x, y) => x + ", " + y);

                result = BadRequest(error);
            }
            catch (Exception e)
            {
                result = StatusCode(500, "Unknown error occurred, please try again later."); //Need to add LINK here 
            }

            return result;
            
        }
        #endregion

        #region /application/updatetimesheebyid


        /// <summary>
        ///     UpdateTimesheetById
        ///     Description:The API End Point looks for action UpdateTimesheetById in TimesheetController and updates the
        ///     information of the timesheet on database as per specified requested edit parameters.
        ///     EndPoint Testing : localhost:xxxxx/application/UpdateTimesheetById
        ///     Test Passed
        /// </summary>
        /// <param name="timesheetId"></param>
        /// <param name="solvingTime"></param>
        /// <param name="studyTime"></param>
        /// <returns>The End Point returns Success Message and Updates the Timesheet according to parameters specified </returns>
        [HttpPatch(nameof(UpdateTimesheetById))]
        public ActionResult UpdateTimesheetById(string timesheetId, string solvingTime, string studyTime)
        {
            ActionResult result;
            try
            {
                TimesheetController.UpdateTimesheetById(timesheetId, solvingTime, studyTime);
                result = StatusCode(200, "Successfully Updated Timesheet");
            }
            catch ( ValidationException e )
            {
                var error = "Error(s) During Creation: "+
                            e.ValidationExceptions.Select(x => x.Message)
                                .Aggregate((x,y) => x+", "+y);

                result=BadRequest(error);
            }
            catch ( Exception e )
            {
                result=StatusCode(500,
                    "Unknown error occurred while creating a Timesheet, please try again later or contact Technical Support Team.");
            }

            return result;
        }

        #endregion

        #region /application/createcourse

        /// <summary>
        ///     CreateCourseByCohortId
        ///     Description:The API End Point looks for action CreateCourseByCohortId in CourseController and creates the course
        ///     information on database with specified parameters as defined below.
        ///     EndPoint Testing : localhost:xxxxx/application/CreateCourseByCohortId?cohortId=2&instructorId=1&name=PHP
        ///     &description=Basics of PHP&durationHrs=5&resourcesLink=www.php.com&startDate=2020-10-04&endDate=2020-10-15
        ///     Test Passed
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
        [HttpPost(nameof(CreateCourse))]
        public ActionResult CreateCourse
        (string name, string description,
            string durationHrs)
        {
            ActionResult result;
            try
            {
                CourseController.CreateCourse(name, description,
                    durationHrs);
                result = StatusCode(200, "Successfully Created Course");
            }
            catch (ValidationException e)
            {
                var error = "Error(s) During Creation: " +
                            e.ValidationExceptions.Select(x => x.Message)
                                .Aggregate((x, y) => x + ", " + y);

                result = BadRequest(error);
            }
            catch (Exception e)
            {
                result = StatusCode(500, "Unknown error occurred, please try again later."); //Need to add LINK here 
            }

            return result;
        }


        #endregion

        #region /application/updatecoursebyid

        /// <summary>
        ///     UpdateCourseById
        ///     Description:The API End Point looks for action UpdateCourseById in CourseController and updates the information of
        ///     the course on database as per specified requested edit parameters.
        ///     EndPoint Testing :localhost:xxxxx/application/UpdateCourseById?courseId=6&instructorId=2&name=REDUX&description
        ///     =Basics&durationHrs=2&resourcesLink=www.redux.com
        ///     Test Passed
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="instructorId"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="durationHrs"></param>
        /// <param name="resourcesLink"></param>
        /// <returns></returns>
        [HttpPatch(nameof(UpdateCourseById))]
        public ActionResult UpdateCourseById(string courseId, string name, string description,
            string durationHrs)
        {
            ActionResult result;
            try
            {
                CourseController.UpdateCourseById(courseId, name, description,
                    durationHrs);
                result = StatusCode(200, "Successfully Updated Course");
            }
            catch (ArgumentNullException e)
            {
                result = BadRequest(e.Message);
            }
            catch (ArgumentException e)
            {
                result = BadRequest(e.Message);
            }
            catch (InvalidOperationException e)
            {
                result = NotFound(e.Message);
            }
            catch (KeyNotFoundException e)
            {
                result = NotFound(e.Message);
            }

            return result;
        }

        #endregion
    }
}