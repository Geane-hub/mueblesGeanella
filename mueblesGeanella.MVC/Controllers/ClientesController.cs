using Microsoft.AspNetCore.Mvc;
using mueblesGeanella.CONSUMER;
using mueblesGeanella.Modelos;

namespace mueblesGeanella.MVC.Controllers
{
    public class ClientesController : Controller
    {

        // GET: CLIENTES
        public IActionResult Index()
        {
            // Llama a la API mediante HTTP GET y le pasa la lista de objetos a la vista
            var lista = CRUD<Cliente>.GetAll();
            return View(lista);
        }

        // GET: CLIENTES/Details/5
        public IActionResult Details(int id)
        {
            var cliente = CRUD<Cliente>.GetById(id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: CLIENTES/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CLIENTES/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("IdCliente,Cedula,Nombre,Apellido,Telefono,Direccion,email")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                // Envía el nuevo cliente en formato JSON hacia la API
                CRUD<Cliente>.Create(cliente);
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: CLIENTES/Edit/5
        public IActionResult Edit(int id)
        {
            var cliente = CRUD<Cliente>.GetById(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: CLIENTES/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("IdCliente,Cedula,Nombre,Apellido,Telefono,Direccion,email")] Cliente cliente)
        {
            if (id != cliente.IdCliente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Envía la actualización del cliente mediante HTTP PUT a la API
                    CRUD<Cliente>.Update(id, cliente);
                }
                catch
                {
                    // Si ocurre un error de red o de base de datos remoto
                    return View(cliente);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: CLIENTES/Delete/5
        public IActionResult Delete(int id)
        {
            var cliente = CRUD<Cliente>.GetById(id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: CLIENTES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Envía la orden de eliminación mediante HTTP DELETE a la API
            CRUD<Cliente>.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
