namespace SistemaVentas.BLL.Interfaces
{
    public interface IFireBaseServices
    {
        Task<string> SubirStorage(Stream StreamArchivo, string CarpetaDestino, string NombreArchivo);

        Task<bool> EliminarStorage(string CarpetaDestino, string NombreArchivo);

    }
}
