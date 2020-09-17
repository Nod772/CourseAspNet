﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourseASP.NET.Areas.Admin.Models
{
    public class CategoriesViewModel
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        public string Name { get; set; }
    }
}