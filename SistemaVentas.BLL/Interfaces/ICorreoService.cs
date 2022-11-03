namespace SistemaVentas.BLL.Interfaces
{
    public interface ICorreoService
    {
        Task<bool> EnviarCorre(string CorreoDestino,string Asunto,string Mensaje);
    }
}
