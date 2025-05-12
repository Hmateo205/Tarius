using Microsoft.AspNetCore.Mvc;

namespace Tarius.Controllers.Usuarios
{
    public class SingInController : Controller
    {
        public IActionResult SingIn()
        {
            return View("~/Views/Usuarios/SingIn.cshtml");
        }
    }
}
