namespace SistemaVenta.Entity.Models
{
    public class DetalleVenta
    {
        public Guid DetalleVentaId{ get; set; }
        public string? MarcaProducto { get; set; }
        public string? DescripcionProducto { get; set; }
        public string? CategoriaProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Total { get; set; }

        public Guid ProductoId { get; set; }
        public Guid VentaId { get; set; }
        public Venta? Ventas { get; set; }

    }
}
