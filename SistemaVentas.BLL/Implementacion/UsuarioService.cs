using Microsoft.EntityFrameworkCore;
using SistemaVenta.DAL.Interfaces;
using SistemaVenta.Entity.Models;
using SistemaVentas.BLL.Interfaces;
using System.Net;
using System.Text;

namespace SistemaVentas.BLL.Implementacion
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IGenericRepository<Usuario> _repository;
        private readonly IFireBaseServices _fireBase;
        private readonly IUtilidadesServices _utilidades;
        private readonly ICorreoService _correoService;

        public UsuarioService(IGenericRepository<Usuario> repository, IFireBaseServices fireBase, IUtilidadesServices utilidades, ICorreoService correoService)
        {
            _repository = repository;
            _fireBase = fireBase;
            _utilidades = utilidades;
            _correoService = correoService;
        }

        public async Task<List<Usuario>> Lista()
        {
            IQueryable<Usuario> query = await _repository.Consultar();

            return query.Include(r => r.RolNav).ToList();
        }
        public async Task<Usuario> Crear(Usuario usuario, Stream? Foto = null, string? NombreFoto = null, string UrlPlantillaCorreo = "")
        {
            Usuario usuario_existe = await _repository.Obtener(u => u.Correo.Equals(usuario.Correo));

            if (usuario_existe != null)
            {
                throw new TaskCanceledException("El correo ya existe");
            }

            try
            {
                string clave = _utilidades.GenerarClave();
                usuario.UsuarioId = Guid.NewGuid();
                usuario.Clave = _utilidades.ConversionSha256(clave);
                usuario.NombreFoto = NombreFoto;

                if(Foto != null)
                {
                    string urlFoto = await _fireBase.SubirStorage(Foto,"carpeta_usuario",NombreFoto);
                    usuario.UrlFoto = urlFoto;
                }

                Usuario usuarioCreado = await _repository.Crear(usuario);
                if(usuarioCreado.UsuarioId == null)
                {
                    throw new TaskCanceledException("No se creo el usuario");
                }

                if(UrlPlantillaCorreo != "")
                {
                    UrlPlantillaCorreo = UrlPlantillaCorreo
                        .Replace("[correo]", usuarioCreado.Correo)
                        .Replace("[clave]", clave);

                    string htmlCorreo = "";

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(UrlPlantillaCorreo);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (Stream dataStream = response.GetResponseStream())
                        {
                            StreamReader sr = null;
                            if (response.CharacterSet == null)
                                sr = new StreamReader(dataStream);
                            else
                                sr = new StreamReader(dataStream, encoding: Encoding.GetEncoding(response.CharacterSet));

                            htmlCorreo = sr.ReadToEnd();
                            response.Close();
                            sr.Close();
                        }
                    }

                    if (htmlCorreo != "")
                        await _correoService.EnviarCorre(usuarioCreado.Correo, "Cuenta_Creada", htmlCorreo);
                }
                IQueryable<Usuario> query = await _repository.Consultar(u => u.UsuarioId.Equals(usuarioCreado.UsuarioId));
                usuarioCreado = query.Include(r => r.RolNav).First();

                return usuarioCreado;

            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<Usuario> Editar(Usuario usuario, Stream Foto = null, string NombreFoto = null)
        {
            Usuario usuario_existe = await _repository.Obtener(u => u.Correo==usuario.Correo && u.UsuarioId != usuario.UsuarioId);

            if (usuario_existe != null)
            {
                throw new TaskCanceledException("El correo ya existe");
            }

            try
            {
                IQueryable<Usuario> queryUsuario = await _repository.Consultar(u => u.UsuarioId == usuario.UsuarioId);
                Usuario usuario_editar = queryUsuario.First();
                usuario_editar.Nombre = usuario.Nombre;
                usuario_editar.Correo = usuario.Correo;
                usuario_editar.Telefono = usuario.Telefono;
                usuario_editar.RolId = usuario.RolId;
                usuario_editar.EsActivo = usuario.EsActivo;

                if( usuario_editar.NombreFoto == "")
                {
                    usuario_editar.NombreFoto = NombreFoto;
                }

                if (Foto != null)
                {
                    var urlFoto = await _fireBase.SubirStorage(Foto, "carpeta_usuario", usuario_editar.NombreFoto);
                    usuario_editar.UrlFoto = urlFoto;
                }

                bool respuesta = await _repository.Editar(usuario_editar);

                if (!respuesta)
                    throw new TaskCanceledException("No se pudo modificar el usuario");

                Usuario usuario_editado = queryUsuario.Include(r => r.RolNav).First();

                return usuario_editado;
                   
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<bool> Eliminar(Guid IdUsuario)
        {
            try
            {
                Usuario usuario_encontrado = await _repository.Obtener(u => u.UsuarioId.Equals(IdUsuario));

                if (usuario_encontrado == null)
                    throw new TaskCanceledException("EL usuario no existe");
                string nombreFoto = usuario_encontrado.NombreFoto;

                bool respuesta = await _repository.Eliminar(usuario_encontrado);

                if(respuesta)
                    await _fireBase.EliminarStorage("carpeta_usuario",nombreFoto);

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<Usuario> ObtenerPorCredenciales(string email, string clave)
        {
            string clave_encriptada = _utilidades.ConversionSha256(clave);

            Usuario usuarioEncontrado = await _repository.Obtener(u => u.Correo.Equals(email) && u.Clave.Equals(clave));

            return usuarioEncontrado;
        }
        public async Task<Usuario> ObtenerPorId(Guid IdUsuario)
        {
            IQueryable<Usuario> query = await _repository.Consultar(u => u.UsuarioId.Equals(IdUsuario));
            Usuario resultado = query.Include(r => r.RolNav).FirstOrDefault();

            return resultado;



        }
        public async Task<bool> GuardarPerfil(Usuario usuario)
        {
            try
            {
                Usuario usuarioEcontrado = await _repository.Obtener(u => u.UsuarioId.Equals(usuario.UsuarioId));
                if (usuarioEcontrado == null)
                    throw new TaskCanceledException("EL usuario no existe");

                usuarioEcontrado.Correo = usuario.Correo;
                usuarioEcontrado.Telefono = usuario.Telefono;

                bool respuesta = await _repository.Editar(usuarioEcontrado);
                return respuesta;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<bool> CambiarClave(Guid IdUsuario, string ClaveActual, string ClaveNueva)
        {
            try
            {
                Usuario usuarioEncontrado = await _repository.Obtener(u => u.UsuarioId == IdUsuario);
                if (usuarioEncontrado == null)
                    throw new TaskCanceledException("EL usuario no existe");
                if (usuarioEncontrado.Clave != _utilidades.ConversionSha256(ClaveActual))
                    throw new TaskCanceledException("La contraseña ingresada como actual no es correcta");

                usuarioEncontrado.Clave = _utilidades.ConversionSha256(ClaveNueva);

                bool respuesta = await _repository.Editar(usuarioEncontrado);

                return respuesta;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<bool> RestablecerClave(string correo, string UrlPlantillaCorreo)
        {
            try
            {
                Usuario UsuarioEncontrado = await _repository.Obtener(u => u.Correo == correo);
                if (UsuarioEncontrado == null)
                    throw new TaskCanceledException("No se encotró ningin usuario asociado");

                string claveGenerada = _utilidades.GenerarClave();
                UsuarioEncontrado.Clave = _utilidades.ConversionSha256(claveGenerada);

                UrlPlantillaCorreo = UrlPlantillaCorreo                   
                    .Replace("[Clave]", claveGenerada);

                string htmlCorreo = "";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(UrlPlantillaCorreo);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (Stream dataStream = response.GetResponseStream())
                    {
                        StreamReader sr = null;
                        if (response.CharacterSet == null)
                            sr = new StreamReader(dataStream);
                        else
                            sr = new StreamReader(dataStream, encoding: Encoding.GetEncoding(response.CharacterSet));

                        htmlCorreo = sr.ReadToEnd();
                        response.Close();
                        sr.Close();
                    }
                }

                bool correo_enviado = false;

                if (htmlCorreo != "")
                    correo_enviado = await _correoService.EnviarCorre(correo, "Contraseña restablecida", htmlCorreo);
                
                if (!correo_enviado)
                    throw new TaskCanceledException("No se pudo mandar el mendaje intenta de nuevo");

                bool respuesta = await _repository.Editar(UsuarioEncontrado);

                return respuesta;
            }
            catch (Exception)
            {

                throw;
            }
        }




 
    }
}



