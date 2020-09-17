using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourseASP.NET.Areas.Saller.Models
{
    public class CreateProductViewModel
    {
       
        public int Id { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        public string Image { get; set; }
        public string Category { get; set; }
        [Required(ErrorMessage = "Category name is required")]
        public int Price { get; set; }
    }
}