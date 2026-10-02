using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mueblesGeanella.Modelos
{
    [Table("facturas")]
    public class Factura
    {
        [Key]
        [Column("id_factura")]
        public int IdFactura { get; set; }

        [Required]
        [Column("fecha", TypeName = "timestamp")]
        public  DateTime Fecha { get; set; }

        [Required]
        [Column("monto_total", TypeName ="numeric(10,2)")]
        public decimal MontoTotal { get; set; }
        
        //Llave foranea
        [ForeignKey("cliente")]
        [Column("id_cliente")]
        public int IdCliente { get; set; }
        public Cliente? cliente { get; set; }

        //relaciones
        public List<DetalleFactura>? DetalleFacturas { get; set; } = new List<DetalleFactura>();
    }
}
