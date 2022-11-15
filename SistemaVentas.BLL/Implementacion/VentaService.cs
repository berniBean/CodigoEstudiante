using Microsoft.EntityFrameworkCore;
using SistemaVenta.BLL.Interfaces;
using SistemaVenta.DAL.Interfaces;
using SistemaVenta.Entity.Models;
using System.Globalization;

namespace SistemaVenta.BLL.Implementacion
{
    public class VentaService : IVentaService
    {
        private readonly IGenericRepository<Producto> _productoRepo;
        private readonly IVentaRepository _ventaRepo;

        public VentaService(IGenericRepository<Producto> productoRepo, IVentaRepository ventaRepo)
        {
            _productoRepo = productoRepo;
            _ventaRepo = ventaRepo;
        }

        public async Task<List<Producto>> ObtenerProductos(string busqueda)
        {
            IQueryable<Producto> qyery = await _productoRepo
                .Consultar(p => p.EsActivo == true && p.Stock > 0 && string.Concat(p.CodigoDeBarra, p.Marca, p.Descripcion).Contains(busqueda));

            return await qyery.Include(c => c.CategoriaNav).ToListAsync();
        }
        public async Task<Venta> Registrar(Venta entidad)
        {
            try
            {
                return await _ventaRepo.Registrar(entidad);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<List<Venta>> Historial(string numeroVenta, string fechaInicio, string fechaFin)
        {
            IQueryable<Venta> query = await _ventaRepo.Consultar();
            fechaInicio = fechaInicio is null ? "" : fechaInicio;
            fechaFin = fechaFin is null ? "" : fechaFin;

            if(fechaInicio != "" && fechaFin != "")
            {
                DateTime inicioF = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-MX"));
                DateTime finF = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-MX"));

                return await query.Where(v => v.FechaRegistro.Date >= inicioF.Date &&
                    v.FechaRegistro.Date <= finF.Date)
                    .Include(tdv => tdv.TipoDocumentosVentasNav)
                    .Include(u => u.UsuarioNav)
                    .Include(v => v.DetalleVenta)
                    .ToListAsync();

            }
            else{
                return await query.Where(v=> v.DetalleVenta.Equals(numeroVenta))
                     .Include(tdv => tdv.TipoDocumentosVentasNav)
                     .Include(u => u.UsuarioNav)
                    .Include(v => v.DetalleVenta)
                    .ToListAsync();
            }

        }
        public async Task<Venta> Detalle(string numeroVenta)
        {
            IQueryable<Venta> query = await _ventaRepo.Consultar(v=> v.NumeroVenta.Equals(numeroVenta));

            return await query.Include(tdv => tdv.TipoDocumentosVentasNav)
                         .Include(u => u.UsuarioNav)
                        .Include(v => v.DetalleVenta)
                        .FirstAsync();
            
        }

        public async Task<List<DetalleVenta>> Reporte(string fechaInicio, string FechaFin)
        {
            DateTime inicioF = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-MX"));
            DateTime finF = DateTime.ParseExact(FechaFin, "dd/MM/yyyy", new CultureInfo("es-MX"));

            List<DetalleVenta> lista = await _ventaRepo.Reporte(inicioF, finF);
            return lista;
        }
    }
}
