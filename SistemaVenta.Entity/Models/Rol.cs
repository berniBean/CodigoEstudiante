

using SistemaVenta.Entity.Comunes;

namespace SistemaVenta.Entity.Models
{
    public class Rol : RegistroComun
    {
        public Guid RolId { get; set; }
        public string Descripcion { get; set; }
        public ICollection<Usuario> Usuarios { get; set; }
        public ICollection<RolMenu> RolMenus { get; set; }
        
    }
}
