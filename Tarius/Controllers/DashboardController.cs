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
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Menu()
        {
            return View("~/Views/Admin/Dashboard/Menu.cshtml");
        }

        // Crear Administrador (GET)
        public IActionResult Create()
        {
            return View("~/Views/Admin/Dashboard/Create.cshtml");
        }

        // Crear Administrador (POST)
        [HttpPost]
        public IActionResult Create(Admin admin)
        {
            if (ModelState.IsValid)
            {
                _context.Administradores.Add(admin);
                _context.SaveChanges();
                return RedirectToAction("Menu");
            }
            return View(admin);
        }

        // Editar Administrador (GET)
        public IActionResult Edit(int id)
        {
            var admin = _context.Administradores.Find(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }

        // Editar Administrador (POST)
        [HttpPost]
        public IActionResult Edit(Admin admin)
        {
            if (ModelState.IsValid)
            {
                _context.Administradores.Update(admin);
                _context.SaveChanges();
                return RedirectToAction("Menu");
            }
            return View(admin);
        }

        // Eliminar Administrador (GET)
        public IActionResult Delete(int id)
        {
            var admin = _context.Administradores.Find(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }

        // Confirmar Eliminación (POST)
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var admin = _context.Administradores.Find(id);
            if (admin != null)
            {
                _context.Administradores.Remove(admin);
                _context.SaveChanges();
            }
            return RedirectToAction("Menu");
        }
    }
}