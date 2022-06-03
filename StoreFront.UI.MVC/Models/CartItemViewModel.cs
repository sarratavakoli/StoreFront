using StoreFront.DATA.EF.Models;

namespace StoreFront.UI.MVC.Models
{
    public class CartItemViewModel
    {
        public int Qty { get; set; }

        public int VProductId { get; set; }
        //public VersionsProduct VProduct { get; set; }
        //containment - the use of a complex data type as a field/prop in a class

        public CartItemViewModel(int qty, int vProductId)
        {
            Qty = qty;
            VProductId = vProductId;
        }

    }
}
