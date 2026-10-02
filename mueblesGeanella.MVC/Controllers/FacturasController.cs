using Microsoft.AspNetCore.Mvc;
using mueblesGeanella.CONSUMER;
using mueblesGeanella.Modelos;

namespace mueblesGeanella.MVC.Controllers
{
    public class FacturasController : Controller
    {
        // 💡 NOTA: La base de datos es administrada remotamente por la API a través del CONSUMER.

        // GET: FACTURAS
        public IActionResult Index()
        {
            // Solicita a la API todas las facturas registradas mediante HTTP GET
            var lista = CRUD<Factura>.GetAll();
            var clientes = CRUD<Cliente>.GetAll();
            foreach(var factura in lista)
            {
                factura.cliente = clientes
                    .FirstOrDefault(c => c.IdCliente == factura.IdCliente);
            }
            return View(lista);
        }

        // GET: FACTURAS/Details/5
        public IActionResult Details(int id)
        {
            var factura = CRUD<Factura>.GetById(id);
            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }

        // GET: FACTURAS/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FACTURAS/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Factura factura)
        {
            if (ModelState.IsValid)
            {
                // Serializa y envía el objeto Factura en un paquete JSON por POST hacia la API
                CRUD<Factura>.Create(factura);
                return RedirectToAction(nameof(Index));
            }
            return View(factura);
        }

        // GET: FACTURAS/Edit/5
        public IActionResult Edit(int id)
        {
            var factura = CRUD<Factura>.GetById(id);
            if (factura == null)
            {
                return NotFound();
            }
            return View(factura);
        }

        // POST: FACTURAS/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("IdFactura,Fecha,MontoTotal,IdCliente")] Factura factura)
        {
            if (id != factura.IdFactura)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Envía la información modificada usando el método HTTP PUT
                    CRUD<Factura>.Update(id, factura);
                }
                catch
                {
                    return View(factura);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(factura);
        }

        // GET: FACTURAS/Delete/5
        public IActionResult Delete(int id)
        {
            var factura = CRUD<Factura>.GetById(id);
            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }

        // POST: FACTURAS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Envía la instrucción de eliminación física mediante HTTP DELETE a la API
            CRUD<Factura>.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
