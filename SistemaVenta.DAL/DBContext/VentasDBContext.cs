using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SistemaVenta.Entity.Models;
using System.Reflection;

namespace SistemaVenta.DAL.DBContext
{
    public class VentasDBContext : DbContext
    {
        public VentasDBContext()
        {

        }

        public VentasDBContext(DbContextOptions<VentasDBContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<Usuario>().Property(u=>u.FechaRegistro).HasDefaultValueSql("(getdate())");
            modelBuilder.Entity<Usuario>().Property(u => u.UrlFoto).IsUnicode(false);
            modelBuilder.Entity<Usuario>().Property(u => u.UrlFoto).HasMaxLength(1500);
            modelBuilder.Entity<Rol>().Property(u=>u.FechaRegistro).HasDefaultValueSql("(getdate())");
            modelBuilder.Entity<TipoDocumentoVenta>().Property(u=>u.FechaRegistro).HasDefaultValueSql("(getdate())");
            modelBuilder.Entity<Negocio>().Property(u => u.UrlLogo).HasMaxLength(1500);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            //if (!optionsBuilder.IsConfigured)
            //{
            //    optionsBuilder.UseSqlServer("server=EFLORESPLAP\\SQLEXPRESS;Database=VentasDB; Trusted_Connection=True");
            //}
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
            configurationBuilder.Properties<string>().HaveMaxLength(100);
            
            configurationBuilder.Properties<decimal>().HavePrecision(10, 2);
            configurationBuilder.Properties<DateTime>().HaveColumnType("DateTime");
            




        }

        public DbSet<Categoria>Categorias { get; set; }
        public DbSet<Configuracion>Configuraciones { get; set; }
        public DbSet<DetalleVenta>DetalleVentas { get; set; }
        public DbSet<Menu>Menus { get; set; }
        public DbSet<Negocio>Negocios { get; set; }
        public DbSet<NumeroCorrelativo> numerosCorrelativos { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<RolMenu> RolesMenus { get; set; }
        public DbSet<TipoDocumentoVenta> TiposDocumentosVentas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Venta> Ventas { get; set; }

    }
}
