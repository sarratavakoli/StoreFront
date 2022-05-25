using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class Product
    {
        public Product()
        {
            VersionsProducts = new HashSet<VersionsProduct>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public int? SupplierId { get; set; }
        public bool? IsActive { get; set; }
        public decimal Price { get; set; }
        public string? Image { get; set; }

        public virtual Category? Category { get; set; } 
        public virtual Supplier? Supplier { get; set; }
        public virtual ICollection<VersionsProduct> VersionsProducts { get; set; }
    }
}
