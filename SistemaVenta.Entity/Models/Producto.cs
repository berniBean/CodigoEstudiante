using SistemaVenta.Entity.Comunes;

namespace SistemaVenta.Entity.Models
{
    public class Producto: RegistroComun
    {
        public Guid ProductoId { get; set; }
        public string CodigoDeBarra { get; set; }
        public string Marca { get; set; }
        public string Descripcion { get; set; }
        public string UrlImagen { get; set; }
        public string NombreImagen { get; set; }
        public decimal Precio { get; set; }
        public Guid CategoriaId { get; set; }
        public Categoria CategoriaNav { get; set; }
    }
}
