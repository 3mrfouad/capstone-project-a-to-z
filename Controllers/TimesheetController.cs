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
       public void CreateTimesheetByHomeworkId(string homeworkId)
        {
        }
       public List<Timesheet> GetTimesheets(string homeworkId){}


       public void UpdateTimesheetById(string timesheetId){}

      public void ArchiveTimesheetsByHomeworkId(string homeworkId){}

    }
}
