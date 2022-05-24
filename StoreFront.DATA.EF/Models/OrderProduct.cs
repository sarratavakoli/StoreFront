using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class OrderProduct
    {
        public int Id { get; set; }
        public string VersionProductId { get; set; } = null!;
        public int OrderId { get; set; }
        public short UnitQuantity { get; set; }
        public string? UnitType { get; set; }
        public decimal ProductPrice { get; set; }

        public virtual VersionsProduct IdNavigation { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
    }
}
