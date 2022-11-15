console.log("hola mundo")

const MODELO_BASE = {
    productoId: "",
    CodigoDeBarra: "",    
    Marca: "",
    Nombre: "",
    CategoriaId: "",
    Stock: 0,
    urlImagen: "",
    Precio: 0,
    esActivo: 1
}

let tablaData;

$(document).ready(function () {

    fetch("/Categoria/Lista")
        
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        }).then(responseJson => {
          
            if (responseJson.data.length > 0) {
                responseJson.data.forEach((item) => {
                    $("#cboCategoria").append(
                        $("<option>").val(item.categoriaId).text(item.descripcion)
                    )
                })
            }
        })

    tablaData = $('#tbdata').DataTable({
        responsive: true,
        "ajax": {
            "url": '/Producto/lista',
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "productoId", "visible": false, "searchale": false },
            {
                "data": "urlImagen", render: function (data) {
                    return `<img style="height:60px" src=${data} class="rounded mx-auto d-block"/>`
                }
            },
            { "data": "codigoDeBarra" },
            { "data": "marca" },
            { "data": "descripcion" },
            { "data": "nombreCategoria" },
            { "data": "stock" },
            { "data": "precio" },
            {
                "data": "esActivo", render: function (data) {
                    if (data == 1)
                        return '<span class="badge badge-info">Activo</span>';
                    else
                        return '<span class="badge badge-danger">Inactivo</span>';
                }
            },
            {
                "defaultContent": '<button class="btn btn-primary btn-editar btn-sm mr-2"><i class="fas fa-pencil-alt"></i></button>' +
                    '<button class="btn btn-danger btn-eliminar btn-sm"><i class="fas fa-trash-alt"></i></button>',
                "orderable": false,
                "searchable": false,
                "width": "80px"
            }
        ],
        order: [[0, "desc"]],
        dom: "Bfrtip",
        buttons: [
            {
                text: 'Exportar Excel',
                extend: 'excelHtml5',
                title: '',
                filename: 'Reporte Producto',
                exportOptions: {
                    columns: [2, 3, 4, 5, 6]
                }
            }, 'pageLength'
        ],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        },
    });
})


function mostrarModal(modelo = MODELO_BASE) {
    console.log(modelo)
    $("#txtId").val(modelo.productoId)

    $("#txtCodigoBarra").val(modelo.codigoDeBarra)
    $("#txtMarca").val(modelo.marca)
    $("#txtDescripcion").val(modelo.descripcion)
    $("#cboCategoria").val(modelo.categoriaId == "" ? $("#cboRol option:first").val() : modelo.CategoriaId)
    $("#txtStock").val(modelo.stock)
    $("#txtPrecio").val(modelo.precio)
    $("#cboEstado").val(modelo.esActivo)
    $("#txtImagen").val("")
    $("#imgProducto").attr("src", modelo.urlImagen)

    $("#modalData").modal("show")
}


$("#btnNuevo").click(function () {
    mostrarModal()
})


$("#btnGuardar").click(function () {

    const inputs = $("input.input-validar").serializeArray();
    const inputs_sin_valor = inputs.filter((item) => item.value.trim() == "")

    if (inputs_sin_valor.length > 0) {
        const mensaje = `Debe completar el campo: "${inputs_sin_valor[0].name}"`;
        toastr.warning("", mensaje)
        $(`input[name="${inputs_sin_valor[0].name}"]`).focus()
        return;
    }


    const modelo = structuredClone(MODELO_BASE);
    modelo["productoId"] = $("#txtId").val()
    modelo["CodigoDeBarra"] = $("#txtCodigoBarra").val()
    modelo["descripcion"] = $("#txtDescripcion").val()
    modelo["marca"] = $("#txtMarca").val()
    modelo["CategoriaId"] = $("#cboCategoria").val()
    modelo["Stock"] = $("#txtStock").val()
    modelo["Precio"] = $("#txtPrecio").val()
    modelo["EsActivo"] = $("#cboEstado").val()

    const inputFoto = document.getElementById("txtImagen")

    const formData = new FormData();
    formData.append("foto", inputFoto.files[0])
    formData.append("modelo", JSON.stringify(modelo))

    $("#modalData").find("div.modal-content").LoadingOverlay("show");
    console.log(modelo.productoId)
    if (modelo.productoId == "") {

        fetch("/Producto/Crear", {
            method: "POST",
            body: formData
        }).then(response => {
            $("#modalData").find("div.modal-content").LoadingOverlay("hide");
            return response.ok ? response.json() : Promise.reject(response);
        }).then(responseJson => {
            if (responseJson.estado) {
                tablaData.row.add(responseJson.objeto).draw(false)
                $("#modalData").modal("hide")
                swal("¡Listo!", "El producto fue creado", "success")
            } else {
                swal("¡Error!", responseJson.mensaje, "error")
            }
        })
    } else {
        fetch("/Producto/Editar", {
            method: "PUT",
            body: formData
        }).then(response => {
            $("#modalData").find("div.modal-content").LoadingOverlay("hide");
            return response.ok ? response.json() : Promise.reject(response);
        }).then(responseJson => {
            if (responseJson.estado) {
                tablaData.row(filaSeleccionada).data(responseJson.objeto).draw(false);
                filaSeleccionada = null
                $("#modalData").modal("hide")
                swal("¡Listo!", "El Producto fue modificado", "success")
            } else {
                swal("¡Error!", responseJson.mensaje, "error")
            }
        })
    }
})

let filaSeleccionada;
$("#tbdata tbody").on("click", ".btn-editar", function () {
    if ($(this).closest("tr").hasClass("child")) {
        filaSeleccionada = $(this).closest("tr").prev();

    } else {
        filaSeleccionada = $(this).closest("tr")
    }


    const data = tablaData.row(filaSeleccionada).data()
    mostrarModal(data);
})

$("#tbdata tbody").on("click", ".btn-eliminar", function () {
    let fila;
    if ($(this).closest("tr").hasClass("child")) {
        fila = $(this).closest("tr").prev();

    } else {
        fila = $(this).closest("tr")
    }


    const data = tablaData.row(fila).data();
    console.log(data.nombre);
    swal({
        title: "¿Está seguro?",
        text: `Eliminar el producto: "${data.descripcion}"`,
        type: "warning",

        showCancelButton: true,
        confirmButtonClass: "btn-danger",
        confirmButtonText: "Sí, eliminar",
        cancelButtonText: "No, cancelar",
        closeOnConfirm: false,
        closeOnCancel: true
    },
        
        function (respuesta) {
            if (respuesta) {
                $(".showSweetAlert").LoadingOverlay("show");

                fetch(`/Producto/Delete?IdProducto=${data.productoId}`, {
                    method: "DELETE"
                }).then(response => {
                    $(".showSweetAlert").LoadingOverlay("hide");
                    return response.ok ? response.json() : Promise.reject(response);
                }).then(responseJson => {
                    if (responseJson.estado) {
                        tablaData.row(filaSeleccionada).remove().draw()
                        swal("¡Listo!", "El producto fue eleminado", "success")
                    } else {
                        swal("¡Error!", responseJson.mensaje, "success")
                    }


                })
            }
        })

})