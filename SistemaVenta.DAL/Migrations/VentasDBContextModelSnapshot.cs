﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SistemaVenta.DAL.DBContext;

#nullable disable

namespace SistemaVenta.DAL.Migrations
{
    [DbContext(typeof(VentasDBContext))]
    partial class VentasDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SistemaVenta.Entity.Models.Categoria", b =>
                {
                    b.Property<Guid>("CategoriaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("EsActivo")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("datetime2");

                    b.HasKey("CategoriaId");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("SistemaVenta.Entity.Models.Configuracion", b =>
                {
                    b.Property<string>("Propiedad")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Recurso")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Valor")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.ToTable("Configuraciones");
                });

            modelBuilder.Entity("SistemaVenta.Entity.Models.DetalleVenta", b =>
                {
                    b.Property<Guid>("DetalleVentaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Cantidad")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("CategoriaProducto")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("DescripcionProducto")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("MarcaProducto")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Precio")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<Guid>("ProductoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Total")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<Guid>("VentaId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("DetalleVentaId");

                    b.HasIndex("VentaId");

                    b.ToTable("DetalleVentas");
                });

            modelBuilder.Entity("SistemaVenta.Entity.Models.Menu", b =>
                {
                    b.Property<Guid>("MenuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("EsActivo")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<string>("Icono")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("IdMenuPadre")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PaginaAccion")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("MenuId");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("SistemaVenta.Entity.Models.Negocio", b =>
                {
                    b.Property<Guid>("NegocioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("NombreDocumento")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("NombreLogo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("PorcentajeImpuestos")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("SimboloMoneda")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UrlNegocio")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("NegocioId");

                    b.ToTable("Negocios");
                });

            modelBuilder.Entity("SistemaVenta.Entity.Models.NumeroCorrelativo", b =>
                {
                    b.Property<Guid>("NumeroCorrelativoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CantidadDigitos")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaActualizacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Gestion")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("UltimoNumero")
                        .HasColumnType("int");

                    b.HasKey("NumeroCorrelativoId");

                    b.ToTable("numerosCorrelativos");
                });

            modelBuilder.Entity("SistemaVenta.Entity.Models.Producto", b =>
                {
                    b.Property<Guid>("ProductoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoriaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CodigoDeBarra")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("EsActivo")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<string>("Marca")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("NombreImagen")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Precio")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("UrlImagen")
                        .IsRequired()
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("varchar(500)");

                    b.HasKey("ProductoId");

                    b.HasIndex("CategoriaId");

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("SistemaVenta.Entity.Models.Rol", b =>
                {
                    b.Property<Guid>("RolId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("EsActivo")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("datetime2");

                    b.HasKey("RolId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("SistemaVenta.Entity.Models.RolMenu", b =>
                {
                    b.Property<Guid>("RolId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MenuId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("EsActivo")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("datetime2");

                    b.HasKey("RolId", "MenuId");

                    b.HasIndex("MenuId");

                    b.ToTable("RolesMenus");
                });

            modelBuilder.Entity("SistemaVenta.Entity.Models.TipoDocumentoVenta", b =>
                {
                    b.Property<Guid>("TipoDocumentoVentaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("EsActivo")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("datetime2");

                    b.HasKey("TipoDocumentoVentaId");

                    b.ToTable("TiposDocumentosVentas");
                });

            modelBuilder.Entity("SistemaVenta.Entity.Models.Usuario", b =>
                {
                    b.Property<Guid>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Clave")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("EsActivo")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("RolId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UrlFoto")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("UsuarioId");

                    b.HasIndex("RolId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("SistemaVenta.Entity.Models.Venta", b =>
                {
                    b.Property<Guid>("VentaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DocumentoCliente")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("IdTipoDocumentoVenta")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("ImpuestoTotal")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("NombreCliente")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("NumeroVenta")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.Property<decimal>("SubTotal")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<Guid>("TipoDocumentosVentasNavTipoDocumentoVentaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Total")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("VentaId");

                    b.HasIndex("TipoDocumentosVentasNavTipoDocumentoVentaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Ventas");
                });

            modelBuilder.Entity("SistemaVenta.Entity.Models.DetalleVenta", b =>
                {
                    b.HasOne("SistemaVenta.Entity.Models.Venta", "Ventas")
                        .WithMany()
                        .HasForeignKey("VentaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ventas");
                });

            modelBuilder.Entity("SistemaVenta.Entity.Models.Producto", b =>
                {
                    b.HasOne("SistemaVenta.Entity.Models.Categoria", "CategoriaNav")
                        .WithMany("Productos")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CategoriaNav");
                });

            modelBuilder.Entity("SistemaVenta.Entity.Models.RolMenu", b =>
                {
                    b.HasOne("SistemaVenta.Entity.Models.Menu", "Menu")
                        .WithMany("RolMenus")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SistemaVenta.Entity.Models.Rol", "Rol")
                        .WithMany("RolMenus")
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Menu");

                    b.Navigation("Rol");
                });

            modelBuilder.Entity("SistemaVenta.Entity.Models.Usuario", b =>
                {
                    b.HasOne("SistemaVenta.Entity.Models.Rol", "UsuarioNav")
                        .WithMany("Usuarios")
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UsuarioNav");
                });

            modelBuilder.Entity("SistemaVenta.Entity.Models.Venta", b =>
                {
                    b.HasOne("SistemaVenta.Entity.Models.TipoDocumentoVenta", "TipoDocumentosVentasNav")
                        .WithMany("Ventas")
                        .HasForeignKey("TipoDocumentosVentasNavTipoDocumentoVentaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SistemaVenta.Entity.Models.Usuario", "UsuarioNav")
                        .WithMany("Ventas")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TipoDocumentosVentasNav");

                    b.Navigation("UsuarioNav");
                });

            modelBuilder.Entity("SistemaVenta.Entity.Models.Categoria", b =>
                {
                    b.Navigation("Productos");
                });

            modelBuilder.Entity("SistemaVenta.Entity.Models.Menu", b =>
                {
                    b.Navigation("RolMenus");
                });

            modelBuilder.Entity("SistemaVenta.Entity.Models.Rol", b =>
                {
                    b.Navigation("RolMenus");

                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("SistemaVenta.Entity.Models.TipoDocumentoVenta", b =>
                {
                    b.Navigation("Ventas");
                });

            modelBuilder.Entity("SistemaVenta.Entity.Models.Usuario", b =>
                {
                    b.Navigation("Ventas");
                });
#pragma warning restore 612, 618
        }
    }
}
