﻿$(document).ready(function () {

    $(".card-body").LoadingOverlay("show");
    
    fetch("/Negocio/Obtener")
        .then(response => {
            $(".card-body").LoadingOverlay("hide");
            return response.ok ? response.json() : Promise.reject(response);
            
        }).then(responseJson => {

                if (responseJson.estado) {
                    const d = responseJson.objeto
                    console.log(d.urlLogo)
                    $("#txtNumeroDocumento").val(d.numeroDocumento)
                    $("#txtRazonSocial").val(d.nombre)
                    $("#txtCorreo").val(d.correo)
                    $("#txtDireccion").val(d.direccion)
                    $("#txTelefono").val(d.telefono)
                    $("#txtImpuesto").val(d.porcentajeImpuestos)
                    $("#txtSimboloMoneda").val(d.simboloMoneda)
                    $("#imgLogo").attr("src", d.urlLogo)
                } else {
                    swal("¡Error!", responseJson.mensaje, "error")
                }
            
        })
})

$("#btnGuardarCambios").click(function () {
    const inputs = $("input.input-validar").serializeArray();
    const inputs_sin_valor = inputs.filter((item) => item.value.trim() == "")

    if (inputs_sin_valor.length > 0) {
        const mensaje = `Debe completar el campo: "${inputs_sin_valor[0].name}"`;
        toastr.warning("", mensaje)
        $(`input[name="${inputs_sin_valor[0].name}"]`).focus()
        return;
    }

    const modelo = {
        numeroDocumento: $("#txtNumeroDocumento").val(),
        nombre: $("#txtRazonSocial").val(),
        correo: $("#txtCorreo").val(),
        direccion: $("#txtDireccion").val(),
        telefono: $("#txTelefono").val(),
        porcentajeImpuesto: $("#txtImpuesto").val(),
        simboloMoneda: $("#txtSimboloMoneda").val()
    }

    const inputLog = document.getElementById("txtLogo")
    const formData = new FormData()

    formData.append("logo", inputLog.files[0])
    formData.append("modelo", JSON.stringify(modelo))

    $(".card-body").LoadingOverlay("show");

    fetch("/Negocio/Guardar", {
        method: "POST",
        body: formData
    })
        .then(response => {
            $(".card-body").LoadingOverlay("hide");
            return response.ok ? response.json() : Promise.reject(response);
        }).then(responseJson => {
           
                if (responseJson.estado) {
                    const d = responseJson.objeto

                    $("#imgLogo").attr("src", d.urlLogo)

                } else {
                    swal("¡Error!", responseJson.mensaje, "error")
                }
            
        })

})