
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mueblesGeanella.Modelos;

public class DetalleFacturasController : Controller
{
    private readonly mueblesGeanellaAPIContext _context;

    public DetalleFacturasController(mueblesGeanellaAPIContext context)
    {
        _context = context;
    }

    // GET: DETALLEFACTURAS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.DetalleFactura.ToListAsync());
    }

    // GET: DETALLEFACTURAS/Details/5
    public async Task<IActionResult> Details(int? iddetalle)
    {
        if (iddetalle == null)
        {
            return NotFound();
        }

        var detallefactura = await _context.DetalleFactura
            .FirstOrDefaultAsync(m => m.IdDetalle == iddetalle);
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
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("IdDetalle,Cantidad,Precio,IdFactura,factura,IdProducto,producto")] DetalleFactura detallefactura)
    {
        if (ModelState.IsValid)
        {
            _context.Add(detallefactura);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(detallefactura);
    }

    // GET: DETALLEFACTURAS/Edit/5
    public async Task<IActionResult> Edit(int? iddetalle)
    {
        if (iddetalle == null)
        {
            return NotFound();
        }

        var detallefactura = await _context.DetalleFactura.FindAsync(iddetalle);
        if (detallefactura == null)
        {
            return NotFound();
        }
        return View(detallefactura);
    }

    // POST: DETALLEFACTURAS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? iddetalle, [Bind("IdDetalle,Cantidad,Precio,IdFactura,factura,IdProducto,producto")] DetalleFactura detallefactura)
    {
        if (iddetalle != detallefactura.IdDetalle)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(detallefactura);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleFacturaExists(detallefactura.IdDetalle))
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
        return View(detallefactura);
    }

    // GET: DETALLEFACTURAS/Delete/5
    public async Task<IActionResult> Delete(int? iddetalle)
    {
        if (iddetalle == null)
        {
            return NotFound();
        }

        var detallefactura = await _context.DetalleFactura
            .FirstOrDefaultAsync(m => m.IdDetalle == iddetalle);
        if (detallefactura == null)
        {
            return NotFound();
        }

        return View(detallefactura);
    }

    // POST: DETALLEFACTURAS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? iddetalle)
    {
        var detallefactura = await _context.DetalleFactura.FindAsync(iddetalle);
        if (detallefactura != null)
        {
            _context.DetalleFactura.Remove(detallefactura);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool DetalleFacturaExists(int? iddetalle)
    {
        return _context.DetalleFactura.Any(e => e.IdDetalle == iddetalle);
    }
}
