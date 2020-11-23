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
            /*Testing CreateHomeworkByCourseId*/
            HomeworkController.CreateHomeworkByCourseId("1","1","1","true","To-Do App using PHP", "5", "2020/11/23", "2020/11/20", "http://googledrive", "http://githublink");

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
