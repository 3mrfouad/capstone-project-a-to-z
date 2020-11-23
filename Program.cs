using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AZLearn.Controllers;
using AZLearn.Models;

namespace AZLearn
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /* Test GetGradeSummaryForInstructor Action method from Grade Controller */
            var gradeSummaryList = GradeController.GetGradeSummaryForInstructor("1", "1");
            foreach (var gradeSummary in gradeSummaryList)
            {
                Debug.WriteLine(gradeSummary.StudentName+ " "+ gradeSummary.TotalMarks + " "+ gradeSummary.MarksInRequirement + " " + gradeSummary.MarksInChallenge + " "+ gradeSummary.TotalTimeSpentOnHomework);
            }

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
