

using SistemaVenta.Entity.Comunes;

namespace SistemaVenta.Entity.Models
{
    public class Usuario : RegistroComun
    {
        public Guid UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public Guid RolId { get; set; }
        public string UrlFoto { get; set; }
        public string NombreFoto { get; set; }
        public string Clave { get; set; }

        public Rol RolNav { get; set; }
        public IEnumerable<Venta> Ventas { get; set; }


    }
}
