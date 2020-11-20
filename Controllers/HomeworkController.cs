using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AZLearn.Models;

namespace AZLearn.Controllers
{
    public class HomeworkController : ControllerBase
    {
        public void CreateHomeworkByCourseId(string CourseId)
        {
        }

        public Homework GetHomeworkById(string HomeworkId)
        {
        }

       public List<Homework> GetHomeworks(string CourseId)
        {}

       public void UpdateHomeworkById(string HomeworkId)
       {
       }

       public void ArchiveHomeworkById(string HomeworkId)
       {
       }



    }
}
