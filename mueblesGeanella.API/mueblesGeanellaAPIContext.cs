using Microsoft.EntityFrameworkCore;

public class mueblesGeanellaAPIContext(DbContextOptions<mueblesGeanellaAPIContext> options) : DbContext(options)
{
    public DbSet<mueblesGeanella.Modelos.Cliente> Cliente { get; set; } = default!;
}
