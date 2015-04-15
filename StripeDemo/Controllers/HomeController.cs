using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using StripeDemo.StripeInfrastructure;

namespace StripeDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public async  Task<ActionResult> About()
        {
            var model = new CreateTokenModel
            {
                CardNumber = "4242424242424242",
                ExpieryMonth = "10",
                ExpiryYear = DateTime.Now.AddYears(2).Year.ToString(),
                Cvc = "123",
                Email = "test@sometest.com",
                Zip = "12345"

            };
            var stripeHelper = new StripeHelper();
            var result =  await  stripeHelper.CreateToken(model);
            ViewBag.Message = string.Format("Stripe Card Token: {0}",result.FailureMessage);

           // ViewBag.Message = "Your application description page.";
            StripeChargeModel chargeCustomerModel = new StripeChargeModel
            {
                Amount = 50,
                Currency = "USD",
                Description = "Testing token creation",
                Email = "fundog@test.com",
                TokenId = result.TokenId
            };
            
            var chargeResult = await stripeHelper.ChargeCustomer(chargeCustomerModel);
            if (chargeResult.Paid)
            {
                ViewBag.Message += string.Format("\nSuccess!, id:{0}", chargeResult.ChargeId);
            }
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}