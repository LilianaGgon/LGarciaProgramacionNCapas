$(document).ready(function () {
    GetAll();
})

function Formulario() {
    LimpiarForm()
    DDLRol();
    DDLEstado();
    DDLMunicipio()
    DDLColonia()
}

function GetAll() {
    $.ajax({
        url: urlGetAll,
        type: 'GET',
        dataType: 'JSON',
        success: function (result) {
            let tbodyCrudJS = $('#tbodyCrudJS')
            if (result.Correct) {
                $.each(result.Objects, function (i, valor) {
                    //console.log(valor)
                    var imagenValidada = "";
                    if (valor.ImagenBase64 != "") {
                        imagenValidada = `"data:image/*;base64, ${valor.ImagenBase64}"`
                    } else {
                        imagenValidada = "https://media.istockphoto.com/id/1473534533/es/vector/avatar-de-imagen-de-perfil-predeterminado-icono-de-avatar-de-usuario-icono-de-persona-icono.jpg?s=612x612&w=0&k=20&c=pxEpFr5yhE4zLcev7LFRFS2uHoe29fq_JKQ3RsRAmlU="
                    }

                    var usuario = `<tr>
                            <td>
                                <button type="button" class="btn btn-outline-warning" onclick="GetById(${valor.IdUsuario})">
                                    <i class="bi-pencil-fill bi-pencil-fill"></i>
                                </button>
                            </td>

                            <td>
                                <div class="form-check form-switch">
                                    <input class="form-check-input" type="checkbox" role="switch" id="flexSwitchCheckDefault">
                                </div>
                            </td>

                            <td>
                                <img src= ${imagenValidada} width="70" height="70" />
                            </td>

                            <td>${valor.Nombre} <br />${valor.ApellidoPaterno} <br />${valor.ApellidoMaterno}</td>

                            <td style="white-space: nowrap; word-wrap: break-word; max-width: 100%; overflow: visible;">CURP: ${valor.CURP} <br />Fecha de nacimiento: ${valor.FechaNacimiento} <br />Sexo: ${valor.Sexo}</td>

                            <td>Celular: ${valor.Celular} <br />Telefono: ${valor.Telefono}</td>

                            <td style="white-space: nowrap; word-wrap: break-word; max-width: 100%; overflow: visible;">Username: ${valor.UserName} <br />Email: ${valor.Email} <br />Password: ${valor.Password}</td>

                            <td>${valor.Rol.Nombre}</td>

                            <td style="white-space: nowrap; word-wrap: break-word; max-width: 100%; overflow: visible;">Calle: ${valor.Direccion.Calle} <br />No. Ext: ${valor.Direccion.NumeroExterior} <br />No. Int: ${valor.Direccion.NumeroInterior} <br />Colonia: ${valor.Direccion.Colonia.Nombre} <br />Municipio: ${valor.Direccion.Colonia.Municipio.Nombre} <br />Estado: ${valor.Direccion.Colonia.Municipio.Estado.Nombre}
                            </td>

                            <td>
                            <button type="button" class="btn btn-outline-danger" onclick="return confirm('¿Eliminar registro?'), Delete(${valor.IdUsuario})">
                                <i class="bi-pencil-fill bi-trash2-fill"></i>
                            </button>

                            </td>
                            
                        </tr>`;
                    tbodyCrudJS.append(usuario);
                })
            }
        },
        error:
            function (xhr) {
                console.log(xhr)
            }
    })
}

function mostrarModal() {
    $('#modalForm').modal('show')
}

function Guardar() {
    $.ajax({
        url: urlForm,
        type: 'POST',
        dataType: 'json',
        data: {
            IdUsuario: 0,
            Nombre: $("#inptNombre").val(),
            ApellidoPaterno: $("#inptApellidoPaterno").val(),
            ApellidoMaterno: $("#inptApellidoMaterno").val(),
            Celular: $("#inptCelular").val(),
            UserName: $("#inptUsername").val(),
            Email: $("#inptEmail").val(),
            Password: $("#inptPassword").val(),
            FechaNacimiento: $("#inptFechaNacimiento").val(),
            Sexo: $("#inptSexo").val(),
            Telefono: $("#inptTelefono").val(),
            Estatus: false,
            Curp: $("#inptCurp").val(),
            ImagenBase64: $("#inptImagen").val(),
            Rol: {
                IdRol: $("#ddlRol").val()
            },
            Direccion: {
                Calle: $("#inptCalle").val(),
                NumeroInterior: $("#inptNumeroInterior").val(),
                NumeroExterior: $("#inptNumeroExterior").val(),
                Colonia: {
                    IdColonia: $("#ddlColonia").val(),
                    Municipio: {
                        IdMunicipio: $("#ddlMunicipio").val(),
                        Estado: {
                            idEstado: $("#ddlEstado").val()
                        }
                    }
                }
            }
        },
        success: function (result) {
            if (result.Correct) {
                alert("Usuario agregado con éxito")
                location.reload();
            } else {
                alert("El usuario no se pudo agregar");
            }
        },
        error: function (xhr) {
            console.log(xhr);
        }
    });
}

function Delete(IdUsuario) {
    $.ajax({
        url: urlDelete,
        type: 'GET',
        dataType: 'JSON',
        data: {
            IdUsuario: IdUsuario,
        },
        success: function (result) {
            if (result.Correct) {
                alert("Registro eliminado con éxito")
                location.reload()
            } else {
                alert("No se pudo eliminar el registro")
            }
        },
        error:
            function (xhr) {
                console.log(xhr)
            }
    })
}


function LimpiarForm() {
    $('input').each(function () {
        let inpt = $(this)
        inpt.val("")
    })

    $('select').each(function () {
        let slct = $(this)
        slct.val("")
    })
}



//function GetById(IdUsuario) {
//    $.ajax({
//        url: urlGetById,
//        type: 'GET',
//        dataType: 'JSON',
//        data: { IdUsuario: IdUsuario },
//        success: function (result) {
//            if (result.Correct) {
//                var usuario = result.Object;
//                console.log(usuario)
//                mostrarModal()
//                LimpiarForm()

//                DDLRol(function () {
//                    let idDdlRol = (usuario.Rol.IdRol).toString();
//                    console.log("Intentando seleccionar rol con ID:", idDdlRol);
//                    $('#ddlRol').val(idDdlRol).trigger('change');
//                });

//                DDLEstado(function () {
//                    let idEstado = usuario.Direccion.Colonia.Municipio.Estado.IdEstado;
//                    console.log("Intentando seleccionar estado con ID:", idEstado);
//                    $('#ddlEstado').val(idEstado).trigger('change');
//                    DDLMunicipio(idEstado, function () {
//                        let idMunicipio = usuario.Direccion.Colonia.Municipio.IdMunicipio;
//                        console.log("Intentando seleccionar municipio con ID:", idMunicipio);
//                        $('#ddlMunicipio').val(idMunicipio).trigger('change');
//                        DDLColonia(idMunicipio, function () {
//                            let idColonia = usuario.Direccion.Colonia.IdColonia;
//                            console.log("Intentando seleccionar colonia con ID:", idColonia);
//                            $('#ddlColonia').val(idColonia).trigger('change');
//                        });
//                    });
//                });


//                $("#inptNombre").val(usuario.Nombre);
//                $("#inptApellidoPaterno").val(usuario.ApellidoPaterno);
//                $("#inptApellidoMaterno").val(usuario.ApellidoMaterno);
//                $("#inptCelular").val(usuario.Celular);
//                $("#inptUsername").val(usuario.UserName);
//                $("#inptEmail").val(usuario.Email);
//                $("#inptPassword").val(usuario.Password);
//                $("#inptFechaNacimiento").val(usuario.FechaNacimiento);
//                $("#inptSexo").val(usuario.Sexo);
//                $("#inptTelefono").val(usuario.Telefono);
//                $("#inptCurp").val(usuario.CURP);
//                $("#inptImagen").val(usuario.ImagenBase64);
//                //let idDdlRol = (usuario.Rol.IdRol).toString()
//                //$('#ddlRol option:eq(3)').prop('selected', true)
//                //let ddlRol = $('#ddlRol').find('option[value="' + idDdlRol + '"]').attr("selected", "selected").change()
//                //$('#ddlRol').prop('selected', true)
//                //$('#ddlRol').val(idDdlRol).trigger('change');
//                $("#inptCalle").val(usuario.Direccion.Calle);
//                $("#inptNumeroInterior").val(usuario.Direccion.NumeroInterior);
//                $("#inptNumeroExterior").val(usuario.Direccion.NumeroExterior);
//                //$("#ddlEstado").val(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
//                //$("#ddlMunicipio").val(usuario.Direccion.Colonia.Municipio.IdMunicipio);
//                //$("#ddlColonia").val(usuario.Direccion.Colonia.IdColonia);

//            } else {
//                alert("No se encontró el usuario");
//            }
//        },
//        error: function (xhr) {
//            console.log(xhr);
//        }
//    });
//}



function DDLRol(callback) {
    $.ajax({
        url: urlDDLRol,
        type: 'GET',
        dataType: 'JSON',
        success: function (result) {
            let ddlRol = $('#ddlRol')
            if (result.Correct) {
                ddlRol.empty()
                let optDefault = `<option value=0>Selecciona una opción</option>`
                ddlRol.append(optDefault)
                $.each(result.Objects, function (i, valor) {
                    var rol = `<option value=${valor.IdRol}>${valor.Nombre}</option>`;
                    ddlRol.append(rol);
                })
            }
        },
        error:
            function (xhr) {
                console.log(xhr)
            }
    })
}
function DDLEstado(callback) {
    $.ajax({
        url: urlDDLEstado,
        type: 'GET',
        dataType: 'JSON',
        success: function (result) {
            let ddlEstado = $('#ddlEstado')
            if (result.Correct) {
                let ddlMunicipio = $('#ddlMunicipio')
                let ddlColonia = $('#ddlColonia')
                ddlEstado.empty()
                ddlMunicipio.empty()
                ddlColonia.empty()

                let optDefault = `<option value=0>Selecciona una opción</option>`
                ddlEstado.append(optDefault)
                ddlMunicipio.append(optDefault)
                ddlColonia.append(optDefault)
                $.each(result.Objects, function (i, valor) {
                    var estado = `<option value=${valor.IdEstado}>${valor.Nombre}</option>`;
                    ddlEstado.append(estado);
                })
            }
        },
        error:
            function (xhr) {
                console.log(xhr)
            }
    })
}
function DDLMunicipio(callback) {
    let urlMunicipio = urlDDLMunicipio + $('#ddlEstado').val()

    $.ajax({
        url: urlMunicipio,
        type: 'GET',
        dataType: 'JSON',
        success: function (result) {
            //console.log(result)
            let ddlMunicipio = $('#ddlMunicipio')
            ddlMunicipio.empty()
            if (result.Correct) {
                //console.log(result)
                ddlMunicipio.empty()
                let optDefault = `<option value=0>Selecciona una opción</option>`
                ddlMunicipio.append(optDefault)
                $.each(result.Objects, function (i, valor) {
                    var municipio = `<option value=${valor.IdMunicipio}>${valor.Nombre}</option>`;
                    ddlMunicipio.append(municipio);
                })
            }
        },
        error:
            function (xhr) {
                console.log(xhr)
            }
    })
}
function DDLColonia(callback) {
    let urlColonia = urlDDLColonia + $('#ddlMunicipio').val()
    $.ajax({
        url: urlColonia,
        type: 'GET',
        dataType: 'JSON',
        success: function (result) {
            let ddlColonia = $('#ddlColonia')
            if (result.Correct) {
                //console.log(result)
                ddlColonia.empty()
                let optDefault = `<option value=0>Selecciona una opción</option>`
                ddlColonia.append(optDefault)
                $.each(result.Objects, function (i, valor) {
                    var colonia = `<option value=${valor.IdColonia}>${valor.Nombre}</option>`;
                    ddlColonia.append(colonia);
                })
            }
        },
        error:
            function (xhr) {
                console.log(xhr)
            }
    })
}



function GetById(IdUsuario) {
    $.ajax({
        url: urlGetById,
        type: 'GET',
        dataType: 'JSON',
        data: { IdUsuario: IdUsuario },
        success: function (result) {
            if (result.Correct) {
                var usuario = result.Object;
                console.log("Datos del usuario:", usuario);
                mostrarModal();
                LimpiarForm();

                // Callback para después de cargar el rol
                const despuesCargarRol = function () {
                    let idDdlRol = (usuario.Rol.IdRol).toString();
                    console.log("Después de cargar Rol, intentando seleccionar ID:", idDdlRol);
                    $('#ddlRol').val(idDdlRol).trigger('change');
                };

                // Callback para después de cargar la colonia
                const despuesCargarColonia = function () {
                    let idColonia = usuario.Direccion.Colonia.IdColonia;
                    console.log("Después de cargar Colonia, intentando seleccionar ID:", idColonia);
                    $('#ddlColonia').val(idColonia).trigger('change');
                };

                // Callback para después de cargar el municipio
                const despuesCargarMunicipio = function () {
                    let idMunicipio = usuario.Direccion.Colonia.Municipio.IdMunicipio;
                    console.log("Después de cargar Municipio, intentando seleccionar ID:", idMunicipio);
                    $('#ddlMunicipio').val(idMunicipio).trigger('change');
                    DDLColonia(idMunicipio, despuesCargarColonia);
                };

                // Callback para después de cargar el estado
                const despuesCargarEstado = function () {
                    let idEstado = usuario.Direccion.Colonia.Municipio.Estado.IdEstado;
                    console.log("Después de cargar Estado, intentando seleccionar ID:", idEstado);
                    $('#ddlEstado').val(idEstado).trigger('change');
                    DDLMunicipio(idEstado, despuesCargarMunicipio);
                };

                DDLRol(despuesCargarRol);
                DDLEstado(despuesCargarEstado);

                // Llenar otros campos del formulario
                $("#inptNombre").val(usuario.Nombre);
                $("#inptApellidoPaterno").val(usuario.ApellidoPaterno);
                $("#inptApellidoMaterno").val(usuario.ApellidoMaterno);
                $("#inptCelular").val(usuario.Celular);
                $("#inptUsername").val(usuario.UserName);
                $("#inptEmail").val(usuario.Email);
                $("#inptPassword").val(usuario.Password);
                $("#inptFechaNacimiento").val(usuario.FechaNacimiento);
                $("#inptSexo").val(usuario.Sexo);
                $("#inptTelefono").val(usuario.Telefono);
                $("#inptCurp").val(usuario.CURP);
                $("#inptImagen").val(usuario.ImagenBase64);
                $("#inptCalle").val(usuario.Direccion.Calle);
                $("#inptNumeroInterior").val(usuario.Direccion.NumeroInterior);
                $("#inptNumeroExterior").val(usuario.Direccion.NumeroExterior);

            } else {
                alert("No se encontró el usuario");
            }
        },
        error: function (xhr) {
            console.log(xhr);
        }
    });
}









