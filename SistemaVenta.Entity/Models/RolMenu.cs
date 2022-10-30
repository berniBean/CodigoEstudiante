

using SistemaVenta.Entity.Comunes;

namespace SistemaVenta.Entity.Models
{
    public class RolMenu : RegistroComun
    {
        public Guid RolId { get; set; }
        public Guid MenuId { get; set; }

        public Menu Menu { get; set; }
        public Rol Rol { get; set; }
    }
}
