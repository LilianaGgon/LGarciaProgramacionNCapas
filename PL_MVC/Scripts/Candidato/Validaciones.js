function ValidarImagen() {
    var input = $('#inptFoto')[0].files[0].name.split(".").pop().toLowerCase() 
    console.log(input)

    var extensionesValidas = ['png', 'jpg', 'jpeg', 'webp'] 
    var bandera = false 

    for (var i = 0; i <= extensionesValidas.length - 1; i++) {
        if (input == extensionesValidas[i]) {
            bandera = true
        }
    }

    if (!bandera) {
        bandera = false
        alert('El archivo no es válido. Ingresa un archivo con extensión png, jpg, jpeg o webp.') 
        $('#inptFoto').val("") 
    }
}

function RemplazarImagen(input) {
    if (input.files) {
        var reader = new FileReader();
        reader.onload = function (elemento) {
            $('#fotoCandidato').attr('src', elemento.target.result)
        }
        reader.readAsDataURL(input.files[0])
    }
}

function ValidarCurriculum() {
    var input = $('#inptCurriculum')[0].value.split(".").pop().toLowerCase()
    console.log(input)

    var extensionesValidas = ['pdf']
    var bandera = false

    for (var i = 0; i <= extensionesValidas.length - 1; i++) {
        if (input == extensionesValidas[i]) {
            bandera = true
        }
    }

    if (!bandera) {
        bandera = false
        alert('El archivo no es válido. Ingresa un archivo con extensión pdf.')
        $('#inptCurriculum').val("")
    }
}

function ValidacionCorreo(evt) {
    var inputField = evt.target
    var ErrorMessage = inputField.parentNode.querySelector('.errorCorreo')
    ErrorMessage.textContent = ''
    var entrada = inputField.value

    if (!(/^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$/.test(entrada))) {
        evt.preventDefault()
        inputField.style.borderColor = 'red'
        ErrorMessage.textContent = 'ejemplo@email.com'
    } else {
        inputField.style.borderColor = 'green'
    }
}

function ValidacionNumeros(evt) {
    var entrada = String.fromCharCode(evt.which)
    var inputField = evt.target
    var ErrorMessage = inputField.parentNode.querySelector('.errorNumeros')
    ErrorMessage.textContent = ''
    if (!(/[0-9]/.test(entrada))) {
        evt.preventDefault()
        inputField.style.borderColor = 'red'
        ErrorMessage.textContent = 'Solo se permiten números'
    } else {
        inputField.style.borderColor = 'green'
    }
}

function ValidacionLetras(evt) {
    var entrada = String.fromCharCode(evt.which) 
    var inputField = evt.target; 
    var ErrorMessage = inputField.parentNode.querySelector('.errorLetras') 
    ErrorMessage.textContent = ''; 
    if (!(/^[a-z A-Z]$/.test(entrada))) { 
        evt.preventDefault() 
        inputField.style.borderColor = 'red' 
        ErrorMessage.textContent = 'Solo se permiten letras' 
    } else {
        inputField.style.borderColor = 'green'
    }
}

function ValidacionDireccion(evt) {
    var entrada = String.fromCharCode(evt.which)
    var inputField = evt.target
    var ErrorMessage = inputField.parentNode.querySelector('.errorDireccion')
    ErrorMessage.textContent = ''
    if (!(/[aA-zZ0-9.-]/.test(entrada))) {
        evt.preventDefault()
        inputField.style.borderColor = 'red'
        ErrorMessage.textContent = 'Solo puede contener letras, numeros y signos como: .-'
    } else {
        inputField.style.borderColor = 'green'
    }
}