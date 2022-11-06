namespace MVCVentas.Models.ViewModel
{
    public class VMDashBoard
    {
        public int TotalVentas { get; set; }
        public string? TotalIngresos { get; set; }
        public int? TotalProductos { get; set; }
        public int? TotalCategorias { get; set; }
        public List<VMVentasSemana>? VemntasUltimaSemana { get; set; }
        public List<VMProductosSemana>? ProductosTopUltimaSemana { get; set; }
    }
}
