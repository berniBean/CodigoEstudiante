namespace MVCVentas.Models.ViewModel
{
    public class VMUsuraio
    {
        
        public Guid UsuarioId { get; set; }
        public Guid RolId { get; set; }
        public string? NombreRol { get; set; }   
        public string? Nombre { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public string? UrlFoto { get; set; }
        public int? EsActivo { get; set; }

    }
}
