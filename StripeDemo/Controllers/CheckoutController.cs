using System;
using System.Configuration;
using System.Security;
using System.Threading.Tasks;
using System.Web.Mvc;
using Antlr.Runtime.Misc;
using Stripe;
using StripeDemo.StripeInfrastructure;

namespace StripeDemo.Controllers
{
    public class CheckoutController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult OldIndex(string stripeToken, string stripeEmail)
        {
            string apiKey = ConfigurationManager.AppSettings["StripePrivateKey"];

            var stripeClient = new Stripe.StripeClient(apiKey);
           
            dynamic response = stripeClient.CreateChargeWithToken(
                24.99m, stripeToken, "usd", "Test Transaction", null);
           
            if (response.IsError == false && response.Paid)
            {ViewBag.Message = "Success";
                ViewBag.Response = response;
                //dynamic responseSource = response.Source;

            }
            else
            {
                ViewBag.Message = response.error.message;
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(StripeChargeModel model)
        {
            Random random = new Random();
            double number = random.NextDouble();
            if (number < 0.01)
            {
                number = 0.01;
            }
            var amount = Convert.ToDecimal(number*100);
            model.Amount = amount;
            model.Currency = "usd";
            model.Description = string.Format("Charge for : {0}", amount);
           
            
            var charge = await new StripeHelper().ChargeCustomer(model);
            if (charge.Paid && !charge.IsError)
            {
                ViewBag.Message = "Success";
            }
            else
            {
                ViewBag.Message = charge.FailureMessage;
            }
            ViewBag.Response = charge;
            return View();
        }
    }
}