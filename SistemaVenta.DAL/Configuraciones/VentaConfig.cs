using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaVenta.Entity.Models;

namespace SistemaVenta.Entity.Configuraciones
{
    public class VentaConfig : IEntityTypeConfiguration<Venta>
    {
        public void Configure(EntityTypeBuilder<Venta> builder)
        {
            builder.Property(prop => prop.NumeroVenta).HasMaxLength(6);
            builder.Property(prop => prop.DocumentoCliente).HasMaxLength(10);
            builder.Property(prop => prop.NombreCliente).HasMaxLength(20);
        }
    }
}
