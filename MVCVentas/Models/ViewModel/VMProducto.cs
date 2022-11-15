namespace MVCVentas.Models.ViewModel
{
    public class VMProducto : ClaseActiva
    {
        public Guid ProductoId { get; set; }
        public Guid CategoriaId { get; set; }
        public string NombreCategoria { get; set; }
        public string Marca { get; set; }
        public string Descripcion { get; set; }
        public string CodigoDeBarra { get; set; }
        public int Stock { get; set; }
        public string UrlImagen { get; set; }
        public string NombreImagen { get; set; }
        public decimal Precio { get; set; }
        public int? EsActivo { get; set ; }
    }
}
