using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mueblesGeanella.Modelos
{
    [Table("clientes")]
    public class Cliente
    {
        [Key]
        [Column("id_cliente" )]
        public int IdCliente { get; set; }

        [Required]
        [Column("cedula")]
        [MaxLength(10)]
        public string Cedula {  get; set; }

        [Required]
        [Column("nombre")]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        [Column("apellido")]
        [MaxLength(100)]
        public string Apellido { get; set; }

        [Required]
        [Column("telefono")]
        [MaxLength (10)]
        public string Telefono {  get; set; }

        [Required]
        [Column("direccion")]
        [MaxLength(200)]
        public string Direccion { get; set; }

        [Required]
        [Column("email")]
        [MaxLength(100)]
        public string email { get; set; }

        //relaciones
        public List<Factura>? Facturas { get; set; } = new List<Factura>();
    }
}
