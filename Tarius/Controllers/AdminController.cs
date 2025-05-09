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
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Muestra el formulario de login
        public IActionResult Login()
        {
            return View();
        }

        // Procesa el formulario de login
        [HttpPost]
        public async Task<IActionResult> Login(Admin admin)
        {
            if (ModelState.IsValid)
            {
                string nombreIngresado = admin.Nombre?.Trim();
                string contraseñaIngresada = admin.Contraseña?.Trim();
                string correoIngresado = admin.Correo?.Trim();

                var usuario = _context.Administradores
                    .FirstOrDefault(a => a.Nombre == nombreIngresado && a.Contraseña == contraseñaIngresada && a.Correo == correoIngresado);

                if (usuario != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, usuario.Nombre),
                        new Claim("Correo", usuario.Correo)
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    // Inicia sesión con el esquema correcto
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Dashboard");
                }

                ViewBag.Message = "Nombre, correo o contraseña incorrectos.";
            }
            else
            {
                ViewBag.Message = "Error en el formulario. Por favor, verifique los campos.";
            }

            return View(admin);
        }

        // Muestra el dashboard solo si el usuario está autenticado
        [Authorize]
        public IActionResult Dashboard()
        {
            return View();
        }

        // Cerrar sesión
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

    }
}
