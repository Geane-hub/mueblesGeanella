using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using mueblesGeanella.CONSUMER;
using mueblesGeanella.Modelos;

namespace mueblesGeanella.MVC.Controllers
{
    public class DetalleFacturasController : Controller
    {
        // GET: DetalleFacturas
        public IActionResult Index()
        {
            var detalles = CRUD<DetalleFactura>.GetAll() ?? new List<DetalleFactura>();
            var productos = CRUD<Producto>.GetAll() ?? new List<Producto>();
            
            foreach(var detalle in detalles)
            {
                detalle.Producto = productos.FirstOrDefault(p => p.IdProducto == detalle.IdProducto);
            }
            
            return View(detalles);
        }

        // GET: DetalleFacturas/Details/5 
        public IActionResult Details(int id)
        {
            var factura = CRUD<Factura>.GetById(id);
            if (factura == null) return NotFound();

            // Cargar cliente asociado
            factura.cliente = CRUD<Cliente>.GetById(factura.IdCliente);

            // Cargar todos los detalles y filtrar los que pertenecen a esta factura
            var todosLosDetalles = CRUD<DetalleFactura>.GetAll() ?? new List<DetalleFactura>();
            factura.DetalleFacturas = todosLosDetalles.Where(d => d.IdFactura == id).ToList();

            // Cargar la información de cada producto en los detalles
            var todosLosProductos = CRUD<Producto>.GetAll() ?? new List<Producto>();
            foreach (var detalle in factura.DetalleFacturas)
            {
                detalle.Producto = todosLosProductos.FirstOrDefault(p => p.IdProducto == detalle.IdProducto);
            }

            return View(factura);
        }

        // GET: DetallesFactura/Edit/5 
        public IActionResult Edit(int id)
        {
            var detalle = CRUD<DetalleFactura>.GetById(id);
            if (detalle == null) return NotFound();

            var productos = CRUD<Producto>.GetAll();
            ViewBag.IdProducto = new SelectList(productos, "IdProducto", "Nombre", detalle.IdProducto);

            return View(detalle);
        }

        // POST: DetallesFactura/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, DetalleFactura detalleEditado)
        {
            if (id != detalleEditado.IdDetalle) return NotFound();
            ModelState.Clear();

            if (ModelState.IsValid)
            {
                try
                {
                    var producto = CRUD<Producto>.GetById(detalleEditado.IdProducto);
                    detalleEditado.PrecioUnitario = producto.PrecioUnitario; // Asumiendo que Producto tiene PrecioUnitario

                    CRUD<DetalleFactura>.Update(id, detalleEditado);

                    RecalcularTotalFactura(detalleEditado.IdFactura);

                    return RedirectToAction(nameof(Details), new { id = detalleEditado.IdFactura });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error al actualizar el renglón: {ex.Message}");
                }
            }

            var productos = CRUD<Producto>.GetAll();
            ViewBag.IdProducto = new SelectList(productos, "IdProducto", "Nombre", detalleEditado.IdProducto);
            return View(detalleEditado);
        }

        // GET: DetallesFactura/Delete/5
        public IActionResult Delete(int id)
        {
            var detalle = CRUD<DetalleFactura>.GetById(id);
            if (detalle == null) return NotFound();

            detalle.Producto = CRUD<Producto>.GetById(detalle.IdProducto);
            return View(detalle);
        }

        // POST: DetallesFactura/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var detalle = CRUD<DetalleFactura>.GetById(id);
            if (detalle == null) return NotFound();

            try
            {
                int idFactura = detalle.IdFactura;

                CRUD<DetalleFactura>.Delete(id);

                RecalcularTotalFactura(idFactura);

                return RedirectToAction(nameof(Details), new { id = idFactura });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error al eliminar el renglón: {ex.Message}");
                detalle.Producto = CRUD<Producto>.GetById(detalle.IdProducto);
                return View(detalle);
            }
        }
        private void RecalcularTotalFactura(int idFactura)
        {
            var todosLosDetalles = CRUD<DetalleFactura>.GetAll() ?? new List<DetalleFactura>();
            var detallesFactura = todosLosDetalles.Where(d => d.IdFactura == idFactura).ToList();

            decimal nuevoTotal = detallesFactura.Sum(d => d.Cantidad * d.PrecioUnitario);

            var factura = CRUD<Factura>.GetById(idFactura);
            factura.MontoTotal = nuevoTotal;
            CRUD<Factura>.Update(idFactura, factura);
        }
    }
}
