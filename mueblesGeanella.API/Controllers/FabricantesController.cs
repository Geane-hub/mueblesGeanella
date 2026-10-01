
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mueblesGeanella.Modelos;

public class FabricantesController : Controller
{
    private readonly mueblesGeanellaAPIContext _context;

    public FabricantesController(mueblesGeanellaAPIContext context)
    {
        _context = context;
    }

    // GET: FABRICANTES
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Fabricante.ToListAsync());
    }

    // GET: FABRICANTES/Details/5
    public async Task<IActionResult> Details(int? idfabricante)
    {
        if (idfabricante == null)
        {
            return NotFound();
        }

        var fabricante = await _context.Fabricante
            .FirstOrDefaultAsync(m => m.IdFabricante == idfabricante);
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
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("IdFabricante,Nombre,Apellido,Servicio,Telefono")] Fabricante fabricante)
    {
        if (ModelState.IsValid)
        {
            _context.Add(fabricante);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(fabricante);
    }

    // GET: FABRICANTES/Edit/5
    public async Task<IActionResult> Edit(int? idfabricante)
    {
        if (idfabricante == null)
        {
            return NotFound();
        }

        var fabricante = await _context.Fabricante.FindAsync(idfabricante);
        if (fabricante == null)
        {
            return NotFound();
        }
        return View(fabricante);
    }

    // POST: FABRICANTES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? idfabricante, [Bind("IdFabricante,Nombre,Apellido,Servicio,Telefono")] Fabricante fabricante)
    {
        if (idfabricante != fabricante.IdFabricante)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(fabricante);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FabricanteExists(fabricante.IdFabricante))
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
        return View(fabricante);
    }

    // GET: FABRICANTES/Delete/5
    public async Task<IActionResult> Delete(int? idfabricante)
    {
        if (idfabricante == null)
        {
            return NotFound();
        }

        var fabricante = await _context.Fabricante
            .FirstOrDefaultAsync(m => m.IdFabricante == idfabricante);
        if (fabricante == null)
        {
            return NotFound();
        }

        return View(fabricante);
    }

    // POST: FABRICANTES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? idfabricante)
    {
        var fabricante = await _context.Fabricante.FindAsync(idfabricante);
        if (fabricante != null)
        {
            _context.Fabricante.Remove(fabricante);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool FabricanteExists(int? idfabricante)
    {
        return _context.Fabricante.Any(e => e.IdFabricante == idfabricante);
    }
}
