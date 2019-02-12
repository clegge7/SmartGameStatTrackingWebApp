using System.Web;
using System.Web.Mvc;

namespace SmartGameStatTrackingWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if(User.Identity.Name == "")
            {
                return Redirect("/Login.aspx");
            }
            return View();
        }

        public ActionResult AnotherLink()
        {
            if (User.Identity.Name == "")
            {
                return Redirect("/Login.aspx");
            }
            return View("Index");
        }

        [HttpPost]
        public ActionResult SignOut()
        {
            return Redirect("/Login.aspx");
        }

    }
}
