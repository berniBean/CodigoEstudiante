using SistemaVenta.Entity.Comunes;
using System.ComponentModel.DataAnnotations;

namespace SistemaVenta.Entity.Models
{
    public class Categoria : RegistroComun
    {
        
        public Guid CategoriaId { get; set; }
        public string Descripcion { get; set; }
        public ICollection<Producto> Productos { get; set; }
        
    }
}
