using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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

            #region Update Grading by Student Id (overalload #1)
            var grading = new Dictionary<string,string>();
            grading.Add("-1", "Thanks Instructor");
            grading.Add("-2", "Thanks Instructor");
            GradeController.UpdateGradingByStudentId("-1", grading);
            /*Test Pass*/
            #endregion

            #endregion Testing Controllers' Actions

            /*=============================================================================================================================*/

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
