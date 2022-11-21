using AutoMapper;
using MVCVentas.Models.ViewModel;
using SistemaVenta.Entity.Models;
using System.Globalization;

namespace MVCVentas.Utilidades.Automapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Rol
            CreateMap<Rol, VMRol>().ReverseMap();
            #endregion

            #region Usuario
            CreateMap<Usuario, VMUsuraio>()
                .ForMember(destino => destino.EsActivo,
                opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0))
                .ForMember(destino => destino.NombreRol,
                opt => opt.MapFrom(origen => origen.RolNav.Descripcion));

            CreateMap<VMUsuraio, Usuario>()
                .ForMember(destino => destino.UsuarioId,
                opt => opt.MapFrom(origen => origen.UsuarioId.Equals("") ? Guid.Empty : new Guid(origen.UsuarioId)))
                .ForMember(dest => dest.EsActivo,
                opt => opt.MapFrom(o => o.EsActivo == 1 ? true : false))
                .ForMember(dest => dest.RolNav,
                opt => opt.Ignore());

            #endregion
            
            #region Venta
            CreateMap<Venta, VMVenta>()
                .ForMember(d => d.TipoDocumentoVenta,
                opt => opt.MapFrom(o => o.TipoDocumentosVentasNav.Descripcion))
                .ForMember(d => d.Usuario,
                opt => opt.MapFrom(o => o.UsuarioNav.Nombre))
               .ForMember(d => d.SubTotal,
                opt => opt.MapFrom(o => Convert.ToString(o.SubTotal, new CultureInfo("es-MX"))))
               .ForMember(d => d.ImpuestoTotal,
                opt => opt.MapFrom(o => Convert.ToString(o.ImpuestoTotal, new CultureInfo("es-MX"))))
               .ForMember(d => d.Total,
                opt => opt.MapFrom(o => Convert.ToString(o.Total, new CultureInfo("es-MX"))))
               .ForMember(d => d.FechaRegistro,
                opt => opt.MapFrom(o => o.FechaRegistro.ToString("dd/MMMM/yyyy")));

            CreateMap<VMVenta, Venta>()
               .ForMember(destino => destino.VentaId,
                opt=>opt.MapFrom(origen=>origen.VentaId.Equals("") ? Guid.Empty : new Guid(origen.VentaId)))
               .ForMember(destino => destino.UsuarioId,
                opt => opt.MapFrom(origen => origen.UsuarioId.Equals("") ? Guid.Empty : new Guid(origen.UsuarioId)))
               .ForMember(destino => destino.IdTipoDocumentoVenta,
                opt => opt.MapFrom(origen => origen.IdTipoDocumentoVenta.Equals("") ? Guid.Empty : new Guid(origen.IdTipoDocumentoVenta)))
               .ForMember(d => d.SubTotal,
                opt => opt.MapFrom(o => Convert.ToDecimal(o.SubTotal, new CultureInfo("es-MX"))))
               .ForMember(d => d.ImpuestoTotal,
                opt => opt.MapFrom(o => Convert.ToDecimal(o.ImpuestoTotal, new CultureInfo("es-MX"))))
               .ForMember(d => d.Total,
                opt => opt.MapFrom(o => Convert.ToDecimal(o.Total, new CultureInfo("es-MX"))));



            #endregion
            
            #region Negocio
            CreateMap<Negocio, VMNegocio>()
                .ForMember(d => d.PorcentajeImpuestos,
                opt => opt.MapFrom(o => Convert.ToString(o.PorcentajeImpuestos, new CultureInfo("es-MX"))));

            CreateMap<VMNegocio, Negocio>()
                .ForMember(d => d.PorcentajeImpuestos,
                opt => opt.MapFrom(o => Convert.ToDecimal(o.PorcentajeImpuestos, new CultureInfo("es-MX"))));
            #endregion

            #region Categoria
            CreateMap<Categoria, VMCategoria>()
                .ForMember(d => d.EsActivo,
                opt => opt.MapFrom(o => o.EsActivo == true ? 1 : 0));

            CreateMap<VMCategoria, Categoria>()
                .ForMember(d => d.EsActivo,
                opt => opt.MapFrom(o => o.EsActivo == 1 ? true : false))
                .ForMember(destino => destino.CategoriaId,
                opt => opt.MapFrom(origen => origen.CategoriaId.Equals("") ? Guid.Empty : new Guid(origen.CategoriaId)));
            #endregion

            #region Producto
            CreateMap<Producto, VMProducto>()
                .ForMember(d => d.EsActivo,
                opt => opt.MapFrom(o => o.EsActivo == true ? 1 : 0))
                .ForMember(d => d.NombreCategoria,
                opt => opt.MapFrom(o => o.CategoriaNav.Descripcion))
               .ForMember(d => d.Precio,
                opt => opt.MapFrom(o => Convert.ToString(o.Precio, new CultureInfo("es-MX"))));

            CreateMap<VMProducto, Producto>()
                .ForMember(destino => destino.ProductoId,
                opt=> opt.MapFrom(origen => origen.ProductoId.Equals("") ? Guid.Empty : new Guid(origen.ProductoId)))
                .ForMember(destino => destino.CategoriaId,
                opt=>opt.MapFrom(origen=>origen.CategoriaId.Equals("") ? Guid.Empty : new Guid(origen.CategoriaId)))
                .ForMember(d => d.EsActivo,
                opt => opt.MapFrom(o => o.EsActivo == 1 ? true : false))
                .ForMember(d => d.CategoriaNav,
                opt => opt.Ignore())
               .ForMember(d => d.Precio,
                opt => opt.MapFrom(o => Convert.ToDecimal(o.Precio, new CultureInfo("es-MX"))));

            #endregion

            #region TipoDocumentoVenta
            CreateMap<TipoDocumentoVenta, VMTipoDocumentoDeVenta>().ReverseMap();

            #endregion

            #region DetalleDeVenta
            CreateMap<DetalleVenta, VMDetalleVenta>()
                .ForMember(d => d.Precio,
                opt => opt.MapFrom(o => Convert.ToString(o.Precio, new CultureInfo("es-MX"))))
                .ForMember(d => d.Total,
                opt => opt.MapFrom(o => Convert.ToString(o.Total, new CultureInfo("es-MX"))));

            CreateMap< VMDetalleVenta, DetalleVenta>()
                .ForMember(d => d.Precio,
                opt => opt.MapFrom(o => Convert.ToDecimal(o.Precio, new CultureInfo("es-MX"))))
                .ForMember(d => d.Total,
                opt => opt.MapFrom(o => Convert.ToDecimal(o.Total, new CultureInfo("es-MX"))));

            CreateMap<DetalleVenta, VMReporteVenta>()
                .ForMember(d => d.FechaRegistro,
                opt => opt.MapFrom(o => o.Ventas.FechaRegistro.ToString("dd/MM/yyyy")))
                .ForMember(d => d.NumeroVenta,
                opt => opt.MapFrom(o => o.Ventas.NumeroVenta))
                .ForMember(d => d.TipoDocumento,
                opt => opt.MapFrom(o => o.Ventas.TipoDocumentosVentasNav.Descripcion))
                .ForMember(d => d.DocumentoCliente,
                opt => opt.MapFrom(o => o.Ventas.DocumentoCliente))
                .ForMember(d => d.NombreCliente,
                opt => opt.MapFrom(o => o.Ventas.NombreCliente))
                .ForMember(d => d.SobTotalVenta,
                opt => opt.MapFrom(o => Convert.ToString(o.Ventas.SubTotal, new CultureInfo("es-MX"))))
                .ForMember(d => d.ImpuestoTotalVenta,
                opt => opt.MapFrom(o => Convert.ToString(o.Ventas.ImpuestoTotal, new CultureInfo("es-MX"))))
                .ForMember(d => d.TotalVenta,
                opt => opt.MapFrom(o => Convert.ToString(o.Ventas.Total, new CultureInfo("es-MX"))))
                .ForMember(d => d.Producto,
                opt => opt.MapFrom(o => o.DescripcionProducto))
                .ForMember(d => d.Precio,
                opt => opt.MapFrom(o => Convert.ToString(o.Precio, new CultureInfo("es-MX"))))
                .ForMember(d => d.Total,
                opt => opt.MapFrom(o => Convert.ToString(o.Total, new CultureInfo("es-MX"))));








            #endregion

            #region Menu
            CreateMap<Menu, VMMenu>()
                .ForMember(d => d.SubMenus,
                opt => opt.MapFrom(o => Convert.ToString(o.InverseIdMenuPadreNavigation)));
            #endregion

        }
    }
}
