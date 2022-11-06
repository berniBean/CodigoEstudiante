using System.Text;
using SistemaVenta.DAL.Interfaces;
using SistemaVenta.Entity.Models;
using SistemaVentas.BLL.Interfaces;
using System.Net;
using System.Net.Mail; 

namespace SistemaVentas.BLL.Implementacion
{
    public class CorreoService : ICorreoService
    {
        private readonly IGenericRepository<Configuracion> _repository;

        public CorreoService(IGenericRepository<Configuracion> repository)
        {
            _repository = repository;
        }

        public async Task<bool> EnviarCorre(string CorreoDestino, string Asunto, string Mensaje)
        {
            try
            {
                IQueryable<Configuracion> query = await _repository.Consultar(c => c.Recurso.Equals("Servicio_Correo"));

                Dictionary<string, string> config = query.ToDictionary(keySelector: c => c.Propiedad, elementSelector: c => c.Valor);

                var Credenciales = new NetworkCredential(config["correo"], config["clave"]);

                var correo = new MailMessage()
                {
                    From = new MailAddress(config["correo"], config["alias"]),
                    Subject = Asunto,
                    Body = Mensaje,
                    IsBodyHtml = true

                };

                correo.To.Add(new MailAddress(CorreoDestino));


                var clienteServidor = new SmtpClient()
                {
                    Host = config["host"],
                    Port = int.Parse(config["puerto"]),
                    Credentials = Credenciales,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = true
                };

                clienteServidor.Send(correo);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}
