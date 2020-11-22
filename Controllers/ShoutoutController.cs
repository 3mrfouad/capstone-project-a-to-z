using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AZLearn.Models;

namespace AZLearn.Controllers
{
    public class ShoutOutController :ControllerBase
    {
        public void CreateShoutOutByHomeworkId(string studentId)
        {
        }
        public List<ShoutOut> GetShoutOuts(string studentId) { }


        public void UpdateShoutOutById(string studentId) { }

        public void ArchiveShoutOutsByStudentId(string studentId) { }

    }
}
