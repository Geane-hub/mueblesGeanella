using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace mueblesGeanella.API.Migrations
{
    /// <inheritdoc />
    public partial class InicialMuebles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clientes",
                columns: table => new
                {
                    id_cliente = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cedula = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    apellido = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    telefono = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    direccion = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clientes", x => x.id_cliente);
                });

            migrationBuilder.CreateTable(
                name: "fabricantes",
                columns: table => new
                {
                    id_fabricante = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    apellido = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    servicio = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    telefono = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fabricantes", x => x.id_fabricante);
                });

            migrationBuilder.CreateTable(
                name: "facturas",
                columns: table => new
                {
                    id_factura = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fecha = table.Column<DateTime>(type: "timestamp", nullable: false),
                    monto_total = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    id_cliente = table.Column<int>(type: "integer", nullable: false),
                    clienteIdCliente = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_facturas", x => x.id_factura);
                    table.ForeignKey(
                        name: "FK_facturas_clientes_clienteIdCliente",
                        column: x => x.clienteIdCliente,
                        principalTable: "clientes",
                        principalColumn: "id_cliente");
                });

            migrationBuilder.CreateTable(
                name: "productos",
                columns: table => new
                {
                    id_producto = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: false),
                    precio_unitario = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    stock = table.Column<int>(type: "integer", nullable: false),
                    id_fabricante = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productos", x => x.id_producto);
                    table.ForeignKey(
                        name: "FK_productos_fabricantes_id_fabricante",
                        column: x => x.id_fabricante,
                        principalTable: "fabricantes",
                        principalColumn: "id_fabricante",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "detalle_factura",
                columns: table => new
                {
                    id_detalle = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cantidad = table.Column<int>(type: "integer", nullable: false),
                    precio = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    id_factura = table.Column<int>(type: "integer", nullable: false),
                    facturaIdFactura = table.Column<int>(type: "integer", nullable: true),
                    id_producto = table.Column<int>(type: "integer", nullable: false),
                    productoIdProducto = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detalle_factura", x => x.id_detalle);
                    table.ForeignKey(
                        name: "FK_detalle_factura_facturas_facturaIdFactura",
                        column: x => x.facturaIdFactura,
                        principalTable: "facturas",
                        principalColumn: "id_factura");
                    table.ForeignKey(
                        name: "FK_detalle_factura_productos_productoIdProducto",
                        column: x => x.productoIdProducto,
                        principalTable: "productos",
                        principalColumn: "id_producto");
                });

            migrationBuilder.CreateIndex(
                name: "IX_detalle_factura_facturaIdFactura",
                table: "detalle_factura",
                column: "facturaIdFactura");

            migrationBuilder.CreateIndex(
                name: "IX_detalle_factura_productoIdProducto",
                table: "detalle_factura",
                column: "productoIdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_facturas_clienteIdCliente",
                table: "facturas",
                column: "clienteIdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_productos_id_fabricante",
                table: "productos",
                column: "id_fabricante");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "detalle_factura");

            migrationBuilder.DropTable(
                name: "facturas");

            migrationBuilder.DropTable(
                name: "productos");

            migrationBuilder.DropTable(
                name: "clientes");

            migrationBuilder.DropTable(
                name: "fabricantes");
        }
    }
}
