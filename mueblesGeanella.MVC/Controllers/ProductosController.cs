using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering; // Simplifica la declaración de SelectList
using mueblesGeanella.CONSUMER;
using mueblesGeanella.Modelos;
using System.Collections.Generic;
using System.Linq;

namespace mueblesGeanella.MVC.Controllers
{
    public class ProductosController : Controller
    {
        // GET: PRODUCTOS
        public IActionResult Index()
        {
            var lista = CRUD<Producto>.GetAll() ?? new List<Producto>();
            var fabricante = CRUD<Fabricante>.GetAll() ?? new List<Fabricante>();

            foreach (var producto in lista)
            {
                producto.fabricante = fabricante.FirstOrDefault(f => f.IdFabricante == producto.IdFabricante);
            }
            return View(lista);
        }

        // GET: PRODUCTOS/Details/5
        public IActionResult Details(int id)
        {
            var producto = CRUD<Producto>.GetById(id);
            if (producto == null)
            {
                return NotFound();
            }

            // Opcional: Cargar el fabricante para mostrarlo en los detalles
            var fabricante = CRUD<Fabricante>.GetById(producto.IdFabricante);
            if (fabricante != null)
            {
                producto.fabricante = fabricante;
            }

            return View(producto);
        }

        // GET: PRODUCTOS/Create
        public IActionResult Create()
        {
            // Corregido: Se debe enviar la lista de fabricantes a la vista para el DropDownList
            var listaFabricantes = CRUD<Fabricante>.GetAll() ?? new List<Fabricante>();
            ViewBag.IdFabricante = new SelectList(listaFabricantes, "IdFabricante", "Nombre");
            return View();
        }

        // POST: PRODUCTOS/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Producto producto)
        {
            if (ModelState.IsValid)
            {
                CRUD<Producto>.Create(producto);
                return RedirectToAction(nameof(Index));
            }

            // Si falla, recargar la lista de fabricantes antes de volver a la vista
            var listaFabricantes = CRUD<Fabricante>.GetAll() ?? new List<Fabricante>();
            ViewBag.IdFabricante = new SelectList(listaFabricantes, "IdFabricante", "Nombre", producto.IdFabricante);
            return View(producto);
        }

        // GET: PRODUCTOS/Edit/5
        public IActionResult Edit(int id)
        {
            var producto = CRUD<Producto>.GetById(id);

            // Simplificado: Si GetById no funciona, busca en la lista completa
            if (producto == null)
            {
                var todasLasListas = CRUD<Producto>.GetAll() ?? new List<Producto>();
                producto = todasLasListas.FirstOrDefault(p => p.IdProducto == id);
            }

            if (producto == null)
            {
                return Content($"Error crítico: El producto con ID {id} no existe en la base de datos.");
            }

            var listaFabricantes = CRUD<Fabricante>.GetAll() ?? new List<Fabricante>();
            // Corregido: Uso de la directiva 'using Microsoft.AspNetCore.Mvc.Rendering;' para limpiar el código
            ViewBag.IdFabricante = new SelectList(listaFabricantes, "IdFabricante", "Nombre", producto.IdFabricante);

            return View(producto);
        }

        // POST: PRODUCTOS/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Producto producto)
        {
            if (id != producto.IdProducto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    CRUD<Producto>.Update(id, producto);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    // Si ocurre un error en el repositorio, continúa para repoblar la vista
                }
            }

            // Corregido: Si el modelo es inválido o falla el Update, rellenar el ViewBag para evitar errores en la vista
            var listaFabricantes = CRUD<Fabricante>.GetAll() ?? new List<Fabricante>();
            ViewBag.IdFabricante = new SelectList(listaFabricantes, "IdFabricante", "Nombre", producto.IdFabricante);
            return View(producto);
        }

        // GET: PRODUCTOS/Delete/5
        // Corregido: Eliminado el parámetro "Producto producto" que rompía la firma de la ruta clásica de MVC
        public IActionResult Delete(int id)
        {
            var producto = CRUD<Producto>.GetById(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        // POST: PRODUCTOS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                CRUD<Producto>.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.Clear();
                ModelState.AddModelError("", ex.Message);
                var productoCompleto = CRUD<Producto>.GetById(id);
                return View(productoCompleto);
            }
        }
    }
}
