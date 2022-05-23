using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreFront.DATA.EF
{
    /// <summary>
    /// Tables that have been skipped:   
    /// --Versions_Products (Joins main product with the variation/version like color/size/etc)
    /// --Order_Products (Joins tables so that each item in an order has its own row)     
    /// </summary>
    public class CategoryMetadata
    {
        public int ID { get; set; }
                
        [Required]
        [StringLength(50, ErrorMessage = "Must not exceed 50 characters")]
        public string Name { get; set; } = null!;
    }
    public class SupplierMetadata
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Must not exceed 100 characters")]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(150, ErrorMessage = "Must not exceed 150 characters")]        
        public string Address { get; set; } = null!;

        [StringLength(100, ErrorMessage = "Must not exceed 100 characters")]
        [Display(Name = "Address Line 2")]
        public string? Address2 { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Must not exceed 100 characters")]
        public string City { get; set; } = null!;

        [StringLength(2, ErrorMessage = "Must not exceed 2 characters")]
        public string? State { get; set; }

        [StringLength(10, ErrorMessage = "Must not exceed 10 characters")]
        [DataType(DataType.PostalCode)]
        public string? Zip { get; set; }

        [StringLength(50, ErrorMessage = "Must not exceed 50 characters")]
        public string? Country { get; set; }

        [StringLength(24, ErrorMessage = "Must not exceed 24 characters")]
        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }
    }
    public class ProductMetadata
    {
        public int ID { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Must not exceed 200 characters")]
        public string Name { get; set; } = null!;

        [StringLength(500, ErrorMessage = "Must not exceed 500 characters")]
        public string? Description { get; set; }

        //Foreign Key - no metadata required
        public int CategoryID { get; set; }
        //Foreign Key - no metadata required
        public int? SupplierID { get; set; }
    }
    public class OrderMetadata
    {
        public int ID { get; set; }

        //Foreign Key - no metadata required
        public int UserID { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Must not exceed 100 characters")]
        [Display(Name = "Ship To Name")]
        public string ShipName { get; set; } = null!;

        [Required]
        [StringLength(150, ErrorMessage = "Must not exceed 150 characters")]
        [Display(Name = "Address")]
        public string ShipAddress { get; set; } = null!;

        [Display(Name = "Address Line 2")]
        [StringLength(100, ErrorMessage = "Must not exceed 100 characters")]
        public string? ShipAddress2 { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Must not exceed 50 characters")]
        public string ShipCity { get; set; } = null!;

        [Required]
        [StringLength(2, ErrorMessage = "Must not exceed 2 characters")]
        [Display(Name = "State")]
        public string ShipState { get; set; } = null!;

        [Required]
        [StringLength(10, ErrorMessage = "Must not exceed 10 characters")]
        [DataType(DataType.PostalCode)]
        [Display(Name = "Zip")]
        public string ShipZip { get; set; } = null!;
    }
    public class UserMetadata
    {
        public int ID { get; set; }

        [StringLength(50, ErrorMessage = "Must not exceed 50 characters")]
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Must not exceed 50 characters")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;

        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(100, ErrorMessage = "Must not exceed 100 characters")]
        [Display(Name = "Email Address")]
        public string? Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [StringLength(15, ErrorMessage = "Must not exceed 15 characters")]
        [Display(Name = "Phone Number")]
        public string? Phone { get; set; }
    }
    public class VersionMetadata
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Must not exceed 50 characters")]
        public string Name { get; set; } = null!;
    }
}
