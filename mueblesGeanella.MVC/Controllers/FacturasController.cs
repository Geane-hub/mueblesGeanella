using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using mueblesGeanella.CONSUMER;
using mueblesGeanella.Modelos;
using System.Collections.Generic;
using System.Linq;

namespace mueblesGeanella.MVC.Controllers
{
    // 💡 Creamos el ViewModel aquí mismo para que no tengas que crear archivos extra
    public class FacturaViewModel
    {
        public Factura Factura { get; set; }
        public Cliente Cliente { get; set; }
        public Producto Producto { get; set; }
    }

    public class FacturasController : Controller
    {
        // GET: FACTURAS
        public IActionResult Index()
        {
            var lista = CRUD<Factura>.GetAll() ?? new List<Factura>();
            var clientes = CRUD<Cliente>.GetAll() ?? new List<Cliente>();
            var productos = CRUD<Producto>.GetAll() ?? new List<Producto>();

            var listaViewModel = new List<FacturaViewModel>();

            foreach (var factura in lista)
            {
                var clienteAsignado = clientes.FirstOrDefault(c => c.IdCliente == factura.IdCliente);
                var productoAsignado = productos.FirstOrDefault(p => p.PrecioUnitario == factura.MontoTotal);

                listaViewModel.Add(new FacturaViewModel
                {
                    Factura = factura,
                    Cliente = clienteAsignado,
                    Producto = productoAsignado
                });
            }

            return View(listaViewModel); 
        }

        // GET: FACTURAS/Details/5
        public IActionResult Details(int id)
        {
            var factura = CRUD<Factura>.GetById(id);
            if (factura == null)
            {
                return NotFound();
            }

            var cliente = CRUD<Cliente>.GetById(factura.IdCliente);
            if (cliente != null)
            {
                factura.cliente = cliente;
            }

            return View(factura);
        }

        // GET: FACTURAS/Create
        public IActionResult Create()
        {
            var clientes = CRUD<Cliente>.GetAll() ?? new List<Cliente>();
            ViewBag.IdCliente = new SelectList(clientes, "IdCliente", "Nombre");
            return View();
        }

        // POST: FACTURAS/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Factura factura)
        {
            if (ModelState.IsValid)
            {
                CRUD<Factura>.Create(factura);
                return RedirectToAction(nameof(Index));
            }

            var clientes = CRUD<Cliente>.GetAll() ?? new List<Cliente>();
            ViewBag.IdCliente = new SelectList(clientes, "IdCliente", "Nombre", factura.IdCliente);
            return View(factura);
        }

        // GET: FACTURAS/Edit/5
        public IActionResult Edit(int id)
        {
            var factura = CRUD<Factura>.GetById(id);

            if (factura == null)
            {
                var todasLasListas = CRUD<Factura>.GetAll() ?? new List<Factura>();
                factura = todasLasListas.FirstOrDefault(f => f.IdFactura == id);
            }

            if (factura == null)
            {
                return Content($"Error crítico: La factura con ID {id} no existe en la base de datos.");
            }

            var clientes = CRUD<Cliente>.GetAll() ?? new List<Cliente>();
            ViewBag.IdCliente = new SelectList(clientes, "IdCliente", "Nombre", factura.IdCliente);
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
                    CRUD<Factura>.Update(id, factura);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    // Manejo del error
                }
            }

            var clientes = CRUD<Cliente>.GetAll() ?? new List<Cliente>();
            ViewBag.IdCliente = new SelectList(clientes, "IdCliente", "Nombre", factura.IdCliente);
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

            var cliente = CRUD<Cliente>.GetById(factura.IdCliente);
            if (cliente != null)
            {
                factura.cliente = cliente;
            }

            return View(factura);
        }

        // POST: FACTURAS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            CRUD<Factura>.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
