using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Tarius.Controllers.Usuarios.Administrador
{
    [Authorize(Roles = "Administrador")]
    public class DashboardAdminController : Controller
    {
        public IActionResult Menu()
        {
            return View("~/Views/Usuarios/Administrador/Menu.cshtml");
        }
    }
}
