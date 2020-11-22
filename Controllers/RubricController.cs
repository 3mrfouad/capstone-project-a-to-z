using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AZLearn.Models;

namespace AZLearn.Controllers
{
    public class RubricController :ControllerBase
    {
       public void CreateRubricByHomeworkId(string HomeworkId){}
       public List<Rubric> GetRubrics(string HomeworkId){}

       public void UpdateRubricById(string RubricId){}

      public void ArchiveRubricsByHomeworkId(string HomeworkId)
       {
       }
    }
}
