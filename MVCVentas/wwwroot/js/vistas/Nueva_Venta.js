﻿let valorImpuesto = 0;
$(document).ready(function () {
    fetch("/Venta/ListaTipoDocumentoVenta")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        }).then(responseJson => {
            
            if (responseJson.length > 0) {
                responseJson.forEach((item) => {
                    $("#cboTipoDocumentoVenta").append(
                        $("<option>").val(item.tipoDocumentoVentaId).text(item.descripcion)
                    )
                })
            }
        })

    fetch("/Negocio/Obtener")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        }).then(responseJson => { 
           
            if (responseJson.estado) {
                const d = responseJson.objeto;
                

                $("#inputGroupSubTotal").text(`Sub total - ${d.simboloMoneda}`)
                $("#inputGroupIGV").text(`IGV(${d.porcentajeImpuestos}) - ${d.simboloMoneda}`)
                $("#inputGroupTotal").text(`Total - ${d.simboloMoneda}`)

                valorImpuesto = parseFloat(d.porcentajeImpuestos);
            }
        })

    $("#cboBuscarProducto").select2({
        ajax: {
            url:"/Venta/ObtenerProductos",
            dataType: 'json',
            contentType: "application/json;charset=utf-8",
            delay: 250,
            data: function (params) {
                return {
                    busqueda: params.term
                };
            },
            processResults: function (data) {
                return {
                    
                    results: data.map((item) => (
                        {
                            id: item.productoId,
                            text: item.descripcion,

                            marca: item.marca,
                            categoria: item.nombreCategoria,
                            urlImagen: item.urlImagen,
                            precio: parseFloat(item.precio)
                        }
                    ))
                };
            },
        },
        language:"es",
        minimumInputLength: 1,
        templateResult: formatoResultados

    });


})

function formatoResultados(data) {
    if (data.loading) {
        return data.text;
    }
        

    var contenedor = $(
        `<table width="100%">
        <tr>
            <td style="width:60px">
                <img style="height:60px;width:60px;margin-right:10px" src="${data.urlImagen}"/>
            </td>
            <td>
                <p style="font-weight:bolder;margin:2px">${data.marca}</p>
                <p style="margin:2px">${data.text}</p>
            </td>
        </tr>
    </table>`
    );

    return contenedor;

}

$(document).on("select2:open", function () {
    document.querySelector(".select2-search__field").focus();
})

let ProductosParaVenta = [];
$("#cboBuscarProducto").on("select2:select", function (e) {
    const data = e.params.data

    console.log(data)
    let producto_encontrado = ProductosParaVenta.filter(p => p.productoId == data.id)
    if (producto_encontrado.length > 0) {
        $("#cboBuscarProducto").val("").trigger("change")
        toastr.warning("", "El producto ya fue agregado")
        return false
    }

    swal({
        title: data.marca,
        text: data.text,
        imageUrl: data.imageUrl,
        type:"input",
        showCancelButton: true,
        closeOnConfirm: false,
        inputPlaceholder:"Ingrese Cantidad"
    },
        function (valor) {
            if (valor == false) return false;

            if (valor === "") {
                toastr.warning("", "Ingrese la cantidad")
                return false;
            }
            if (isNaN(parseInt(valor))) {
                toastr.warning("", "Debe ingresar un valor númerico")
                return false;
            }
            let producto = {
                productoId: data.id,
                marcaProducto: data.marca,
                descripcionProducto: data.text,
                categoriaProducto: data.categoria,
                cantidad: parseInt(valor),
                precio: data.precio.toString(),
                total:(parseFloat(valor)* data.precio).toString()
            }

            ProductosParaVenta.push(producto)
            mostrarProducto_Precios()
            $("#cboBuscarProducto").val("").trigger("change")
            swal.close()
        })

})

function mostrarProducto_Precios() {
    let total = 0;
    let igv = 0;
    let subtotal = 0;
    let porcentaje = valorImpuesto / 100;

    $("#tbProducto tbody").html("")
    
    ProductosParaVenta.forEach((item) => {
        
        total = total + parseFloat(item.total);
        console.log(item.descripcionProducto)
        console.log(item.productoId)
        $("#tbProducto tbody").append(
            $("<tr>").append(
                $("<td>").append(
                    $("<button>").addClass("btn btn-danger btn-eliminar btn-sm").append(
                        $("<i>").addClass("fas fa-trash-alt")
                    ).data("idProducto", item.productoId)
                ),
                $("<td>").text(item.descripcionProducto),
                $("<td>").text(item.cantidad),
                $("<td>").text(item.precio),
                $("<td>").text(item.total)
            )

        )
    })

    subtotal = total / (1 + porcentaje);
    igv = total - subtotal;

    $("#txtSubTotal").val(subtotal.toFixed(2))
    $("#txtIGV").val(igv.toFixed(2))
    $("#txtTotal").val(total.toFixed(2))


}

$(document).on("click","button.btn-eliminar", function () {

    const _idProducto = $(this).data("idProducto")

    ProductosParaVenta = ProductosParaVenta.filter(p => p.productoId != _idProducto);
   
    mostrarProducto_Precios();
})

$("#btnTerminarVenta").click(function () {
    if (ProductosParaVenta.length < 1) {
        toastr.warning("", "Debe ingresar productos")
        return;
    }

    const vmDetalleVenta = ProductosParaVenta;
    console.log(vmDetalleVenta)
    const venta = {
        idTipoDocumentoVenta: $("#cboTipoDocumentoVenta").val(),
        documentoCliente: $("#txtDocumentoCliente").val(),
        NombreCliente: $("#txtNombreCliente").val(),
        SubTotal: $("#txtSubTotal").val(),
        impuesto: $("#txtIGV").val(),
        total: $("#txtTotal").val(),
        detalleVenta: vmDetalleVenta
    }

    $("#btnTerminarVenta").LoadingOverlay("show");
    fetch("/Venta/RegistrarVenta", {
        method: "POST",
        headers: { "Content-Type": "application/json;charset=utf-8" },
        body: JSON.stringify(venta)
        })
        .then(response => {
            $("#btnTerminarVenta").LoadingOverlay("hide");
            return response.ok ? response.json() : Promise.reject(response);
        }).then(responseJson => {
            if (responseJson.estado) {
                ProductosParaVenta = [];
                mostrarProducto_Precios();

                $("#txtDocumentoCliente").val()
                $("#txtNombreCliente").val()
                $("#cboTipoDocumentoVenta").val($("#cboTipoDocumentoVenta option:first").val())

                swal("¡Listo!", `Numero venta: ${responseJson.objeto.numeroVenta}`, "success")

            } else {
                swal("¡Error!", "No se pudo registrar la venta", "error")
            }
        })

})