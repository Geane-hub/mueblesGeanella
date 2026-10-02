using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mueblesGeanella.Modelos
{
    [Table("detalle_facturas")] 
    public class DetalleFactura
    {
        [Key]
        [Column("id_detalle")]
        public int IdDetalle { get; set; }

        [Required]
        [Column("cantidad")]
        public int Cantidad { get; set; }

        [Required]
        [Column("precio", TypeName = "numeric(10,2)")]
        public decimal PrecioUnitario { get; set; } 

        // Llave foranea a Factura
        [ForeignKey("Factura")]
        [Column("id_factura")]
        public int IdFactura { get; set; }
        public Factura? Factura { get; set; }

        // Llave foranea a Producto
        [ForeignKey("Producto")]
        [Column("id_producto")]
        public int IdProducto { get; set; }
        public Producto? Producto { get; set; }

        [NotMapped]
        public decimal Subtotal => Cantidad * PrecioUnitario;
    }
}
