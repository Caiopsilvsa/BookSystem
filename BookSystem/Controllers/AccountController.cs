using Microsoft.AspNetCore.Mvc;

namespace BookSystem.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
