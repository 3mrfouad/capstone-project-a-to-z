using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AZLearn.Controllers
{
    public class ShoutOutController :ControllerBase
    {
        public void CreateShoutOutByHomeworkId(string StudentId)
        {
        }
        public List<ShoutOut> GetShoutOuts(string StudentId) { }


        public void UpdateShoutOutById(string StudentId) { }

        public void ArchiveShoutOutsByStudentId(string StudentId) { }

    }
}
