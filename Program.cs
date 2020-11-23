using AZLearn.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace AZLearn
{
    public class Program
    {
        public static void Main(string[] args)
        {
            #region Testing Controllers Actions

            #region Create Course

            /*CourseController.CreateCourseByCohortId("1", "1","React","React Basics","3","https://reactjs.org/tutorial/tutorial.html", "2020-08-10","2020-08-10" );*/
            /*Test Passed*/

            #endregion

            #region Assign CourseByCohort

            /* CourseController.AddCourseByCohortId("1","4");*/
            /*Test Passed (Was able to Assign CourseID 4 to Cohort 1 in Course Cohort*/

            #endregion

            #region Update Course

            /*For testing updated Description and Duration Hours*/
            /*CourseController.UpdateCourseById("3","1","React","React Props","5","https://reactjs.org/tutorial/tutorial.html");*/
            /*Test Passed*/

            #endregion

            #region Get List of All Courses

            /*    var courses = CourseController.GetCourses();
                foreach (Course course in courses)
                {
                    System.Diagnostics.Debug.WriteLine(
                        $"Id:{course.CourseId} Instructor:{course.InstructorId} Name:{course.Name} Description:{course.Description} DurationInHrs:{course.DurationHrs} Resources Link: {course.ResourcesLink} ");
                }*/
            /*Test Passed*/

            #endregion

            #region Get Courses By Cohort

            /*  var coursesList = CourseController.GetCoursesByCohortId("1");
              foreach ( var course in coursesList )
              {
                  System.Diagnostics.Debug.WriteLine(
                      $"Id:{course.CourseId} Instructor:{course.InstructorId} Name:{course.Name} Description:{course.Description} DurationInHrs:{course.DurationHrs} Resources Link: {course.ResourcesLink} ");
              }*/

            #endregion

            #region Get User

            /*var user = UserController.GetUserById("1");
            System.Diagnostics.Debug.WriteLine(
                $"Id:{user.UserId} Name:{user.Name} Email:{user.Email} ");*/
            /*Test Passed*/

            #endregion

            #region Create Timesheet

            TimesheetController.CreateTimesheetByHomeworkId("1","2","60","40");
            /*Test Passed*/

            #endregion

            #region Update Timesheet

            /*For testing updated SolvingTime and StudyTime*/
            /* TimesheetController.UpdateTimesheetByHomeworkId("1","2","50","30");*/
            /*Test */

            #endregion

            #region GetHomeworkById

            /*var homework = HomeworkController.GetHomeworkById("1");
            System.Diagnostics.Debug.WriteLine(
                $"Id:{homework.HomeworkId} Title:{homework.Title} Average Completion Time:{homework.AvgCompletionTime} DueDate:{homework.DueDate} DocumentLink:{homework.DocumentLink} GitHUbCloassroomLink: {homework.GitHubClassRoomLink}");*/
            /*Test Passed*/


            #endregion




            #endregion Testing Controllers Action

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}