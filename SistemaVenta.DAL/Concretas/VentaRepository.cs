using Microsoft.EntityFrameworkCore;
using SistemaVenta.DAL.DBContext;
using SistemaVenta.DAL.Interfaces;
using SistemaVenta.Entity.Models;
using System.Linq.Expressions;

namespace SistemaVenta.DAL.Concretas
{
    public class VentaRepository : GenericRepository<Venta>, IVentaRepository
    {
        private readonly VentasDBContext _ventasDBContext;

        public VentaRepository(VentasDBContext ventasDBContext) : base(ventasDBContext)
        {
            _ventasDBContext = ventasDBContext;
        }



        public async Task<Venta> Registrar(Venta entidad)
        {
            Venta generada = new Venta();


            using(var trans = _ventasDBContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (DetalleVenta item in entidad.DetalleVenta)
                    {
                        Producto encontrado = _ventasDBContext.Productos.Where(p => p.ProductoId == item.ProductoId).FirstOrDefault();
                        encontrado.Stock = encontrado.Stock - item.Cantidad;
                        _ventasDBContext.Update(encontrado);
                    }
                    await _ventasDBContext.SaveChangesAsync();

                    NumeroCorrelativo correlativo = _ventasDBContext.numerosCorrelativos.Where(n => n.Gestion == "venta").First();

                    correlativo.UltimoNumero = correlativo.UltimoNumero + 1;
                    correlativo.FechaActualizacion = DateTime.Now;

                    _ventasDBContext.numerosCorrelativos.Update(correlativo);
                    await _ventasDBContext.SaveChangesAsync();

                    string ceros = string.Concat(Enumerable.Repeat("0", int.Parse( correlativo.CantidadDigitos.ToString())));
                    string numeroVenta = ceros + correlativo.UltimoNumero.ToString();

                    numeroVenta = numeroVenta.Substring(numeroVenta.Length - int.Parse(correlativo.CantidadDigitos.ToString()),int.Parse(correlativo.CantidadDigitos.ToString()));
                    entidad.NumeroVenta = numeroVenta;

                    await _ventasDBContext.SaveChangesAsync();

                    generada = entidad;

                    trans.Commit();
                }
                catch (Exception)
                {

                    trans.Rollback();
                    throw;
                }
            }
            return generada;

        }

        public async Task<List<DetalleVenta>> Reporte(DateTime FechaInicio, DateTime FechaFin)
        {
            List<DetalleVenta>listaResumen = await _ventasDBContext.DetalleVentas
                .Include(v=> v.Ventas)
                .ThenInclude(u=> u.UsuarioNav)
                .Include(v=> v.Ventas)
                .ThenInclude(td => td.TipoDocumentosVentasNav)
                .Where(dv => dv.Ventas.FechaRegistro.Date >= FechaInicio.Date && dv.Ventas.FechaRegistro.Date<= FechaFin.Date)
                .ToListAsync();

            return listaResumen;
        }
    }
}
