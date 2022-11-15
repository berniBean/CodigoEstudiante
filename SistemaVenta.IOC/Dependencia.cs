using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using SistemaVenta.DAL.DBContext;
using Microsoft.EntityFrameworkCore;
using SistemaVenta.DAL.Interfaces;
using SistemaVenta.DAL.Concretas;
using SistemaVentas.BLL.Interfaces;
using SistemaVentas.BLL.Implementacion;
using SistemaVenta.BLL.Interfaces;
using SistemaVenta.BLL.Implementacion;

namespace SistemaVenta.IOC
{
    public static class Dependencia
    {
        public static void InyectarDependencia(this IServiceCollection Services, IConfiguration Configuration)
        {
            Services.AddDbContext<VentasDBContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            Services.AddScoped<IVentaRepository, VentaRepository>();
            Services.AddScoped<ICorreoService, CorreoService>();
            Services.AddScoped<IFireBaseServices, FirebaseService>();
            Services.AddScoped<IUtilidadesServices, UtilidadesServices>();
            Services.AddScoped<IRolService, RolService>();
            Services.AddScoped<IUsuarioService, UsuarioService>();
            Services.AddScoped<INegocioService, NegocioService>();
            Services.AddScoped<ICategoriaService, CategoriaService>();
            Services.AddScoped<IProductoService,ProductoService>();
            Services.AddScoped<ITipoDocumentoService, TipoDocumentoventaService>();
            Services.AddScoped<IVentaService, VentaService>();
        }
    }
}
