using SistemaVenta.Entity.Models;

namespace MVCVentas.Models.ViewModel
{
    public class VMVenta
    {
        public string? VentaId { get; set; }
        public string? NumeroVenta { get; set; }
        public string? IdTipoDocumentoVenta { get; set; }
        public string? TipoDocumentoVenta { get; set; }
        public string? UsuarioId { get; set; }
        public string? Usuario { get; set; }
        public Usuario? UsuarioNav { get; set; }
        public string? DocumentoCliente { get; set; }
        public string? NombreCliente { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? ImpuestoTotal { get; set; }
        public decimal? Total { get; set; }
        public string? FechaRegistro { get; set; }

        public ICollection<VMDetalleVenta>? DetalleVenta { get; set; }
    }
}
