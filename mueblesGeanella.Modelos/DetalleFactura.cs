using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mueblesGeanella.Modelos
{
    [Table("detalle_factura")]
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
        public decimal Precio { get; set; }

        //Llave foranea
        [ForeignKey("facturas")]
        [Column("id_factura")]
        public int IdFactura { get; set; }
        public Factura? factura { get; set; }

        [ForeignKey("productos")]
        [Column("id_producto")]
        public int IdProducto { get; set; }
        public Producto? producto { get; set; }

    }
}
