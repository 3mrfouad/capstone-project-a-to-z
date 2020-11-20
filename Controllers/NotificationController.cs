using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AZLearn.Models;

namespace AZLearn.Controllers
{
    public class NotificationController :ControllerBase
    {
        public void CreateNotificationByHomeworkId(string StudentId){}
       
        public List<Notification> GetNotifications(string StudentId) { }


        public void ArchiveNotificationsByStudentId(string StudentId) { }
    }
}
