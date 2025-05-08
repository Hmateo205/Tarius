using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Login(Admin admin)
        {
            // Verifica si el modelo cumple con las validaciones
            if (ModelState.IsValid)
            {
                // Normaliza el nombre y la contrase�a para evitar problemas de comparaci�n
                string nombreIngresado = admin.Nombre?.Trim();
                string correoIngresado = admin.Correo?.Trim();
                string contrase�aIngresada = admin.Contrase�a?.Trim();


                // Depuraci�n: Muestra los datos ingresados
                ViewBag.DebugInfo = $"Nombre ingresado: '{nombreIngresado}', Contrase�a ingresada: '{contrase�aIngresada}', Correo Ingresado: '{correoIngresado}'";

                // Consulta para verificar el nombre y la contrase�a en la base de datos
                var usuario = _context.Administradores
                    .FirstOrDefault(a => a.Nombre == nombreIngresado && a.Correo == correoIngresado && a.Contrase�a == contrase�aIngresada);

                // Verifica si el usuario fue encontrado
                if (usuario != null)
                {
                    ViewBag.Message = "Login exitoso";
                    return RedirectToAction("Dashboard");
                }

                // Mensaje de error si el nombre o la contrase�a son incorrectos
                ViewBag.Message = "Nombre correo o contrase�a incorrectos.";
            }
            else
            {
                // Muestra los errores de validaci�n espec�ficos
                ViewBag.Message = "Error en el formulario. Por favor, verifique los campos.";
            }

            // Retorna a la vista de login si el proceso falla
            return View(admin);
        }

        // Muestra el dashboard si el login es exitoso
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
