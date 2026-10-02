using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mueblesGeanella.Modelos
{
    [Table("productos")]
    public class Producto
    {
        [Key]
        [Column("id_producto")]
        public int IdProducto { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("nombre")]
        public string Nombre { get; set; }

        [Required]
        [Column("descripcion")]
        public string Descripcion { get; set; }

        [Required]
        [Column("precio_unitario", TypeName = "numeric(10,2)")]
        public decimal PrecioUnitario { get; set; }

        [Required]
        [Column("stock")]
        public int Stock {  get; set; }

        //Llave foranea
        [ForeignKey("fabricante")]
        [Column("id_fabricante")]
        public int IdFabricante { get; set; }
        public Fabricante? fabricante { get; set; }

        //relaciones
        public List<DetalleFactura> DetalleFacturas { get; set; } = new List<DetalleFactura>();
    }
}
