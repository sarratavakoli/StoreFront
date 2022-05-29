using System.ComponentModel.DataAnnotations; //grants access to annotations for validation

namespace StoreFront.UI.MVC.Models
{
    public class ShippingViewModel 
    {
        [Required(ErrorMessage = "Name is required")]
        public string ShipName { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [DataType(DataType.EmailAddress)]
        public string ShipAddress { get; set; }

        
        public string ShipAddress2 { get; set; }

        [Required(ErrorMessage = "City is required")]        
        public string ShipCity { get; set; }

        [StringLength(2, ErrorMessage = "State must be 2 characters.")]
        [Required(ErrorMessage = "State is required")]
        public string ShipState { get; set; }

        [DataType(DataType.PostalCode)]
        [Required(ErrorMessage = "Zip is required")]
        public string ShipZip { get; set; }
    }
}
