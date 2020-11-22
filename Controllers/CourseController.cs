using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AZLearn.Data;
using AZLearn.Models;

namespace AZLearn.Controllers
{
    public class CourseController :ControllerBase
    {

        public static void CreateCourseByCohortId(string cohortId,string instructorId,string name,string description,string durationHrs,string resourcesLink,string startDate,string endDate)
        {

            using var context = new AppDbContext();
            var newCourse = new Course()
            {

              //  Create A Course
                InstructorId=int.Parse(instructorId),
                Name=name,
                Description=description,
                DurationHrs=float.Parse(durationHrs),
                ResourcesLink =resourcesLink,
            };

            context.Courses.Add(newCourse);
            context.SaveChanges();


            //Create a Join between Course and Cohort by Creating an object
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
        /*
                public void UpdateCourseById(string courseId) { }



                public Cohort GetCoursesByCohortId(string cohortId) { }
        */
        //public List<Course> GetCoursesByCohortId(string cohortId){}
        //public void ArchiveCourseById(string courseId){}



    }
}
