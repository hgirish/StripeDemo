using System.Configuration;
using System.Threading.Tasks;
using System.Web.Mvc;
using Stripe;
using StripeDemo.Models;

namespace StripeDemo.Controllers
{
    public class ChargeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Learn how to process payments with Stripe";

            var model = new StripeChargeModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(StripeChargeModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var chargeId = await ProcessPayment(model);

            ViewBag.ChargeId = chargeId;
            return View("Index");
        } 

        private async Task<string> ProcessPayment(StripeChargeModel model)
        {
            return await Task.Run(() =>
            {
                var myCharge = new StripeChargeCreateOptions
                {
                    Amount = (int) (model.Amount*100),
                    Currency = "usd",
                    Description = "Description for test charge",
                    Source = new StripeSourceOptions() { TokenId = model.Token }
                };
                string stripePrivateKey = ConfigurationManager.AppSettings["StripePrivateKey"];
                var chargeService = new StripeChargeService(stripePrivateKey);

                var stripeCharge = chargeService.Create(myCharge);

                return stripeCharge.Id;
            });
        }
    }
}