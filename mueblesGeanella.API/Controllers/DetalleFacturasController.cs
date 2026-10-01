using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mueblesGeanella.Modelos;

[Route("api/[controller]")]
[ApiController]
public class DetalleFacturasController : ControllerBase
{
    private readonly mueblesGeanellaAPIContext _context;
    public DetalleFacturasController(mueblesGeanellaAPIContext context)
    {
        _context = context;
    }

    // GET: api/DetalleFactura
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DetalleFactura>>> GetDetalleFactura()
    {
        return await _context.DetalleFactura.ToListAsync();
    }

    // GET: api/DetalleFactura/5
    [HttpGet("{iddetalle}")]
    public async Task<ActionResult<DetalleFactura>> GetDetalleFactura(int iddetalle)
    {
        var detallefactura = await _context.DetalleFactura.FindAsync(iddetalle);

        if (detallefactura == null)
        {
            return NotFound();
        }

        return detallefactura;
    }

    // PUT: api/DetalleFactura/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{iddetalle}")]
    public async Task<IActionResult> PutDetalleFactura(int? iddetalle, DetalleFactura detallefactura)
    {
        if (iddetalle != detallefactura.IdDetalle)
        {
            return BadRequest();
        }

        _context.Entry(detallefactura).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!DetalleFacturaExists(iddetalle))
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

    // POST: api/DetalleFactura
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<DetalleFactura>> PostDetalleFactura(DetalleFactura detallefactura)
    {
        _context.DetalleFactura.Add(detallefactura);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetDetalleFactura", new { iddetalle = detallefactura.IdDetalle }, detallefactura);
    }

    // DELETE: api/DetalleFactura/5
    [HttpDelete("{iddetalle}")]
    public async Task<IActionResult> DeleteDetalleFactura(int? iddetalle)
    {
        var detallefactura = await _context.DetalleFactura.FindAsync(iddetalle);
        if (detallefactura == null)
        {
            return NotFound();
        }

        _context.DetalleFactura.Remove(detallefactura);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool DetalleFacturaExists(int? iddetalle)
    {
        return _context.DetalleFactura.Any(e => e.IdDetalle == iddetalle);
    }
}
