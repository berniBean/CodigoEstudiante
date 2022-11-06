using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaVenta.Entity.Models;

namespace SistemaVenta.Entity.Configuraciones
{
    public class ProductoConfig : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            builder.Property(prop => prop.UrlImagen).HasMaxLength(500)
                .IsUnicode(false);
            builder.Property(prop=>prop.FechaRegistro).HasDefaultValueSql("(getdate())");

        }
    }
}
