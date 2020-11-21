using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AZLearn.Controllers
{
    public class CourseController :ControllerBase
    { 
        
        public void CreateCourseByCohortId(string CohortId){}
       public  Cohort GetCourseById(string CoursetId){}

       public List<Course> GetCoursesByCohortId(string CohortId)
       {

       }
        public void UpdateCourseById(string CourseId){}

        public void ArchiveCourseById(string CourseId){}



    }
}
