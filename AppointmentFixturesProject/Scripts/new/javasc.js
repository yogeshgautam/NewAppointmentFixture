/// <reference path="jquery-1.9.1.intellisense.js" />



$(document).ready(function () {
    loadData();
});
//Load Data function
function loadData() {
    $.ajax({
        url: "/Company/List",
        type: "GET",
        contentType: "json",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.ID + '</td>';
                html += '<td>' + item.VIPname + '</td>';
                html += '<td>' + item.Users + '</td>';
                html += '<td>' + item.StartTime + '</td>';
                html += '<td>' + item.EndTime + '</td>';
                html += '<td>' + item.Date + '</td>';
                html += '<td><a href="#"  class="btn btn-info"  onclick="return getbyID(' + item.ID + ')" >Edit </a>  <a href="#" class="btn btn-danger" onclick="Delete(' + item.ID + ')">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function getbyID(MeetID) {

    $('#VIPuser').css('border-color', 'lightgrey');
    $('#Users').css('border-color', 'lightgrey');
    $('#StartTime').css('border-color', 'lightgrey');
    $('#EndTime').css('border-color', 'lightgrey');
    $('#Date').css('border-color', 'lightgrey');
    $.ajax({
        url: "/Company/GetByID/" + MeetID,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            debugger;
            $('#ID').val(result.ID);
            $('#VIPuser').val(result.VIPuser);
            $('#Users').val(result.Users);
            $('#StartTime').val(result.StartTime);
            $('#EndTime').val(result.EndTime);
            $('#Date').val(result.Date);

            $('#myModal').modal('show')
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}
function Delete(ID) {
    var ans = confirm("Are you sure you want to delete this Record?");
    if (ans) {
        $.ajax({
            url: "/Company/Delete/" + ID,
            type: "POST",
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
function Add() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var meetObj =
        {
            ID: $('#ID').val(),
            VIPuser: $('#VIPuser').val(),
            Users: $('#Users').val(),
            StartTime: $('#StartTime').val(),
            EndTime: $('#EndTime').val(),
            Date: $('#Date').val()
        };
    $.ajax({
        url: "/Company/Add",
        data: JSON.stringify(meetObj),
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
}

function clearTextBox() {
    $('#ID').val("");
    $('#VIPuser').val("");
    $('#Users').val("");
    $('#StartTime').val("");
    $('#EndTime').val("");
    $('#Date').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    //$('#Name').css('border-color', 'lightgrey');
    //$('#Age').css('border-color', 'lightgrey');
    //$('#State').css('border-color', 'lightgrey');
    //$('#Country').css('border-color', 'lightgrey');
}
function Update() {
    //var res = validate();
    //if (res == false) {
    //    return false;
    //}
    var empObj = {
        ID: $('#ID').val(),
        VIPuser: $('#VIPuser').val(),
        Users: $('#Users').val(),
        StartTime: $('#StartTime').val(),
        EndTime: $('#EndTime').val(),
        Date: $('#Date').val(),
    };
    $.ajax({
        url: "/Company/UpdateMeetingOne",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",

        success: function (result) {
            loadData(); debugger;
            $('#myModal').modal('hide');
            $('#ID').val("");
            $('#VIPuser').val("");
            $('#Users').val("");
            $('#StartTime').val("");
            $('#EndTime').val("");
            $('#Date').val("");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function validate() {
    var isValid = true;
    if ($('#VIPuser').val().trim() == "") {
        $('#VIPuser').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#VIPuser').css('border-color', 'lightgrey');
    }
    if ($('#User').val().trim() == "") {
        $('#User').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#User').css('border-color', 'lightgrey');
    }
    if ($('#StartTime').val().trim() == "") {
        $('#StartTime').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#StartTime').css('border-color', 'lightgrey');
    }
    if ($('#EndTime').val().trim() == "") {
        $('#EndTime').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#EndTime').css('border-color', 'lightgrey');
    }
    if ($('#Date').val().trim() == "") {
        $('#Date').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Date').css('border-color', 'lightgrey');
    }

    return isValid;
}