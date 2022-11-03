using Firebase.Auth;
using Firebase.Storage;
using SistemaVenta.DAL.Interfaces;
using SistemaVenta.Entity.Models;
using SistemaVentas.BLL.Interfaces;

namespace SistemaVentas.BLL.Implementacion
{
    public class FirebaseService : IFireBaseServices
    {

        public readonly IGenericRepository<Configuracion> _repository;

        public FirebaseService(IGenericRepository<Configuracion> repository)
        {
            _repository = repository;
        }

        public async Task<string> SubirStorage(Stream StreamArchivo, string CarpetaDestino, string NombreArchivo)
        {
            string UrlImagen = "";

            try
            {
                IQueryable<Configuracion> query = await _repository.Consultar(c => c.Recurso.Equals("FireBase_Storage"));

                Dictionary<string, string> config = query.ToDictionary(keySelector: c => c.Propiedad, elementSelector: c => c.Valor);

                var auth = new FirebaseAuthProvider(new FirebaseConfig(config["api_key"]));
                var a = await auth.SignInWithEmailAndPasswordAsync(config["email"], config["clave"]);
                var cancell = new CancellationTokenSource();

                var task = new FirebaseStorage(
                    config["ruta"],
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true
                    })
                    .Child(CarpetaDestino)
                    .Child(NombreArchivo)
                    .PutAsync(StreamArchivo,cancell.Token);


                UrlImagen = await task;



            }
            catch (Exception)
            {
                UrlImagen = "";
                throw;
            }

            return UrlImagen;
        }

        public async Task<bool> EliminarStorage(string CarpetaDestino, string NombreArchivo)
        {
            try
            {
                IQueryable<Configuracion> query = await _repository.Consultar(c => c.Recurso.Equals("FireBase_Storage"));

                Dictionary<string, string> config = query.ToDictionary(keySelector: c => c.Propiedad, elementSelector: c => c.Valor);

                var auth = new FirebaseAuthProvider(new FirebaseConfig(config["api_key"]));
                var a = await auth.SignInWithEmailAndPasswordAsync(config["email"], config["clave"]);
                var cancell = new CancellationTokenSource();

                var task = new FirebaseStorage(
                    config["ruta"],
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true
                    })
                    .Child(CarpetaDestino)
                    .Child(NombreArchivo)
                    .DeleteAsync();


                await task;



            }
            catch (Exception)
            {
                return false;
                throw;
            }

            return true;
        }

        
    }
}
