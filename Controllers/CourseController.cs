using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AZLearn.Data;
using AZLearn.Models;
using Microsoft.EntityFrameworkCore;

namespace AZLearn.Controllers
{
    public class CourseController :ControllerBase
    {

        public static void CreateCourseByCohortId(string cohortId,string instructorId,string name,string description,string durationHrs,string resourcesLink,string startDate,string endDate)
        {

            using var context = new AppDbContext();
            var newCourse = new Course()
            {

              /*  Create a Course*/
                InstructorId=int.Parse(instructorId),
                Name=name,
                Description=description,
                DurationHrs=float.Parse(durationHrs),
                ResourcesLink =resourcesLink,
            };

            context.Courses.Add(newCourse);
            context.SaveChanges();


            /*Create a Join between Course and Cohort by Creating an object*/
            var newCohortCourse = new CohortCourse()
            {
                CohortId = int.Parse(cohortId),
                CourseId = newCourse.CourseId,
                StartDate = DateTime.Parse(startDate),
                EndDate = DateTime.Parse(endDate),
            };
            context.CohortCourses.Add(newCohortCourse);

            context.SaveChanges();
        }

        /*Update a Course CourseById:*/
        public static void UpdateCourseById(string courseId,string instructorId,string name,string description,string durationHrs,string resourcesLink)
        {
            var parsedCourseId=int.Parse(courseId);
            using var context = new AppDbContext();
            {
                var course = context.Courses.SingleOrDefault(key => key.CourseId == parsedCourseId);

              course.InstructorId = int.Parse(instructorId);
              course.Name = name;
              course.Description = description;
              course.DurationHrs = float.Parse(durationHrs);
              course.ResourcesLink = resourcesLink;

            }
            context.SaveChanges();
        }



       // public Cohort GetCoursesByCohortId(string cohortId) { }

        //public List<Course> GetCoursesByCohortId(string cohortId){}
        //public void ArchiveCourseById(string courseId){}



    }
}
