
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mueblesGeanella.Modelos;

public class FacturasController : Controller
{
    private readonly mueblesGeanellaMVCContext _context;

    public FacturasController(mueblesGeanellaMVCContext context)
    {
        _context = context;
    }

    // GET: FACTURAS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Factura.ToListAsync());
    }

    // GET: FACTURAS/Details/5
    public async Task<IActionResult> Details(int? idfactura)
    {
        if (idfactura == null)
        {
            return NotFound();
        }

        var factura = await _context.Factura
            .FirstOrDefaultAsync(m => m.IdFactura == idfactura);
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
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("IdFactura,Fecha,MontoTotal,IdCliente,cliente")] Factura factura)
    {
        if (ModelState.IsValid)
        {
            _context.Add(factura);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(factura);
    }

    // GET: FACTURAS/Edit/5
    public async Task<IActionResult> Edit(int? idfactura)
    {
        if (idfactura == null)
        {
            return NotFound();
        }

        var factura = await _context.Factura.FindAsync(idfactura);
        if (factura == null)
        {
            return NotFound();
        }
        return View(factura);
    }

    // POST: FACTURAS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? idfactura, [Bind("IdFactura,Fecha,MontoTotal,IdCliente,cliente")] Factura factura)
    {
        if (idfactura != factura.IdFactura)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(factura);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FacturaExists(factura.IdFactura))
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
        return View(factura);
    }

    // GET: FACTURAS/Delete/5
    public async Task<IActionResult> Delete(int? idfactura)
    {
        if (idfactura == null)
        {
            return NotFound();
        }

        var factura = await _context.Factura
            .FirstOrDefaultAsync(m => m.IdFactura == idfactura);
        if (factura == null)
        {
            return NotFound();
        }

        return View(factura);
    }

    // POST: FACTURAS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? idfactura)
    {
        var factura = await _context.Factura.FindAsync(idfactura);
        if (factura != null)
        {
            _context.Factura.Remove(factura);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool FacturaExists(int? idfactura)
    {
        return _context.Factura.Any(e => e.IdFactura == idfactura);
    }
}
