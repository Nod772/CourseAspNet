using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CourseASP.NET.Entities
{
    public class Category
    {

        public int Id { get; set; }
        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }


        public Category()
        {
            Products = new List<Product>();
        }
    }
}