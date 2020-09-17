using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourseASP.NET.Areas.Admin.Models
{
    public class CreateRoleViewModel
    {

        [Required(ErrorMessage = "Role name is required")]
        public string Name { get; set; }
    }
}