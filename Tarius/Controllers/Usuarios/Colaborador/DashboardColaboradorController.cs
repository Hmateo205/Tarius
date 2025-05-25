using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Tarius.Controllers.Usuarios.Colaborador
{
    [Authorize(Roles = "Colaborador")]
    public class DashboardColaboradorController : Controller
    {
        public IActionResult Menu()
        {
            return View("~/Views/Usuarios/Colaborador/Menu.cshtml");
        }
    }
}
