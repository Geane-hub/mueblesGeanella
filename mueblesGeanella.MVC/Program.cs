using mueblesGeanella.CONSUMER;
using mueblesGeanella.Modelos;

namespace mueblesGeanella.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CRUD<Cliente>.Endpoint = "https://localhost:7159/api/Clientes";
            CRUD<DetalleFactura>.Endpoint = "https://localhost:7159/api/DetalleFacturas";
            CRUD<Fabricante>.Endpoint = "https://localhost:7159/api/Fabricantes";
            CRUD<Factura>.Endpoint = "https://localhost:7159/api/Facturas";
            CRUD<Producto>.Endpoint = "https://localhost:7159/api/Productos";

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            // Configurar la sesión para el sistema de Login
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseSession();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}

