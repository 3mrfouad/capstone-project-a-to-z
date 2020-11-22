using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AZLearn.Controllers;


namespace AZLearn
{
    public class Program
    {
        public static void Main(string[] args)
        {

            #region Testing Controllers Actions:

            #region Create Course Testing
            /*CourseController.CreateCourseByCohortId("1", "1","React","React Basics","3","https://reactjs.org/tutorial/tutorial.html", "2020-08-10","2020-08-10" );*/
            /*Test Passed*/
            #endregion

            #region Update Course Testing
            CourseController.UpdateCourseById("3","1","React","React Props","5","https://reactjs.org/tutorial/tutorial.html");
            /*Test Passed*/
            #endregion

            #endregion Testing Controllers Action

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
