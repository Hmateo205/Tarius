using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tarius.Data;
using Tarius.Models.Cliente;
using Tarius.Models;

namespace Tarius.Controllers.Usuarios.Cliente
{
    public class MisIngredientesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MisIngredientesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Ingredientes()
        {
            // 1. Obtener el correo desde los claims
            var correoUsuario = User.Identity?.Name;

            if (string.IsNullOrEmpty(correoUsuario))
            {
                return RedirectToAction("Login", "Login");
            }

            // 2. Obtener el ID del usuario desde la base de datos
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == correoUsuario);

            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }

            ViewBag.Correo = correoUsuario;


            // 3. Filtrar los ingredientes por el ID del usuario
            var ingredientes = await _context.IngredienteCliente
                .Include(i => i.Usuario)
                .Where(i => i.UsuarioId == usuario.Id)
                .ToListAsync();
             
            return View("~/Views/Usuarios/Cliente/MisIngredientes/Ingredientes.cshtml", ingredientes);
        }




    }
}