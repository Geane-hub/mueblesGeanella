using Microsoft.AspNetCore.Mvc;
using mueblesGeanella.CONSUMER;
using mueblesGeanella.Modelos;

namespace mueblesGeanella.MVC.Controllers
{
    public class FabricantesController : Controller
    {
        // GET: Fabricantes
        public IActionResult Index()
        {
            var lista = CRUD<Fabricante>.GetAll() ?? new List<Fabricante>();

            return View(lista);
        }

        // GET: Fabricantes/Details/5
        public IActionResult Details(int id)
        {
            var fabricante = CRUD<Fabricante>.GetById(id);

            if (fabricante == null)
            {
                return NotFound();
            }

            return View(fabricante);
        }

        // GET: Fabricantes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fabricantes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Fabricante fabricante)
        {
            if (!ModelState.IsValid)
            {
                return View(fabricante);
            }

            try
            {
                // El ID debe ser generado por la API
                CRUD<Fabricante>.Create(fabricante);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(
                    "",
                    $"Error al crear el fabricante: {ex.Message}"
                );

                return View(fabricante);
            }
        }

        // GET: Fabricantes/Edit/5
        public IActionResult Edit(int id)
        {
            var fabricante = CRUD<Fabricante>.GetById(id);

            if (fabricante == null)
            {
                return NotFound();
            }

            return View(fabricante);
        }

        // POST: Fabricantes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Fabricante fabricante)
        {
            if (id != fabricante.IdFabricante)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(fabricante);
            }

            try
            {
                CRUD<Fabricante>.Update(id, fabricante);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(
                    "",
                    $"Error al actualizar el fabricante: {ex.Message}"
                );

                return View(fabricante);
            }
        }

        // GET: Fabricantes/Delete/5
        public IActionResult Delete(int id)
        {
            var fabricante = CRUD<Fabricante>.GetById(id);

            if (fabricante == null)
            {
                return NotFound();
            }

            return View(fabricante);
        }

        // POST: Fabricantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                CRUD<Fabricante>.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(
                    "",
                    $"Error al eliminar el fabricante: {ex.Message}"
                );

                var fabricante = CRUD<Fabricante>.GetById(id);

                if (fabricante == null)
                {
                    return NotFound();
                }

                return View("Delete", fabricante);
            }
        }
    }
}