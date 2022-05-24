using StoreFront.DATA.EF.Models;

namespace StoreFront.UI.MVC.Models
{
    public class CartItemViewModel
    {
        public int Qty { get; set; }
        public Product Product { get; set; }//containment - the use of a complex data type as a field/prop in a class
        //complex data type - any class with multiple properties

        public CartItemViewModel(int qty, Product product)
        {
            Qty = qty;
            Product = product;
        }

    }
}
