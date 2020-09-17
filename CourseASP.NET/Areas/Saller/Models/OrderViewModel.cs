using CourseASP.NET.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseASP.NET.Areas.Saller.Models
{
    public class OrderViewModel
    {
        
        public int Id { get; set; }
        public string SaleDate { get; set; }
        public string Price { get; set; }
        public virtual ICollection<Product> Products { get; set; }


    }
}