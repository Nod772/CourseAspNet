using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseASP.NET.Models
{
    public class AdminViewModel
    {
        public IEnumerable<string> Roles { get; set; }
        public IEnumerable<UserViewModel> Users { get; set; }
    }
}