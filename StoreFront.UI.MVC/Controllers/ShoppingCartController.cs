﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using StoreFront.DATA.EF.Models;
using StoreFront.UI.MVC.Models;
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
            ViewBag.Cart = HttpContext.Session.GetString("cart");
            return View();
        }

        //added add to cart action
        public IActionResult AddToCart(int id)
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
            Product product = _context.Products.Find(id);

            // instantiate the object so we can add to the cart
            CartItemViewModel civm = new CartItemViewModel(1, product);

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
    }
}