using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AZLearn.Controllers
{
    public class CohortController :ControllerBase
    {
        public void CreateCohort(){}
       public  Cohort GetCohortById(string ChortId){}

       public List<Cohort> GetCohorts()
       {
       }

       public void UpdateCohortById(string CohortId){}

       public void ArchiveCohortById(string CohortId){}

    }
}
