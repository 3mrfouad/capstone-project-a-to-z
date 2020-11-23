using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using AZLearn.Controllers;
using AZLearn.Models;

namespace AZLearn
{
    public class Program
    {
        public static void Main(string[] args)
        {
            #region Testing Controllers' Actions

            #region Create Cohort
            /*CohortController.CreateCohort("Cohort 4.1", "20", "Edmonton", "Remote","2020-06-04","2020-08-30" );*/
            /*Test passed*/
            #endregion

            #region Update Cohort
            /*CohortController.UpdateCohortById("1", "Cohort 4.2", "40", "Edmonton", "Remote","2020-06-04","2020-08-30" );*/
            /*Test passed*/
            #endregion

            #region Get Cohorts

            /*var cohorts = CohortController.GetCohorts();
            foreach (Cohort cohort in cohorts)
            {
                System.Diagnostics.Debug.WriteLine($"Id: {cohort.CohortId}  Mode of Teaching: {cohort.ModeOfTeaching}   Capacity: {cohort.Capacity} Name: {cohort.Name} City: {cohort.City} Start Date: {cohort.StartDate}  End Date: {cohort.EndDate}");
            }*/
            /*Test passed*/
            #endregion

            #region Create Grading by Student Id
            /*var grading = new Dictionary<string, Tuple<string, string>>();
            grading.Add("-1", new Tuple<string, string>("1", "Good Job"));
            grading.Add("-2", new Tuple<string, string>("0", "Bad Job"));
            GradeController.CreateGradingByStudentId("-1", grading);*/
            /*Test Pass*/
            #endregion

            #region Update Grading by Student Id (overalload #1)
            /*var grading = new Dictionary<string, Tuple<string, string>>();
            grading.Add("-1", new Tuple<string, string>("1", "ok Job"));
            grading.Add("-2", new Tuple<string, string>("1", "ok Job"));
            GradeController.UpdateGradingByStudentId("-1", grading);*/
            /*Test Pass*/
            #endregion

            #region Update Grading by Student Id (overalload #2)
            /*var grading = new Dictionary<string,string>();
            grading.Add("-1", "Thanks Instructor");
            grading.Add("-2", "Thanks Instructor");
            GradeController.UpdateGradingByStudentId("-1", grading);*/
            /*Test Pass*/
            #endregion

            #region Create Rubrics By Homework Id
            /*var rubrics = new List<Tuple<string, string, string>>();
            rubrics.Add( new Tuple<string, string,string>("false", "Do a practice in so and so","1"));
            rubrics.Add(new Tuple<string, string,string>("true", "Here is more difficult one","2"));
            RubricController.CreateRubricsByHomeworkId("-2",rubrics);*/
            /*Test Pass*/
            #endregion

            #region Update Rubrics By Homework Id

            /*var rubrics = new Dictionary<string, Tuple<string, string, string>>();
            rubrics.Add("3",new Tuple<string, string, string>("false", "Update, Do a practice per classroom demo", "1"));
            rubrics.Add("4",new Tuple<string, string, string>("true", "Here is more difficult one to do", "1"));
            RubricController.UpdateRubricsById(rubrics);*/
            /*Test Pass*/
            #endregion

            #endregion Testing Controllers' Actions

            /*=============================================================================================================================*/

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

            #region Get List of All Instructors

            var instructors = UserController.GetInstructors();
            foreach ( User user in instructors )
            {
                System.Diagnostics.Debug.WriteLine(
                    $"Id:{user.UserId} Instructor Name:{user.Name} Email: {user.Email} Is Instructor:{user.IsInstructor} ");
            }
            /*Test Passed*/

            #endregion

            #region Create Timesheet

            TimesheetController.CreateTimesheetByHomeworkId("1", "2", "60", "40");
            /*Test Passed*/

            #endregion

            #region Update TimesheetById

            /*For testing updated SolvingTime and StudyTime for Timesheet id 2*/
            /*TimesheetController.UpdateTimesheetByTimesheetId("2","50","30");*/
            /*Test Passed*/

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