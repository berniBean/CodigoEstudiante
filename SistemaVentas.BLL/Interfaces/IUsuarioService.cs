using SistemaVenta.Entity.Models;

namespace SistemaVentas.BLL.Interfaces
{
    public interface IUsuarioService
    {
        Task<List<Usuario>> Lista();
        Task<Usuario> Crear(Usuario usuario, Stream Foto = null, string NombreFoto = null, string UrlPlantillaCorreo="");
        Task<Usuario> Editar(Usuario usuario, Stream Foto = null, string NombreFoto = null);
        Task<bool> Eliminar(Guid IdUsuario);
        Task<Usuario> ObtenerPorCredenciales(string email,string clave);
        Task<Usuario> ObtenerPorId(Guid IdUsuario);
        Task<bool> GuardarPerfil(Usuario usuario);
        Task<bool> CambiarClave(Guid IdUsuario, string ClaveActual, string ClaveNueva);
        Task<bool> RestablecerClave(string correo, string UrlPlantillaCorreo);

    }
}
