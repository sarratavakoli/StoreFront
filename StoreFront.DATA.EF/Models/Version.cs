using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class Version
    {
        public Version()
        {
            VersionsProducts = new HashSet<VersionsProduct>();
        }

        public int ID { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<VersionsProduct> VersionsProducts { get; set; }
    }
}
