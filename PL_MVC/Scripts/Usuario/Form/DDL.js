function MunicipioGetByIdEstado() { //Nombre de la funcion
    let ddl = urlDDLMunicipio + $('#ddlEstado').val() //Obtiene el valor del select de la etiqueta con id ddlEstado y lo guarda en una variable let
    //console.log(ddl)
    $.ajax({ //Se invoca a ajax y es un metodo que recibe un json
        url: ddl, //la accion del url es el nombre del metodo del controller a ejecutar, el nombre del controlador y se le concatena el valor del ddl
        type: "GET", //Metodo a usar
        dataType: "JSON", //Tipo de arcivo que va a devolver
        //No se usa la propiedad data porque solo se va a mandar el id, no un modelo completo. Data solo se usa cuando se trabjaa con modelos
        success: function (result) { //Una funcion que devuelve un result
            //console.log(result) //Imprime en consola lo que trae el result
            if (result.Correct) {
                let ddlMunicipio = $('#ddlMunicipio') //Busca el ddl donde se van a pintar los valores

                $.each(result.Objects, function (i, valor) { //Foreach. por cada valor en el result.objects se ejecuta una funcion. i es el index, nos da la posicion en la que se encuentra, es un iterados y siempre va ahi, valor es lo que queremos objeter del objects
                    //let option = "<option value=" + valor.IdMunicipio + ">" + valor.Nombre + "</option>" //Concatenacion normal
                    let option = `<option value = ${valor.IdMunicipio}>${valor.Nombre}</option>`; //Concatenacion con backticks
                    ddlMunicipio.append(option)

                })
            }
        },
        error: function (xhr) {
            console.log(xhr)
        }
    })
}


function ColoniaGetByIdMunicipio() {
    let ddlMunicipio = $('#ddlMunicipio').val()
    $.ajax({
        url: urlDDLColonia + ddlMunicipio,
        type: "GET",
        dataType: "JSON",
        success: function (result) {
            //console.log(result)
            if (result.Correct) {
                let ddlColonia = $('#ddlColonia')

                $.each(result.Objects, function (i, valor) {
                    let option = `<option value = ${valor.IdColonia}>${valor.Nombre}</option>`;
                    ddlColonia.append(option)
                })
            }
        },
        error: function (xhr) {
            console.log(xhr)
        }
    })
}