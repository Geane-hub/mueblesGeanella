using Microsoft.AspNetCore.Mvc;
using mueblesGeanella.CONSUMER;
using mueblesGeanella.Modelos;

namespace mueblesGeanella.MVC.Controllers
{
    public class FabricantesController : Controller
    {
        // 💡 NOTA: Toda la comunicación con la base de datos se realiza a través de las peticiones HTTP del CONSUMER.

        // GET: FABRICANTES
        public IActionResult Index()
        {
            // Llama a la API mediante HTTP GET para obtener la lista de fabricantes
            var lista = CRUD<Fabricante>.GetAll();
            return View(lista);
        }

        // GET: FABRICANTES/Details/5
        public IActionResult Details(int id)
        {
            var fabricante = CRUD<Fabricante>.GetById(id);
            if (fabricante == null)
            {
                return NotFound();
            }

            return View(fabricante);
        }

        // GET: FABRICANTES/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FABRICANTES/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("IdFabricante,Nombre,Apellido,Servicio,Telefono")] Fabricante fabricante)
        {
            if (ModelState.IsValid)
            {
                // Envía el nuevo objeto Fabricante en formato JSON a la API
                CRUD<Fabricante>.Create(fabricante);
                return RedirectToAction(nameof(Index));
            }
            return View(fabricante);
        }

        // GET: FABRICANTES/Edit/5
        public IActionResult Edit(int id)
        {
            var fabricante = CRUD<Fabricante>.GetById(id);
            if (fabricante == null)
            {
                return NotFound();
            }
            return View(fabricante);
        }

        // POST: FABRICANTES/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("IdFabricante,Nombre,Apellido,Servicio,Telefono")] Fabricante fabricante)
        {
            if (id != fabricante.IdFabricante)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Envía la actualización mediante HTTP PUT
                    CRUD<Fabricante>.Update(id, fabricante);
                }
                catch
                {
                    return View(fabricante);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(fabricante);
        }

        // GET: FABRICANTES/Delete/5
        public IActionResult Delete(int id)
        {
            var fabricante = CRUD<Fabricante>.GetById(id);
            if (fabricante == null)
            {
                return NotFound();
            }

            return View(fabricante);
        }

        // POST: FABRICANTES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Envía la instrucción de eliminación remota mediante HTTP DELETE
            CRUD<Fabricante>.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
