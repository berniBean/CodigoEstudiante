using SistemaVentas.BLL.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace SistemaVentas.BLL.Implementacion
{
    public class UtilidadesServices : IUtilidadesServices
    {
        public string GenerarClave()
        {
            string clave = Guid.NewGuid().ToString("N").Substring(0,6);
            return clave;
        }
        public string ConversionSha256(string texto)
        {
            StringBuilder sb = new StringBuilder();

            using (SHA256 hash= SHA256Managed.Create())
            {
                Encoding encoder = Encoding.UTF8;

                byte[] result = hash.ComputeHash(encoder.GetBytes(texto));

                foreach (var item in result)
                {
                    sb.Append(item.ToString("x2"));
                }
            }

            return sb.ToString();
        }


    }
}
