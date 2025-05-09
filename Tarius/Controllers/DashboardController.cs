using Microsoft.AspNetCore.Mvc;
using Tarius.Data;
using Tarius.Models;
using System.Linq;

namespace Tarius.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Vista principal del Dashboard
        public IActionResult Index()
        {
            var admins = _context.Administradores.ToList();
            return View(admins);
        }

        // Crear Administrador (GET)
        public IActionResult Create()
        {
            return View();
        }

        // Crear Administrador (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Admin admin)
        {
            if (ModelState.IsValid)
            {
                _context.Administradores.Add(admin);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin);
        }

        // Editar Administrador (GET)
        public IActionResult Edit(int id)
        {
            var admin = _context.Administradores.Find(id);
            if (admin == null)
                return NotFound();
            return View(admin);
        }

        // Editar Administrador (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Admin admin)
        {
            if (ModelState.IsValid)
            {
                _context.Administradores.Update(admin);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin);
        }

        // Eliminar Administrador
        public IActionResult Delete(int id)
        {
            var admin = _context.Administradores.Find(id);
            if (admin != null)
            {
                _context.Administradores.Remove(admin);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
