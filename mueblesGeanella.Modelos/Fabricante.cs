using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mueblesGeanella.Modelos
{
    [Table("fabricantes")]
    public class Fabricante
    {
        [Key]
        [Column ("id_fabricante")]
        public int IdFabricante { get; set; }

        [Required]
        [Column("nombre")]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        [Column("apellido")]
        [MaxLength(100)]
        public string Apellido { get; set; }

        [Required]
        [Column("servicio")]
        [MaxLength(100)]
        public string Servicio { get; set; }

        [Required]
        [Column("telefono")]
        [MaxLength(10)]
        public string Telefono { get; set; }

        //relaciones
        public List<Producto> Productos { get; set; } = new List<Producto>();
    }
}
