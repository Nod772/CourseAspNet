using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CourseASP.NET.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string SaleDate { get; set; }
        public string Price { get; set; }
        public virtual ICollection<Product> Products { get; set; }


        public Order()
        {
            Products = new List<Product>();
        }

    }
}