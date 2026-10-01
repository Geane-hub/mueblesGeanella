using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mueblesGeanella.Modelos;

[Route("api/[controller]")]
[ApiController]
public class FabricantesController : ControllerBase
{
    private readonly mueblesGeanellaAPIContext _context;
    public FabricantesController(mueblesGeanellaAPIContext context)
    {
        _context = context;
    }

    // GET: api/Fabricante
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Fabricante>>> GetFabricante()
    {
        return await _context.Fabricante.ToListAsync();
    }

    // GET: api/Fabricante/5
    [HttpGet("{idfabricante}")]
    public async Task<ActionResult<Fabricante>> GetFabricante(int idfabricante)
    {
        var fabricante = await _context.Fabricante.FindAsync(idfabricante);

        if (fabricante == null)
        {
            return NotFound();
        }

        return fabricante;
    }

    // PUT: api/Fabricante/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{idfabricante}")]
    public async Task<IActionResult> PutFabricante(int? idfabricante, Fabricante fabricante)
    {
        if (idfabricante != fabricante.IdFabricante)
        {
            return BadRequest();
        }

        _context.Entry(fabricante).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!FabricanteExists(idfabricante))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Fabricante
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Fabricante>> PostFabricante(Fabricante fabricante)
    {
        _context.Fabricante.Add(fabricante);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetFabricante", new { idfabricante = fabricante.IdFabricante }, fabricante);
    }

    // DELETE: api/Fabricante/5
    [HttpDelete("{idfabricante}")]
    public async Task<IActionResult> DeleteFabricante(int? idfabricante)
    {
        var fabricante = await _context.Fabricante.FindAsync(idfabricante);
        if (fabricante == null)
        {
            return NotFound();
        }

        _context.Fabricante.Remove(fabricante);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool FabricanteExists(int? idfabricante)
    {
        return _context.Fabricante.Any(e => e.IdFabricante == idfabricante);
    }
}
