using Microsoft.AspNetCore.Mvc;

namespace API5.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
