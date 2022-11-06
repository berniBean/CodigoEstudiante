using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaVenta.Entity.Models;

namespace SistemaVenta.DAL.Configuraciones
{
    public class MenuConfig : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.HasKey(e => e.MenuId)
                .HasName("PK__Menu__C26AF48353DB2DAF");
            builder.ToTable("Menu");
            builder.Property(e => e.Controlador)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("controlador");

            builder.Property(e => e.Descripcion)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("descripcion");

            builder.Property(e => e.EsActivo).HasColumnName("esActivo");

            builder.Property(e => e.FechaRegistro)
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Icono)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("icono");

            builder.Property(e => e.IdMenuPadre).HasColumnName("idMenuPadre");

            builder.Property(e => e.PaginaAccion)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("paginaAccion");

            builder.HasOne(d => d.IdMenuPadreNavigation)
                .WithMany(p => p.InverseIdMenuPadreNavigation)
                .HasForeignKey(d => d.IdMenuPadre)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Menu__idMenuPadr__36B12243");

        }
    }
}
