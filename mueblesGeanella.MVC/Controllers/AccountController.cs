using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace mueblesGeanella.MVC.Controllers
{
    public class AccountController : Controller
    {
        // 1. Muestra la pantalla del formulario (GET)
        [HttpGet]
        public IActionResult Login()
        {
            // Si ya hay una sesión activa, lo mandamos al inicio
            if (HttpContext.Session.GetString("UsuarioLogueado") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // 2. Procesa los datos del formulario (POST)
        [HttpPost]
        public IActionResult Login(string correo, string password)
        {
            if (string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "Por favor, ingresa tu correo y contraseña.";
                return View();
            }

            // Validación estática para presentación del proyecto
            if (correo == "admin@mueblesgeanella.com" && password == "admin123")
            {
                HttpContext.Session.SetString("UsuarioLogueado", "Jenny Rosero (Dueña)");
                HttpContext.Session.SetString("Rol", "Administradora");
                return RedirectToAction("Index", "Home");
            }
            else if (correo == "ventas@mueblesgeanella.com" && password == "ventas123")
            {
                HttpContext.Session.SetString("UsuarioLogueado", "Vendedor");
                HttpContext.Session.SetString("Rol", "Vendedor");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Correo o contraseña incorrectos.";
                return View();
            }
        }

        // 3. Método para Cerrar Sesión
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}
