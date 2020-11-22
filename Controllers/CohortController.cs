using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AZLearn.Models;

namespace AZLearn.Controllers
{
    public class CohortController :ControllerBase
    {
        public void CreateCohort(){}
       public  Cohort GetCohortById(string chortId){}

       public List<Cohort> GetCohorts()
       {
       }

       public void UpdateCohortById(string cohortId){}

       public void ArchiveCohortById(string cohortId){}

    }
}
