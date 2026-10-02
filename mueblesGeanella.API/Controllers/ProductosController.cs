using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mueblesGeanella.Modelos;

[Route("api/[controller]")]
[ApiController]
public class ProductosController : ControllerBase
{
    private readonly mueblesGeanellaAPIContext _context;
    public ProductosController(mueblesGeanellaAPIContext context)
    {
        _context = context;
    }

    // GET: api/Producto
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Producto>>> GetProducto()
    {
        return await _context.Producto.ToListAsync();
    }

    // GET: api/Producto/5
    [HttpGet("{idproducto}")]
    public async Task<ActionResult<Producto>> GetProducto(int idproducto)
    {
        var producto = await _context.Producto.FindAsync(idproducto);

        if (producto == null)
        {
            return NotFound();
        }

        return producto;
    }

    // PUT: api/Producto/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{idproducto}")]
    public async Task<IActionResult> PutProducto(int? idproducto, Producto producto)
    {
        if (idproducto != producto.IdProducto)
        {
            return BadRequest();
        }

        _context.Entry(producto).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProductoExists(idproducto))
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

    // POST: api/Producto
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Producto>> PostProducto(Producto producto)
    {
        _context.Producto.Add(producto);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetProducto", new { idproducto = producto.IdProducto }, producto);
    }

    // DELETE: api/Producto/5
    [HttpDelete("{idproducto}")]
    public async Task<IActionResult> DeleteProducto(int? idproducto)
    {
        var producto = await _context.Producto.FindAsync(idproducto);
        if (producto == null)
        {
            return NotFound();
        }

        _context.Producto.Remove(producto);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return BadRequest("No se puede eliminar el producto porque está asociado a otros registros (por ejemplo, en Detalles de Factura). Elimine primero las referencias.");
        }

        return NoContent();
    }

    private bool ProductoExists(int? idproducto)
    {
        return _context.Producto.Any(e => e.IdProducto == idproducto);
    }
}
