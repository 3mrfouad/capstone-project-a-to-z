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
            var timesheet = TimesheetController.GetTimesheetByHomeworkId("1", "3");
            Debug.WriteLine(timesheet.SolvingTime + "  "+ timesheet.StudyTime);
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
