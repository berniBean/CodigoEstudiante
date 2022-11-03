namespace SistemaVentas.BLL.Interfaces
{
    public interface IUtilidadesServices
    {
        string GenerarClave();
        string ConversionSha256(string texto);
    }
}
