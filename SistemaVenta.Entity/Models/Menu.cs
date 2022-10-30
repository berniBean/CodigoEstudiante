using SistemaVenta.Entity.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.Entity.Models
{
    public class Menu : RegistroComun
    {
        public Guid MenuId { get; set; }
        public string Descripcion { get; set; }
        public Guid IdMenuPadre { get; set; }
        public string Icono { get; set; }
        public string PaginaAccion { get; set; }

        public ICollection<RolMenu> RolMenus { get; set; }
        
    }
}
