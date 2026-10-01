using Microsoft.EntityFrameworkCore;

public class mueblesGeanellaAPIContext(DbContextOptions<mueblesGeanellaAPIContext> options) : DbContext(options)
{
    public DbSet<mueblesGeanella.Modelos.Cliente> Cliente { get; set; } = default!;
    public DbSet<mueblesGeanella.Modelos.DetalleFactura> DetalleFactura { get; set; } = default!;
    public DbSet<mueblesGeanella.Modelos.Fabricante> Fabricante { get; set; } = default!;
    public DbSet<mueblesGeanella.Modelos.Factura> Factura { get; set; } = default!;
    public DbSet<mueblesGeanella.Modelos.Producto> Producto { get; set; } = default!;
}
