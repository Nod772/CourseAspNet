using CourseASP.NET.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourseASP.NET.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public int Price { get; set; }
        public int ID_Category { get; set; }

        /*navigation properties*/
        public Category Category { get; set; }
        public ApplicationUser SallerShop { get; set; }
        public ApplicationUser Client { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        public Product()
        {
            Category = new Category();
            SallerShop = new ApplicationUser();
            Orders = new List<Order>();
            Client = new ApplicationUser();
            ID_Category = Category.Id;
        }
    }
}