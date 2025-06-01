using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tarius.Data;
using Tarius.Models.Colaborador; // necesario para acceder al modelo Receta

namespace Tarius.Controllers.Usuarios.Cliente
{
    public class RecetasClienteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecetasClienteController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> VerRecetas()
        {
            var recetas = await _context.Recetas.ToListAsync();
            return View("~/Views/Usuarios/Cliente/Recetas/VerRecetas.cshtml", recetas);
        }
    }
}
