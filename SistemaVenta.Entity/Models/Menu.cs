using SistemaVenta.Entity.Comunes;

namespace SistemaVenta.Entity.Models
{
    public class Menu : RegistroComun
    {
        public Guid MenuId { get; set; }
        public string? Descripcion { get; set; }
        public Guid IdMenuPadre { get; set; }
        public string? Icono { get; set; }
        public string? Controlador { get; set; }
        public string? PaginaAccion { get; set; }

        public  Menu? IdMenuPadreNavigation { get; set; }
        public  ICollection<Menu>? InverseIdMenuPadreNavigation { get; set; }
        public ICollection<RolMenu>? RolMenus { get; set; }
        
    }
}
