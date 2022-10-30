using SistemaVenta.Entity.Comunes;

namespace SistemaVenta.Entity.Models
{
    public class TipoDocumentoVenta : RegistroComun
    {
        public Guid TipoDocumentoVentaId { get; set; }
        public string Descripcion { get; set; }

        public ICollection<Venta> Ventas { get; set; }
    }
}
