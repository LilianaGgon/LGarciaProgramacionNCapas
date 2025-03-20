function ValidarImagen() {
    var input = $('#inptFileImgUsuario')[0].files[0].name.split(".").pop().toLowerCase() //De lo agregado en el input con id inptFileImgUsuario va a acceder a su posicion 0 del objeto, despues va a tomar la posicion 0 del files. de files toma la propiedad name y la va a dividir en un array cuando encuentre el punto. Pop va a eliminar la primera parte del array y se queda solo con las letras de la extension del archivo. En caso de que las letras de extension del archivo esté en mayúculas, las pasa a minusculas para no generar falsos errores.
    console.log(input) //imprime la extension con las modificaciones ya hechas

    var extensionesValidas = ['png', 'jpg', 'jpeg', 'webp'] //array con extensiones validas para comparar el input
    var bandera = false //Bandera para comprobar que la extension es valida o no

    //Ciclo para comparar input con extensiones válidas
    for (var i = 0; i <= extensionesValidas.length - 1; i++) {
        if (input == extensionesValidas[i]) {
            bandera = true
        }
    }

    if (!bandera) {
        bandera = false
        alert('El archivo no es válido. Ingresa un archivo con extensión png, jpg, jpeg o webp.') //Avisa que el archivo no es válido
        $('#inptFileImgUsuario').val("") //Vacia el input para poder cargar otro archivo
    }
}

function RemplazarImagen(input) {
    if (input.files) {
        var reader = new FileReader();
        reader.onload = function (elemento) {
            $('#imgUsuario').attr('src', elemento.target.result)
        }
        reader.readAsDataURL(input.files[0])
    }
}

function ValidacionSoloLetras(evt) { //Función para validar si solo se ingresan letras
    var entrada = String.fromCharCode(evt.which) //entrada toma el valor de la letra seleccionada en formato Unicode
    var inputField = evt.target; //
    var ErrorMessage = inputField.parentNode.querySelector('.errorSoloLetras') //El ErrorMessage lo ingresa en el span asignado
    ErrorMessage.textContent = ''; //Reinicia el mensaje de error y lo pone vacio
    if (!(/^[a-z A-Z]$/.test(entrada))) { //Evalua si el caracter ingresado es diferente a un caracter valido en la expresion regular. Test hace el testeo de la letra
        evt.preventDefault() //Evita que se siga con el proceso con en el que estaba
        inputField.style.borderColor = 'red' //Colorea el borde del span de color rojo cuando se ingresa algo diferente a una letra
        ErrorMessage.textContent = 'Solo se permiten letras' //El mensaje de error de lanza es que solo se permiten letras
    } else {
        inputField.style.borderColor = 'green' //Colorea el borde del span de color verde cuando se aceptan caracteres validos
    }
}

function ValidacionSoloNumeros(evt) {
    var entrada = String.fromCharCode(evt.which)
    var inputField = evt.target
    var ErrorMessage = inputField.parentNode.querySelector('.errorSoloNumeros')
    ErrorMessage.textContent = ''
    if (!(/[0-9]/.test(entrada))) {
        evt.preventDefault()
        inputField.style.borderColor = 'red'
        ErrorMessage.textContent = 'Solo se permiten números'
    } else {
        inputField.style.borderColor = 'green'
    }
}

function ValidacionEmail(evt) {
    var inputField = evt.target
    var ErrorMessage = inputField.parentNode.querySelector('.errorEmail')
    ErrorMessage.textContent = ''
    var entrada = inputField.value
    if (!(/^[^\s@@]+@@[^\s@@]+\.[^\s@@]+$/.test(entrada))) {
        evt.preventDefault()
        inputField.style.borderColor = 'red'
        ErrorMessage.textContent = 'ejemplo@email.com'
    } else {
        inputField.style.borderColor = 'green'
    }
}

function ValidacionNumerosLetras(evt) {
    var entrada = String.fromCharCode(evt.which)
    var inputField = evt.target
    var ErrorMessage = inputField.parentNode.querySelector('.errorNumerosLetras')
    ErrorMessage.textContent = ''
    if (!(/[aA-zZ 0-9]/.test(entrada))) {
        evt.preventDefault()
        inputField.style.borderColor = 'red'
        ErrorMessage.textContent = 'Solo se permiten números y letras'
    } else {
        inputField.style.borderColor = 'green'
    }
}

function ValidacionUserName(evt) {
    var entrada = String.fromCharCode(evt.which)
    var inputField = evt.target
    var ErrorMessage = inputField.parentNode.querySelector('.errorUsername')
    ErrorMessage.textContent = ''
    if (!(/[aA-zZ0-9_.-]/.test(entrada))) {
        evt.preventDefault()
        inputField.style.borderColor = 'red'
        ErrorMessage.textContent = 'Solo puede contener letras, numeros y signos como: .-_'
    } else {
        inputField.style.borderColor = 'green'
    }
}

function ValidacionCurp(evt) {
    var inputField = evt.target
    var ErrorMessage = inputField.parentNode.querySelector('.errorCurp')
    ErrorMessage.textContent = ''
    var entrada = inputField.value
    if (!(/^([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$/.test(entrada))) {
        evt.preventDefault()
        inputField.style.borderColor = 'red'
        ErrorMessage.textContent = 'Curp inválido'
    } else {
        inputField.style.borderColor = 'green'
    }
}

function ValidacionPassword(evt) {
    var inputField = evt.target
    var ErrorMessage = inputField.parentNode.querySelector('.errorPassword')
    ErrorMessage.textContent = ''
    var password = inputField.value

    if (!(/^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$/gm.test(password))) {
        evt.preventDefault()
        inputField.style.borderColor = 'red'
        ErrorMessage.textContent = 'Mínimo 8 caracteres, una mayuscula, una minuscula y un signo.'
    } else {
        inputField.style.borderColor = 'green'
    }
}

function ConfirmacionPassword(evt) {
    var password = $('#password').val()
    var confirmacionPassword = $('#inptConfirmacionPassword').val()
    $('.errorCoincidenciaPassword').html('');
    if (password != confirmacionPassword) {
        document.getElementById("inptConfirmacionPassword").style.borderColor = "red"
        $('.errorCoincidenciaPassword').html('Las contraseñas no coinciden');
    } else {
        document.getElementById("inptConfirmacionPassword").style.borderColor = "green"

    }
}

function validarFormulario(evt) {
    let isValid = true;

    $(".inputObligatorio").each(function () {
        let input = $(this);
        let valor = input.val().trim()
        let p = input.siblings('p');
        //let p = $(".errorMensaje")

        if (valor === "") {
            isValid = false;
            input.css("border-color", "red");
            p.text("Este campo no puede estar vacío").css("color", "red");
        } else {
            input.css("border-color", "green");
            p.text("").css("color", "green");
        }
    });

    if (!isValid) {
        evt.preventDefault();
    }
}
document.getElementById("form").addEventListener("submit", validarFormulario);
