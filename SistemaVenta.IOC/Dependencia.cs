using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using SistemaVenta.DAL.DBContext;
using Microsoft.EntityFrameworkCore;

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
        }
    }
}
