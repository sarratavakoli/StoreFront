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

        public int ID { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int CategoryID { get; set; }
        public int? SupplierID { get; set; }
        public bool IsActive { get; set; }

        public virtual Category? Category { get; set; } = null!;
        public virtual Supplier? Supplier { get; set; }
        public virtual ICollection<VersionsProduct> VersionsProducts { get; set; }
    }
}
