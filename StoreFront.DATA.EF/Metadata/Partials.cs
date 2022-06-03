using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreFront.DATA.EF.Models
{
    #region Category
    [ModelMetadataType(typeof(CategoryMetadata))]
    public partial class Category { }
    #endregion

    #region Supplier
    [ModelMetadataType(typeof(SupplierMetadata))]
    public partial class Supplier { }
    #endregion

    #region Product
    [ModelMetadataType(typeof(ProductMetadata))]
    public partial class Product 
    {
        [NotMapped]
        public IFormFile? UploadedImage { get; set; }
    }
    #endregion

    #region OrderProduct
    [ModelMetadataType(typeof(OrderProductMetadata))]
    public partial class OrderProduct { }
    #endregion

    #region Order
    [ModelMetadataType(typeof(OrderMetadata))]
    public partial class Order { }
    #endregion

    #region User
    [ModelMetadataType(typeof(UserMetadata))]
    public partial class User
    { 
        public string FullName {  get { return $"{FirstName} {LastName}";  } }
    }
    #endregion

    #region VersionsProduct
    [ModelMetadataType(typeof(VersionsProduct))]
    public partial class VersionsProduct
    {
        public override string ToString()
        {
            return Version.ToString();
        }
    }

    #endregion
}
