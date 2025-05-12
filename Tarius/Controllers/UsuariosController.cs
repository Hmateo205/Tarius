using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Tarius.Models;
using Tarius.Data;
using System.Linq;

namespace Tarius.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }
        // Muestra el dashboard solo si el usuario está autenticado
        [Authorize]
        public IActionResult Dashboard()
        {
            return View("~/Views/Usuarios/Dashboard/Menu.cshtml");
        }

    }
}
