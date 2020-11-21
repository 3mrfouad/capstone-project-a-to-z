using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AZLearn.Models;

namespace AZLearn.Controllers
{
    public class TimesheetController :ControllerBase
    {
       public void CreateTimesheetByHomeworkId(string HomeworkId)
        {
        }
       public List<Timesheet> GetTimesheets(string HomeworkId){}


       public void UpdateTimesheetById(string TimesheetId){}

      public void ArchiveTimesheetsByHomeworkId(string HomeworkId){}

    }
}
