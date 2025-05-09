$(document).ready(function () {
    GetAll();

    $('#miModal').off('shown.bs.modal').on('shown.bs.modal', function () {
        var usuarioData = $(this).data('usuario');

        if (usuarioData) {
            console.log("Datos del usuario para selección:", usuarioData);

            // Establecer el valor del rol
            let idDdlRol = (usuarioData.Rol.IdRol).toString();
            console.log("Seleccionando rol con ID:", idDdlRol);
            $('#ddlRol').val(idDdlRol);

            // Establecer el valor del estado y disparar el evento 'change'
            let idEstado = usuarioData.Direccion.Colonia.Municipio.Estado.IdEstado;
            console.log("Seleccionando estado con ID:", idEstado);
            $('#ddlEstado').val(idEstado).trigger('change');

            // Callback para después de que los municipios se hayan cargado
            const despuesCargarMunicipio = function () {
                let idMunicipio = usuarioData.Direccion.Colonia.Municipio.IdMunicipio;
                console.log("Seleccionando municipio con ID:", idMunicipio);
                $('#ddlMunicipio').val(idMunicipio).trigger('change');

                // Callback para después de que las colonias se hayan cargado
                const despuesCargarColonia = function () {
                    let idColonia = usuarioData.Direccion.Colonia.IdColonia;
                    console.log("Seleccionando colonia con ID:", idColonia);
                    $('#ddlColonia').val(idColonia);
                };

                // Adjuntar el callback a la función DDLColonia
                $._data($('#ddlColonia')[0], 'events').success = [function (event) { despuesCargarColonia(); }];
            };

            // Adjuntar el callback a la función DDLMunicipio
            $._data($('#ddlMunicipio')[0], 'events').success = [function (event) { despuesCargarMunicipio(); }];
        }
    });

    // Asegurarse de que los listeners 'change' se adjunten solo una vez
    $('#ddlEstado').off('change').on('change', function () {
        var idEstado = $(this).val();
        if (idEstado > 0) {
            DDLMunicipio(idEstado);
            $('#ddlColonia').empty().append('<option value="0">Selecciona una opción</option>');
        } else {
            $('#ddlMunicipio').empty().append('<option value="0">Selecciona una opción</option>');
            $('#ddlColonia').empty().append('<option value="0">Selecciona una opción</option>');
        }
    });

    $('#ddlMunicipio').off('change').on('change', function () {
        var idMunicipio = $(this).val();
        if (idMunicipio > 0) {
            DDLColonia(idMunicipio);
        } else {
            $('#ddlColonia').empty().append('<option value="0">Selecciona una opción</option>');
        }
    });
});


function Formulario() {
    LimpiarForm();
    DDLRol();
    DDLEstado();
    // Ya no llamamos a DDLMunicipio ni DDLColonia aquí, se cargarán dinámicamente
}

function GetAll() {
    $.ajax({
        url: urlGetAll,
        type: 'GET',
        dataType: 'JSON',
        success: function (result) {
            let tbodyCrudJS = $('#tbodyCrudJS');
            tbodyCrudJS.empty(); // Limpiar la tabla antes de agregar datos
            if (result.Correct) {
                $.each(result.Objects, function (i, valor) {
                    var imagenValidada = "";
                    if (valor.ImagenBase64 != "") {
                        imagenValidada = `"data:image/*;base64, ${valor.ImagenBase64}"`;
                    } else {
                        imagenValidada = "https://media.istockphoto.com/id/1473534533/es/vector/avatar-de-imagen-de-perfil-predeterminado-icono-de-avatar-de-usuario-icono-de-persona-icono.jpg?s=612x612&w=0&k=20&c=pxEpFr5yhE4zLcev7LFRFS2uHoe29fq_JKQ3RsRAmlU=";
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
                });
            }
        },
        error: function (xhr) {
            console.log(xhr);
        }
    });
}

function mostrarModal() {
    $('#modalForm').modal('show');
}

function Guardar() {
    $.ajax({
        url: urlForm,
        type: 'POST',
        dataType: 'json',
        data: {
            IdUsuario: $("#hdnIdUsuario").val(), // Asegúrate de tener un campo oculto para el ID en edición
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
                alert("Usuario guardado con éxito");
                location.reload();
            } else {
                alert("El usuario no se pudo guardar");
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
                alert("Registro eliminado con éxito");
                location.reload();
            } else {
                alert("No se pudo eliminar el registro");
            }
        },
        error: function (xhr) {
            console.log(xhr);
        }
    });
}

function LimpiarForm() {
    $('input').each(function () {
        let inpt = $(this);
        inpt.val("");
    });

    $('select').each(function () {
        let slct = $(this);
        slct.val("0");
    });
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
                console.log("Datos del usuario (GetById):", usuario);
                LimpiarForm();
                DDLRol(function () {
                    DDLEstado(function () {
                        // Almacenar los datos del usuario en el modal para usarlos en 'shown.bs.modal'
                        $('#modalForm').data('usuario', usuario);
                        mostrarModal();
                    });
                });

                $("#hdnIdUsuario").val(usuario.IdUsuario); // Si estás implementando edición
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

function DDLRol(callback) {
    $.ajax({
        url: urlDDLRol,
        type: 'GET',
        dataType: 'JSON',
        success: function (result) {
            let ddlRol = $('#ddlRol');
            ddlRol.empty().append('<option value="0">Selecciona una opción</option>');
            if (result.Correct) {
                $.each(result.Objects, function (i, valor) {
                    ddlRol.append(`<option value="${valor.IdRol}">${valor.Nombre}</option>`);
                });
                if (typeof callback === 'function') {
                    console.log("DDLRol completado.");
                    callback();
                }
            } else {
                console.error("Error al cargar roles.");
            }
        },
        error: function (xhr) {
            console.log("Error en DDLRol:", xhr);
        }
    });
}

function DDLEstado(callback) {
    $.ajax({
        url: urlDDLEstado,
        type: 'GET',
        dataType: 'JSON',
        success: function (result) {
            let ddlEstado = $('#ddlEstado');
            let ddlMunicipio = $('#ddlMunicipio');
            let ddlColonia = $('#ddlColonia');

            ddlEstado.empty().append('<option value="0">Selecciona una opción</option>');
            ddlMunicipio.empty().append('<option value="0">Selecciona una opción</option>');
            ddlColonia.empty().append('<option value="0">Selecciona una opción</option>');

            if (result.Correct) {
                $.each(result.Objects, function (i, valor) {
                    ddlEstado.append(`<option value="${valor.IdEstado}">${valor.Nombre}</option>`);
                });
                if (typeof callback === 'function') {
                    console.log("DDLEstado completado.");
                    callback();
                }
            } else {
                console.error("Error al cargar estados.");
            }
        },
        error: function (xhr) {
            console.log("Error en DDLEstado:", xhr);
        }
    });
}

function DDLMunicipio(idEstado, callback) {
    let urlMunicipio = urlDDLMunicipio + idEstado;
    console.log("URL para municipios:", urlMunicipio);
    $.ajax({
        url: urlMunicipio,
        type: 'GET',
        dataType: 'JSON',
        success: function (result) {
            let ddlMunicipio = $('#ddlMunicipio');
            ddlMunicipio.empty().append('<option value="0">Selecciona una opción</option>');
            if (result.Correct) {
                $.each(result.Objects, function (i, valor) {
                    ddlMunicipio.append(`<option value="${valor.IdMunicipio}">${valor.Nombre}</option>`);
                });
                if (typeof callback === 'function') {
                    console.log("DDLMunicipio completado.");
                    callback();
                } else if ($._data($('#ddlMunicipio')[0], 'events') && $._data($('#ddlMunicipio')[0], 'events').success) {
                    $.each($._data($('#ddlMunicipio')[0], 'events').success, function (i, handler) {
                        handler.handler();
                    });
                }
            } else {
                console.error("Error al cargar municipios para el estado:", idEstado);
            }
        },
        error: function (xhr) {
            console.log("Error en DDLMunicipio:", xhr);
        }
    });
}

function DDLColonia(idMunicipio, callback) {
    let urlColonia = urlDDLColonia + idMunicipio;
    console.log("URL para colonias:", urlColonia);
    $.ajax({
        url: urlColonia,
        type: 'GET',
        dataType: 'JSON',
        success: function (result) {
            let ddlColonia = $('#ddlColonia');
            ddlColonia.empty().append('<option value="0">Selecciona una opción</option>');
            if (result.Correct) {
                $.each(result.Objects, function (i, valor) {
                    ddlColonia.append(`<option value="${valor.IdColonia}">${valor.Nombre}</option>`);
                });
                if (typeof callback === 'function') {
                    console.log("DDLColonia completado.");
                    callback();
                } else if ($._data($('#ddlColonia')[0], 'events') && $._data($('#ddlColonia')[0], 'events').success) {
                    $.each($._data($('#ddlColonia')[0], 'events').success, function (i, handler) {
                        handler.handler();
                    });
                }
            } else {
                console.error("Error al cargar colonias para el municipio:", idMunicipio);
            }
        },
        error: function (xhr) {
            console.log("Error en DDLColonia:", xhr);
        }
    });
}

$(document).ready(function () {
    $('#ddlEstado').off('change').on('change', function () {
        var idEstado = $(this).val();
        if (idEstado > 0) {
            DDLMunicipio(idEstado);
            $('#ddlColonia').empty().append('<option value="0">Selecciona una opción</option>');
        } else {
            $('#ddlMunicipio').empty().append('<option value="0">Selecciona una opción</option>');
            $('#ddlColonia').empty().append('<option value="0">Selecciona una opción</option>');
        }
    });

    $('#ddlMunicipio').off('change').on('change', function () {
        var idMunicipio = $(this).val();
        if (idMunicipio > 0) {
            DDLColonia(idMunicipio);
        } else {
            $('#ddlColonia').empty().append('<option value="0">Selecciona una opción</option>');
        }
    });
});

function mostrarModal() {
    $('#modalForm').modal('show');
}