using Microsoft.AspNetCore.Mvc;
using mueblesGeanella.CONSUMER;
using mueblesGeanella.Modelos;

namespace mueblesGeanella.MVC.Controllers
{
    public class ProductosController : Controller
    {

        // GET: PRODUCTOS
        public IActionResult Index()
        {

            var lista = CRUD<Producto>.GetAll();
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

            return View(producto);
        }

        // GET: PRODUCTOS/Create
        public IActionResult Create()
        {
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
            return View(producto);
        }

        // GET: PRODUCTOS/Edit/5
        public IActionResult Edit(int id)
        {
            var producto = CRUD<Producto>.GetById(id);
            if (producto == null)
            {
                return NotFound();
            }
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
                    // Envía los cambios del producto usando HTTP PUT
                    CRUD<Producto>.Update(id, producto);
                }
                catch
                {
                    return View(producto);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        // GET: PRODUCTOS/Delete/5
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
        public ActionResult Delete(int id, Producto producto)
        {
            try
            {
                CRUD<Producto>.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }
    }
}
