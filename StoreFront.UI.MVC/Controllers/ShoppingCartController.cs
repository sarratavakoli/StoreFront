using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using StoreFront.DATA.EF.Models;
using StoreFront.UI.MVC.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace StoreFront.UI.MVC.Controllers
{
    public class ShoppingCartController : Controller
    {
        #region Steps to Implement Session Based Shopping Cart

        /*
         * 1) Register Session in program.cs (builder.Services.AddSession... && app.UseSession())
         * 2) Create the CartItemViewModel class in [ProjName].UI.MVC/Models Folder
         * 3) Add the 'Add to Cart' button in the Index and/or Details view of your products
         * 4) Create the ShoppingCartController (from empty new controller)
         *      - add using statements to Controller
         *          - ProjName.DATA.EF.Models 
         *          - ProjName.UI.MVC.Models
         *          - Microsoft.AspNetCore.Identity
         *          - Newtonsoft.Json (manage cart)
         *      - add props  for the ProjNameContext && UserManager
         *      
         *      - Create a constructor for the controller - assign values to context && userManager
         *      - code the AddToCart() action into the controller
         * 
         */

        #endregion

        private readonly StoreFrontContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        //ctor
        public ShoppingCartController(StoreFrontContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var sessionCart = HttpContext.Session.GetString("cart");
            Dictionary<int, CartItemViewModel> shoppingCart = null;
            if (sessionCart == null || sessionCart.Count() == 0)
            {
                shoppingCart = new Dictionary<int, CartItemViewModel>();
                ViewBag.Message = "There are no items in your cart.";
            }
            else
            {
                ViewBag.Message = null;
                shoppingCart = JsonConvert.DeserializeObject<Dictionary<int, CartItemViewModel>>(sessionCart);

               List<Product> products = _context.Products.Where(p => shoppingCart.Keys.Contains(p.Id)).ToList();

                //foreach (var product in products)
                //{
                //    shoppingCart[product.Id].VProductId = product;
                //}
            }
            List<VersionsProduct> versions = new List<VersionsProduct>();
            List<Product> prods = new List<Product>();
            foreach (var item in shoppingCart)
            {
                VersionsProduct v = _context.VersionsProducts.Where(vp => vp.Id == item.Value.VProductId).First();
                versions.Add(v);
                prods.Add(_context.Products.Where(p => p.Id == v.ProductId).First());
                //prods.Add(v.Product);
            }
            ViewBag.CartVersion = versions;
            ViewBag.CartProduct = prods;
            return View(shoppingCart);
        }

        public IActionResult Checkout()
        {
            var sessionCart = HttpContext.Session.GetString("cart");
            Dictionary<int, CartItemViewModel> shoppingCart = null;
            if (sessionCart == null || sessionCart.Count() == 0)
            {
                shoppingCart = new Dictionary<int, CartItemViewModel>();
                ViewBag.Message = "There are no items in your cart.";
            }
            else
            {
                ViewBag.Message = null;
                shoppingCart = JsonConvert.DeserializeObject<Dictionary<int, CartItemViewModel>>(sessionCart);

                List<Product> products = _context.Products.Where(p => shoppingCart.Keys.Contains(p.Id)).ToList();

                //foreach (var product in products)
                //{
                //    shoppingCart[product.Id].VProduct.Product = product;
                //}
            }
            List<VersionsProduct> versions = new List<VersionsProduct>();
            List<Product> prods = new List<Product>();
            foreach (var item in shoppingCart)
            {
                VersionsProduct v = _context.VersionsProducts.Where(vp => vp.Id == item.Value.VProductId).First();
                versions.Add(v);
                prods.Add(_context.Products.Where(p => p.Id == v.ProductId).First());
                //prods.Add(v.Product);
            }
            ViewBag.CartVersion = versions;
            ViewBag.CartProduct = prods;
            return View(shoppingCart);
        }

        //added add to cart action
        public IActionResult AddToCart(int? id)
        {
            //we are going to make a collection to store cart items
            //int (key) -> ProductID
            //CartItemViewModel (value) -> Product & Qty
            Dictionary<int, CartItemViewModel> shoppingCart = null;

            #region Session Notes
            /*
             * Session is a tool available on the server-side that can store information for a user while they are 
             * actively using your site.Typically the session lasts for 20 minutes (can be adjusted in program.cs). Once the 
             * 20 minutes is up, the session variable is disposed.
             * 
             * Values that we can store in Session are limited to: string, int 
             * - Because of this we have to get creative when trying to store complex object (like CartItemViewModel).
             * To Keep the info separated into properties we will convert the C# object to a JSON String
             */
            #endregion

            //"cart" is the name we choose for the session key
            var sessionCart = HttpContext.Session.GetString("cart");

            //check if session object exists. 
            //if not, instantiate the new local collection
            if (String.IsNullOrEmpty(sessionCart))
            {
                //if the session didn't exist yet, instantiate the collection as a new object
                shoppingCart = new Dictionary<int, CartItemViewModel>();
            }
            else
            {
                //cart already exists - transfer the existing cart items from session into our local shoppingCart
                //deserializeobject is the method that converts the json back to c#
                //we have to tell it what C# class to convert the json into
                //here, the type is Dictionary<int, CartItemViewModel> and we give it the sessionCart to convert
                shoppingCart = JsonConvert.DeserializeObject<Dictionary<int, CartItemViewModel>>(sessionCart);
            }

            // add newly selected products to the cart
            // retrieve product from the database
            //VersionsProduct product = _context.VersionsProducts.ToList()
            //    .Find(vp => vp.ProductId == id);

            //VersionsProduct product = Request.Form["Version"].ToString()

            /* This SelectedVersion form is populated by a list of versionsproducts stored in viewdata in the productscontroller
               and passed to the Details view. The form is named SelectedVersion */
            //string selectedVersion = Request.Form["SelectedVersion"].ToString();

            if(id == null)
            {
                return NotFound();
            }
            
            VersionsProduct product = _context.VersionsProducts.FirstOrDefault(vp => vp.Id == id);

            //VersionsProduct product = _context.VersionsProducts.Where(vp => vp.Id == id).First();

            // instantiate the object so we can add to the cart
            CartItemViewModel civm = new CartItemViewModel(1, product.Id);

            // check if the product is already in the cart.
            // if it is, increase quantity. if not, add it with quantity of 1
            if (shoppingCart.ContainsKey(product.Id))
            {
                //update qty
                shoppingCart[product.Id].Qty++;
            }
            else
            {
                shoppingCart.Add(product.Id, civm);
            }

            //update the session version of the cart
            //take the local copy, and serialize it as JSON
            //then assign that value to our session
            string jsonCart = JsonConvert.SerializeObject(shoppingCart);
            HttpContext.Session.SetString("cart", jsonCart);

       
            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int id)
        {
            //retrieve the cart from session
            var sessionCart = HttpContext.Session.GetString("cart");

            //convert JSON cart to C#
            Dictionary<int, CartItemViewModel> shoppingCart = JsonConvert.DeserializeObject<Dictionary<int, CartItemViewModel>>(sessionCart);

            //remove cart item
            shoppingCart.Remove(id);

            //if there are no remaining items in the cart, remove from session
            if (shoppingCart.Count == 0)
            {
                HttpContext.Session.Remove("cart");
            }
            //otherwise, update the session variable with our local cart contents
            else
            {
                string jsonCart = JsonConvert.SerializeObject(shoppingCart);
                HttpContext.Session.SetString("cart", jsonCart);
            }

            return RedirectToAction("Index");
        }

        public IActionResult UpdateCart(int productId, int qty)
        {
            //retrieve the cart from session
            var sessionCart = HttpContext.Session.GetString("cart");

            //convert JSON Cart to C#
            Dictionary<int, CartItemViewModel> shoppingCart = JsonConvert.DeserializeObject<Dictionary<int, CartItemViewModel>>(sessionCart);

            //update qty at index of passed in Id
            shoppingCart[productId].Qty = qty;

            //update session
            string jsonCart = JsonConvert.SerializeObject(shoppingCart);
            HttpContext.Session.SetString("cart", jsonCart);

            return RedirectToAction("Index");
        }

        //This method must be async in order to invoke the UserManager's async methods in this action.        
        public async Task<IActionResult> SubmitOrder()
        {
            string? userId = (await _userManager.GetUserAsync(HttpContext.User))?.Id;
            User ud = _context.Users.Find(userId);
            Order o = new Order()
            {
                Date = DateTime.Now,
                UserId = userId,
                ShipName = ud.FullName,
                //These are not in the user record so they will need to come from another screen
                //ShipCity = ud.City,
                //ShipState = ud.State,
                //ShipZip = ud.Zip
            };

            _context.Orders.Add(o);
            var sessionCart = HttpContext.Session.GetString("cart");
            Dictionary<int, CartItemViewModel> shoppingCart = JsonConvert.DeserializeObject<Dictionary<int, CartItemViewModel>>(sessionCart);

            //create orderproduct object for each record in the cart
            foreach (var item in shoppingCart)
            {
                OrderProduct op = new OrderProduct()
                {
                    OrderId = o.Id,
                    Id = item.Key,
                    ProductPrice = item.Value.VProductId,
                    UnitQuantity = (short)item.Value.Qty
                };

                //ONLY need to add items to an existing entity if the items are a related record from a linking table etc
                o.OrderProducts.Add(op);

            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Orders");

        }
    }
}
