using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AZLearn.Models;

namespace AZLearn.Controllers
{
    public class GradeController :ControllerBase
    {
        public void CreateGradeByRubricId(string RubricId)
        {
        }

       public List<Grade> GetGrades(string HomeworkId)
        {
        }

       public void UpdateGradeById(string GradeId){}

       public void ArchiveGradesByHomeworkId(string HomeworkId){}


    }
}
