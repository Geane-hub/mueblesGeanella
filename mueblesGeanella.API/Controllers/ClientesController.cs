using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mueblesGeanella.Modelos;

[Route("api/[controller]")]
[ApiController]
public class ClientesController : ControllerBase
{
    private readonly mueblesGeanellaAPIContext _context;
    public ClientesController(mueblesGeanellaAPIContext context)
    {
        _context = context;
    }

    // GET: api/Cliente
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cliente>>> GetCliente()
    {
        return await _context.Cliente.ToListAsync();
    }

    // GET: api/Cliente/5
    [HttpGet("{idcliente}")]
    public async Task<ActionResult<Cliente>> GetCliente(int idcliente)
    {
        var cliente = await _context.Cliente.FindAsync(idcliente);

        if (cliente == null)
        {
            return NotFound();
        }

        return cliente;
    }

    // PUT: api/Cliente/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{idcliente}")]
    public async Task<IActionResult> PutCliente(int? idcliente, Cliente cliente)
    {
        if (idcliente != cliente.IdCliente)
        {
            return BadRequest();
        }

        _context.Entry(cliente).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ClienteExists(idcliente))
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

    // POST: api/Cliente
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
    {
        _context.Cliente.Add(cliente);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetCliente", new { idcliente = cliente.IdCliente }, cliente);
    }

    // DELETE: api/Cliente/5
    [HttpDelete("{idcliente}")]
    public async Task<IActionResult> DeleteCliente(int? idcliente)
    {
        var cliente = await _context.Cliente.FindAsync(idcliente);
        if (cliente == null)
        {
            return NotFound();
        }

        _context.Cliente.Remove(cliente);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ClienteExists(int? idcliente)
    {
        return _context.Cliente.Any(e => e.IdCliente == idcliente);
    }
}
