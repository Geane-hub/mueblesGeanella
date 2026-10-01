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

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}

