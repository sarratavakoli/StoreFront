using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int ID { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }
    }
}
