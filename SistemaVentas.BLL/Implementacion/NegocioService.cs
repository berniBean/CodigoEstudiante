using Microsoft.EntityFrameworkCore;
using SistemaVenta.BLL.Interfaces;
using SistemaVenta.DAL.Interfaces;
using SistemaVenta.Entity.Models;
using SistemaVentas.BLL.Interfaces;

namespace SistemaVenta.BLL.Implementacion
{
    public class NegocioService : INegocioService
    {
        private readonly IGenericRepository<Negocio> _repository;
        private readonly IFireBaseServices _fireBaseServices;

        public NegocioService(IGenericRepository<Negocio> repository, IFireBaseServices fireBaseServices)
        {
            _repository = repository;
            _fireBaseServices = fireBaseServices;
        }

        public async Task<Negocio> Obtener()
        {
            try
            {
                var negocio_encotrado = await _repository.Consultar();
                      var unico=   negocio_encotrado.FirstOrDefault();
                return unico;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        public async Task<Negocio> GuardarCambios(Negocio entidad, Stream? Logo = null, string NombreLogo = "")
        {
            try
            {
                var negocio_encotrado = await _repository.Consultar();
                var unico = await negocio_encotrado.FirstOrDefaultAsync();

                unico.NumeroDocumento = entidad.NumeroDocumento;
                unico.Nombre = entidad.Nombre;
                unico.Correo = entidad.Correo;
                unico.Direccion = entidad.Direccion;
                unico.Telefono = entidad.Telefono;
                unico.PorcentajeImpuestos = entidad.PorcentajeImpuestos;
                unico.SimboloMoneda = entidad.SimboloMoneda;
                
                if(entidad.NombreLogo is null)
                {
                    unico.NombreLogo = NombreLogo;
                }
                else
                {
                    unico.NombreLogo = entidad.NombreLogo;
                }
                //unico.NombreLogo = entidad.NombreLogo == "" ? NombreLogo : unico.NombreLogo;
                if (Logo != null)
                {
                    string urlFoto=await _fireBaseServices.SubirStorage(Logo,"carpeta_logo",unico.NombreLogo);
                    unico.UrlLogo = urlFoto;
                }

                await _repository.Editar(unico);
                return unico;
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
