using System.Web.Mvc;

namespace SmartGameStatTrackingWebApp.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult InternalServerError()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }
    }
}
