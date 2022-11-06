using SistemaVenta.Entity.Models;

namespace MVCVentas.Models.ViewModel
{
    public class VMMenu
    {
        public string? Descripcion { get; set; } 
        public string? Icono { get; set; }
        public string? PaginaAccion { get; set; }

        public ICollection<VMMenu>? SubMenus { get; set; }

    }
}
