using CourseASP.NET.Areas.Saller.Models;
using CourseASP.NET.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseASP.NET.Models
{
    public class SallerViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; set; }
        public IEnumerable<OrderViewModel> Orders  { get; set; }
        public IEnumerable<string> Categories { get; set; }
    }
}