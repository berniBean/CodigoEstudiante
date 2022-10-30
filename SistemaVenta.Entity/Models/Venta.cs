namespace SistemaVenta.Entity.Models
{
    public class Venta
    {
        public Guid VentaId { get; set; }
        public string NumeroVenta { get; set; }
        public string DocumentoCliente { get; set; }
        public string NombreCliente { get; set; }
        public decimal SubTotal { get; set; }
        public decimal ImpuestoTotal { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaRegistro { get; set; }

        public Guid UsuarioId { get; set; }
        public Usuario UsuarioNav { get; set; }

        public Guid IdTipoDocumentoVenta { get; set; }
        public TipoDocumentoVenta TipoDocumentosVentasNav { get; set; }





    }
}
