using mueblesGeanella.Modelos;

namespace mueblesGeanella.MVC.Modelos 
{
    public class FacturaViewModel
    {
        public Factura Factura { get; set; }
        public Cliente Cliente { get; set; }
        public Producto Producto { get; set; }
    }
}
