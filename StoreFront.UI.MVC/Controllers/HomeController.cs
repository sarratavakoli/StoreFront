using Microsoft.AspNetCore.Mvc;
using StoreFront.UI.MVC.Models;
using System.Diagnostics;
using MimeKit;
using MailKit.Net.Smtp;

namespace StoreFront.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //field for the config settings in appsettings.json
        private readonly IConfiguration _config;

        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        public IActionResult Contact()
        {
            //we want the info in our contact form to use the ContactViewModel we 
            //created. to do so, follow the next steps:

            #region Code Generation Steps

            //1. Go to Tools > NuGet Package Manager > Manage NuGet Packages for Solution
            //2. Go to the Browse tab and search for Microsoft.VisualStudio.Web
            //3. Click Microsoft.VisualStudio.Web.CodeGeneration.Design
            //4. On the right, check the box next to the CORE1 project, then click "Install"
            //5. Once installed, return here and right click the Contact action
            //6. Select Add View, then select the Razor View template and click "Add"
            //7. Enter the following settings:
            //      - View Name: Contact
            //      - Template: Create
            //      - Model Class: ContactViewModel
            //8. Leave all other settings as-is and click "Add"

            #endregion

            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel cvm)
        {
            //validation
            if (!ModelState.IsValid)
            {
                //return input if returning user
                return View(cvm);
            }

            //contact email message format
            string message = $"You have received a new email from your site's contact form!<br/><br/>" +
                $"Sender: {cvm.Name}<br/>" +
                $"Email: {cvm.Email}<br/>" +
                $"Subject: {cvm.Subject}<br/>" +
                $"Message: {cvm.Message}";

            //MimeMessage object assists with storing/transporting the email info from the contact form
            var mm = new MimeMessage();

            //access email user to send email
            mm.From.Add(new MailboxAddress("User", _config.GetValue<string>("Credentials:Email:User")));
           
            //add personal email as recipient 
            mm.To.Add(new MailboxAddress("Personal", _config.GetValue<string>("Credentials:Email:Recipient")));

            //the subject will be the one provided by the user, which is stored in the cvm object
            mm.Subject = cvm.Subject;

            //the body of the message will be formatted with the string we created above.
            mm.Body = new TextPart("HTML") { Text = message };

            //set urgent priority flag
            mm.Priority = MessagePriority.Urgent;

            //we can also add the user's provided email address to the list of ReplyTo addresses 
            //so our replies can be sent directly to them (instead of sending to our own email user)
            mm.ReplyTo.Add(new MailboxAddress("Sender", cvm.Email));

            //the using directive will create the SmtpClient object, which is used to send the email
            //once all of the code inside the using directive's scope has been executed, it will 
            //automatically close any open connections and dispose of the object for us.

            using (var client = new SmtpClient())
            {
                //connect to the mail server using the credentials in our appsettings.json
                client.Connect(_config.GetValue<string>("Credentials:Email:Client"));

                //Log in to mail server using the credentials for our email user
                client.Authenticate(

                    //username
                    _config.GetValue<string>("Credentials:Email:User"),

                    //password
                    _config.GetValue<string>("Credentials:Email:Password")

                    );

                //it's possible the mail server may be down when the user attempts to contact us. 
                //so, we can "encapsulate" our code to send the message in a try/catch block
                try
                {
                    //try to send the email
                    client.Send(mm);
                }
                catch (Exception ex)
                {
                    //if there is an issue, we can store an error message in the ViewBag
                    //to be displayed in the View
                    ViewBag.ErrorMessage = $"There was an error processing your request. Please try " +
                        $"again later. <br/><br/>Error Message: ${ex.StackTrace}";
                    return View(cvm);
                }

            }

            return View("EmailConfirmation", cvm);
        }

    }
}