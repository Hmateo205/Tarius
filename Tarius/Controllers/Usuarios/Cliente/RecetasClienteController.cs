using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tarius.Data;
using Tarius.Models.Cliente;
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


        [HttpGet]
        public async Task<IActionResult> SeleccionarPorciones(int recetaId)
        {
            var receta = await _context.Recetas
                .Include(r => r.Ingredientes)
                .FirstOrDefaultAsync(r => r.Id == recetaId);

            if (receta == null)
                return NotFound();

            ViewBag.RecetaId = recetaId;
            ViewBag.NombreReceta = receta.Nombre;

            return View("~/Views/Usuarios/Cliente/Recetas/SeleccionarPorciones.cshtml");
        }


        //ver lista de compras
        public async Task<IActionResult> ListaCompras()
        {
            var correoUsuario = User.Identity?.Name;
            if (string.IsNullOrEmpty(correoUsuario))
                return RedirectToAction("Login", "Login");

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == correoUsuario);
            if (usuario == null)
                return RedirectToAction("Login", "Login");

            var lista = await _context.ListaComprasCliente
                .Where(i => i.UsuarioId == usuario.Id)
                .ToListAsync();

            return View("~/Views/Usuarios/Cliente/ListaCompras/ListaCompras.cshtml", lista);
        }






        //logica de comparacion de los inghredientes de la receta con los ingredientes del stock, multiplicando por porciones.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcesarPorciones(int recetaId, int porciones)
        {
            var correoUsuario = User.Identity?.Name;
            if (string.IsNullOrEmpty(correoUsuario))
                return RedirectToAction("Login", "Login");

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == correoUsuario);
            if (usuario == null)
                return RedirectToAction("Login", "Login");

            var receta = await _context.Recetas
                .Include(r => r.Ingredientes)
                .FirstOrDefaultAsync(r => r.Id == recetaId);

            if (receta == null || receta.Ingredientes == null || !receta.Ingredientes.Any())
                return RedirectToAction("VerRecetas");

            var inventario = await _context.IngredienteCliente
                .Where(i => i.UsuarioId == usuario.Id)
                .ToListAsync();

            foreach (var ingredienteReceta in receta.Ingredientes)
            {
                // Convertir la cantidad de la receta a double
                if (!double.TryParse(ingredienteReceta.Cantidad, out double cantidadBase))
                    continue; // Saltar si la cantidad no es numérica válida

                double cantidadNecesaria = cantidadBase * porciones;

                var enInventario = inventario.FirstOrDefault(i =>
                    i.Nombre?.Trim().ToLower() == ingredienteReceta.Nombre?.Trim().ToLower() &&
                    i.Unidad?.Trim().ToLower() == ingredienteReceta.Unidad?.Trim().ToLower());

                double cantidadDisponible = 0;

                if (enInventario != null && double.TryParse(enInventario.Cantidad, out double cantidadParseada))
                    cantidadDisponible = cantidadParseada;

                if (cantidadDisponible < cantidadNecesaria)
                {
                    double cantidadFaltante = cantidadNecesaria - cantidadDisponible;

                    _context.ListaComprasCliente.Add(new ListaComprasCliente
                    {
                        UsuarioId = usuario.Id,
                        NombreIngrediente = ingredienteReceta.Nombre!,
                        Unidad = ingredienteReceta.Unidad!,
                        Cantidad = Math.Round(cantidadFaltante, 2)
                    });
                }
            }

            await _context.SaveChangesAsync();

            TempData["Mensaje"] = "Ingredientes faltantes añadidos a la lista de compras.";

            return RedirectToAction("VerRecetas");
        }

        //Eliminar de la lista
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarLista()
        {
            var correo = User.Identity?.Name;
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == correo);
            if (usuario == null) return RedirectToAction("Login", "Login");

            var lista = _context.ListaComprasCliente.Where(l => l.UsuarioId == usuario.Id);
            _context.ListaComprasCliente.RemoveRange(lista);

            await _context.SaveChangesAsync();

            TempData["Mensaje"] = "Lista de compras vaciada correctamente.";
            return RedirectToAction("ListaCompras");
        }

        //Boton de ingresar al inventario

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AgregarAlInventario()
        {
            var correo = User.Identity?.Name;
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == correo);
            if (usuario == null) return RedirectToAction("Login", "Login");

            var lista = await _context.ListaComprasCliente
                .Where(l => l.UsuarioId == usuario.Id)
                .ToListAsync();

            var inventario = await _context.IngredienteCliente
                .Where(i => i.UsuarioId == usuario.Id)
                .ToListAsync();

            foreach (var item in lista)
            {
                var existente = inventario.FirstOrDefault(i =>
                    i.Nombre?.Trim().ToLower() == item.NombreIngrediente.Trim().ToLower() &&
                    i.Unidad?.Trim().ToLower() == item.Unidad.Trim().ToLower());

                if (existente != null)
                {
                    if (double.TryParse(existente.Cantidad, out double cantidadActual))
                        existente.Cantidad = (cantidadActual + item.Cantidad).ToString();
                }
                else
                {
                    _context.IngredienteCliente.Add(new IngredienteCliente
                    {
                        UsuarioId = usuario.Id,
                        Nombre = item.NombreIngrediente,
                        Unidad = item.Unidad,
                        Cantidad = item.Cantidad.ToString()
                    });
                }
            }

            // Limpiar lista luego de agregar
            _context.ListaComprasCliente.RemoveRange(lista);

            await _context.SaveChangesAsync();

            TempData["Mensaje"] = "Ingredientes añadidos al inventario correctamente.";
            return RedirectToAction("ListaCompras");
        }


        //boton de ordenamiento alfabetico
        [HttpGet]
        public async Task<IActionResult> VerRecetas(string orden)
        {
            var recetas = _context.Recetas.AsQueryable();

            // Si el cliente seleccionó orden alfabético
            if (orden == "az")
            {
                recetas = recetas.OrderBy(r => r.Nombre);
            }

            var lista = await recetas.ToListAsync();

            return View("~/Views/Usuarios/Cliente/Recetas/VerRecetas.cshtml", lista);
        }


    }
}
