$(document).ready(function () {
    console.log('ready!')
    CitaDefault()
    EstatusCita()
})

function CitaDefault() {
    $('#btnCitaVirtual').removeClass('btn btn-dark').addClass('btn btn-outline-dark')
    $('#btnCitaPresencial').removeClass('btn btn-outline-dark').addClass('btn btn-dark')
    $('#colInptUrl').addClass('visually-hidden')
    $('#colDdlPiso').removeClass('visually-hidden')
}

function CitaPresencial(evt) {
    $('#btnCitaVirtual').removeClass('btn btn-dark').addClass('btn btn-outline-dark')
    $('#btnCitaPresencial').removeClass('btn btn-outline-dark').addClass('btn btn-dark')
    $('#colInptUrl').addClass('visually-hidden')
    $('#colDdlPiso').removeClass('visually-hidden')
    $('#inptUrl').val(null)

}

function CitaVirtual(evt) {
    $('#btnCitaPresencial').removeClass('btn btn-dark').addClass('btn btn-outline-dark')
    $('#btnCitaVirtual').removeClass('btn btn-outline-dark').addClass('btn btn-dark')
    $('#colDdlPiso').addClass('visually-hidden')
    $('#colInptUrl').removeClass('visually-hidden')
    $('#ddlPiso').val("")

}

function EstatusCita() {
    let idCitaHiddenFor = $('#idCitaHiddenFor').val()
    console.log(idCitaHiddenFor)

    if (idCitaHiddenFor == 0) {
        $('#ddlEstatusCita').attr("disabled", "disabled")

    }

}


