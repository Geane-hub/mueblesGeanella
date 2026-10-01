using Microsoft.AspNetCore.Mvc;
using mueblesGeanella.CONSUMER;
using mueblesGeanella.Modelos;

namespace mueblesGeanella.MVC.Controllers
{
    public class DetalleFacturasController : Controller
    {
        // 💡 NOTA: Se eliminó el DbContext directo. Toda la información viaja por internet a la API.

        // GET: DETALLEFACTURAS
        public IActionResult Index()
        {
            // Obtiene la lista completa de detalles desde el Endpoint de la API
            var lista = CRUD<DetalleFactura>.GetAll();
            return View(lista);
        }

        // GET: DETALLEFACTURAS/Details/5
        public IActionResult Details(int id)
        {
            var detallefactura = CRUD<DetalleFactura>.GetById(id);
            if (detallefactura == null)
            {
                return NotFound();
            }

            return View(detallefactura);
        }

        // GET: DETALLEFACTURAS/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DETALLEFACTURAS/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("IdDetalle,Cantidad,Precio,IdFactura,IdProducto")] DetalleFactura detallefactura)
        {
            if (ModelState.IsValid)
            {
                // Registra el detalle mandándolo en formato JSON a la API
                CRUD<DetalleFactura>.Create(detallefactura);
                return RedirectToAction(nameof(Index));
            }
            return View(detallefactura);
        }

        // GET: DETALLEFACTURAS/Edit/5
        public IActionResult Edit(int id)
        {
            var detallefactura = CRUD<DetalleFactura>.GetById(id);
            if (detallefactura == null)
            {
                return NotFound();
            }
            return View(detallefactura);
        }

        // POST: DETALLEFACTURAS/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("IdDetalle,Cantidad,Precio,IdFactura,IdProducto")] DetalleFactura detallefactura)
        {
            if (id != detallefactura.IdDetalle)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Envía la actualización usando el método HTTP PUT mediante tu CONSUMER
                    CRUD<DetalleFactura>.Update(id, detallefactura);
                }
                catch
                {
                    return View(detallefactura);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(detallefactura);
        }

        // GET: DETALLEFACTURAS/Delete/5
        public IActionResult Delete(int id)
        {
            var detallefactura = CRUD<DetalleFactura>.GetById(id);
            if (detallefactura == null)
            {
                return NotFound();
            }

            return View(detallefactura);
        }

        // POST: DETALLEFACTURAS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Envía la instrucción de remoción mediante HTTP DELETE
            CRUD<DetalleFactura>.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
