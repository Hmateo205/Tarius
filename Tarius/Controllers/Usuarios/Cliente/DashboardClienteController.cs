using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Tarius.Controllers.Usuarios.Cliente
{
    [Authorize(Roles = "Cliente")]
    public class DashboardClienteController : Controller
    {
        public IActionResult Menu()
        {
            return View("~/Views/Usuarios/Cliente/Menu.cshtml"); 
        }
    }
}
