using System.ComponentModel.DataAnnotations; //grants access to annotations for validation

namespace StoreFront.UI.MVC.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Subject is required")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Message Content is required")]
        [DataType(DataType.MultilineText)] 
        public string Message { get; set; }
    }
}
