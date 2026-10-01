
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mueblesGeanella.Modelos;

public class ProductosController : Controller
{
    private readonly mueblesGeanellaAPIContext _context;

    public ProductosController(mueblesGeanellaAPIContext context)
    {
        _context = context;
    }

    // GET: PRODUCTOS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Producto.ToListAsync());
    }

    // GET: PRODUCTOS/Details/5
    public async Task<IActionResult> Details(int? idproducto)
    {
        if (idproducto == null)
        {
            return NotFound();
        }

        var producto = await _context.Producto
            .FirstOrDefaultAsync(m => m.IdProducto == idproducto);
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
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("IdProducto,Nombre,Descripcion,PrecioUnitario,Stock,IdFabricante,fabricante")] Producto producto)
    {
        if (ModelState.IsValid)
        {
            _context.Add(producto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(producto);
    }

    // GET: PRODUCTOS/Edit/5
    public async Task<IActionResult> Edit(int? idproducto)
    {
        if (idproducto == null)
        {
            return NotFound();
        }

        var producto = await _context.Producto.FindAsync(idproducto);
        if (producto == null)
        {
            return NotFound();
        }
        return View(producto);
    }

    // POST: PRODUCTOS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? idproducto, [Bind("IdProducto,Nombre,Descripcion,PrecioUnitario,Stock,IdFabricante,fabricante")] Producto producto)
    {
        if (idproducto != producto.IdProducto)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(producto);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(producto.IdProducto))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(producto);
    }

    // GET: PRODUCTOS/Delete/5
    public async Task<IActionResult> Delete(int? idproducto)
    {
        if (idproducto == null)
        {
            return NotFound();
        }

        var producto = await _context.Producto
            .FirstOrDefaultAsync(m => m.IdProducto == idproducto);
        if (producto == null)
        {
            return NotFound();
        }

        return View(producto);
    }

    // POST: PRODUCTOS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? idproducto)
    {
        var producto = await _context.Producto.FindAsync(idproducto);
        if (producto != null)
        {
            _context.Producto.Remove(producto);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ProductoExists(int? idproducto)
    {
        return _context.Producto.Any(e => e.IdProducto == idproducto);
    }
}
