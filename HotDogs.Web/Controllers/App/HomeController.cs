using System.Web.Mvc;

namespace HotDogs.Web.Controllers.App
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}