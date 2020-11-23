$(document).ready(function () {
    loadData();
    listContinente();
});  

let fechaFormateada = function (fecha) {
    nuevaFecha =  moment(fecha).format("DD/MM/YYYY")
    return nuevaFecha;
}

//Load Data function  
function loadData() {
    $.ajax({
        url: "/Jugador/ListaJugador",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += `<tr>`;
                html += `<td>` + item.ide_jug + `</td>`;
                html += `<td>` + item.nom_jug + `</td>`;
                html += `<td>` + fechaFormateada(item.fna_jug) + `</td>`;
                html += `<td>` + item.nom_pais + `</td>`;
                html += `<td>` + item.nom_con + `</td>`;
                html += `<td>` + item.pes_jug + `</td>`;
                html += `<td><a href="#" onclick="return getbyID('${item.ide_jug}');">Editar</a> | <a href="#" onclick="Delete('${item.ide_jug}');">Eliminar</a></td>`;
                html += `</tr>`;
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function listContinente() {
    $.ajax({
        type: "GET",
        url: "/Jugador/ListaContinentes",
        data: "{}",
        success: function (data) {
            var s = `<option value="-1">Seleccione un continente</option>`;
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].ide_con + '">' + data[i].nom_con + '</option>';
            }
            $("#continentes").html(s);
        }
    });
    $('#continentes').change(function () {
        $('#paises').empty();
        $.ajax({
            type: "POST",
            url: "/Jugador/ListPaises",
            datatype: "Json",
            data: { ide_con_pais: $('#continentes').val() },
            success: function (data) {
                $.each(data, function (index, value) {
                    $('#paises').append('<option value="' + value.ide_pais + '">' + value.nom_pais + '</option>');
                });
            }
        });
    });
}

//Add Data Function   
function Add() {
    var jugObj = {
        nom_jug: $('#nom_jug').val(),
        fna_jug: $('#fna_jug').val(),
        nom_pais: $('#paises').val(),
        nom_con: $('#continentes').val(),
        pes_jug: $('#pes_jug').val(),
    };
    $.ajax({
        url: "/Jugador/Add",
        data: JSON.stringify(jugObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    form = document.querySelector('form');
    form.reset();
}

//Function for getting the Data Based upon Employee ID  
function getbyID(EmpID) {
    $('#nom_jug').css('border-color', 'lightgrey');
    $('#fna_jug').css('border-color', 'lightgrey');
    $('#paises').css('border-color', 'lightgrey');
    $('#continentes').css('border-color', 'lightgrey');
    $('#paises').css('border-color', 'lightgrey');
    $('#pes_jug').css('border-color', 'lightgrey');
    $.ajax({
        url: "/Jugador/getbyID",
        type: "POST",
        data: `{'idjug': '${EmpID}'}`,
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            //console.log(result);
            $('#ide_jug').val(result.ide_jug);
            $('#nom_jug').val(result.nom_jug);
            $('#fna_jug').val(fechaFormateada(result.fna_jug));
            $(`#continentes option[value='${result.ide_con}']`).prop('selected', true);
            $(`#paises option[value='${result.ide_pais}']`).prop('selected', true);
            $('#pes_jug').val(result.pes_jug);
            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

//function for updating employee's record  
function Update() {
    var jugObj = {
        ide_jug: $('#ide_jug').val(),
        nom_jug: $('#nom_jug').val(),
        fna_jug: $('#fna_jug').val(),
        ide_con: $('#continentes').val(),
        ide_pais: $('#paises').val(),
        pes_jug: $('#pes_jug').val(),
    };
    console.log(jugObj);
    $.ajax({
        url: "/Jugador/Update",
        data: JSON.stringify(jugObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
            $('#nom_jug').val("");
            $('#fna_jug').val("");
            $('#continentes').val("");
            $('#paises').val("");
            $('#pes_jug').val("");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//function for deleting employee's record  
function Delete(ID) {
    var ans = confirm("Estas seguro de eliminar este registro?");
    if (ans) {
        $.ajax({
            url: "/Jugador/Delete/",
            type: "POST",
            data: `{"idjug": "${ID}"}`,
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                loadData();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

//Function for clearing the textboxes  
function clearTextBox() {
    $('#ide_jug').val("");
    $('#nom_jug').val("");
    $('#fna_jug').val("");
    $('#paises').val("");
    $('#continentes').val("");
    $('#pes_jug').val("");
    $('#btnAdd').show();
    $('#nom_jug').css('border-color', 'lightgrey');
    $('#fna_jug').css('border-color', 'lightgrey');
    $('#paises').css('border-color', 'lightgrey');
    $('#continentes').css('border-color', 'lightgrey');
    $('#pes_jug').css('border-color', 'lightgrey');
}