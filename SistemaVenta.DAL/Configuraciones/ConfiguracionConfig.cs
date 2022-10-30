using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaVenta.Entity.Models;

namespace SistemaVenta.DAL.Configuraciones
{
    internal class ConfiguracionConfig : IEntityTypeConfiguration<Configuracion>
    {
        public void Configure(EntityTypeBuilder<Configuracion> builder)
        {
            builder.HasNoKey();
        }
    }
}
