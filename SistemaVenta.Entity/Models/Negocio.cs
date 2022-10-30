namespace SistemaVenta.Entity.Models
{
    public class Negocio
    {
        public Guid NegocioId { get; set; }
        public string UrlNegocio { get; set; }
        public string NombreLogo { get; set; }
        public string NombreDocumento { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public decimal PorcentajeImpuestos { get; set; }
        public string SimboloMoneda { get; set; }
    }
}
