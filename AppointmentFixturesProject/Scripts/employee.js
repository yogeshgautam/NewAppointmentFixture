/// <reference path="jquery-1.9.1.intellisense.js" />
//Load Data in Table when documents is ready
$(document).ready(function () {
    loadData();
});
//Load Data function
function loadData()
{
    $.ajax({
        url: "/VIP/List",
        type: "GET",
        contentType: "json",
        dataType: "json",
        success: function (result) {
            //debugger
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.Id + '</td>';
                html += '<td>' + item.Date + '</td>';
                //var date = new Date(item.StartTime);          
                html += '<td>' + item.StartTime + '</td>';
                html += '<td>' + item.EndTime + '</td>';
                html += '<td>' + item.IsAvailable + '</td>';
                html += '<td><a href="#" class="btn btn-info" onclick="return getbyID(' + item.Id + ')">Edit</a>   <a href="#" class="btn btn-danger" onclick="Delete(' + item.Id + ')">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            //alert(errormessage.response);
        }
    });
}
function getbyID(EmpID) {
    $('#Date').css('border-color', 'lightgrey');
    $('#StartTime').css('border-color', 'lightgrey');
    $('#EndTime').css('border-color', 'lightgrey');
    $('#IsAvailable').css('border-color', 'lightgrey');
    $.ajax({
        url: "/VIP/getbyID/" + EmpID,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            //debugger;
            $('#AppointmentId').val(result.Id);
            $('#Date').val(result.Date);
            $('#StartTime').val(result.StartTime);
            $('#EndTime').val(result.EndTime);
            $('#IsAvailable').val(result.IsAvailable);
            $('#myModal').modal('show')
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            //alert(errormessage.responseText);
        }
    });
    return false;
}

function Delete(ID) {
    var ans = confirm("Are you sure you want to delete this Record?");
    if (ans) {
        $.ajax({
            url: "/VIP/Delete/" + ID,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                loadData();
            },
            error: function (errormessage) {
                //alert(errormessage.responseText);
            }
        });
    }
}
function Add() {

    var res = validate();
    if (res == false) {
        return false;
    }
    var empObj =
        {
            Id: $('#AppointmentId').val(),
            Date: $('#Date').val(),
            StartTime: $('#StartTime').val(),
            EndTime: $('#EndTime').val(),
            IsAvailable: $('#IsAvailable').val()
        };
  
    $.ajax({
        url: "/VIP/Add",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
        },
        error: function (errormessage) {
            //alert(errormessage.responseText);
        }
    });
}

function clearTextBox() {
    $('#AppointmentId').val("");
    $('#Date').val("");
    $('#StartTime').val("");
    $('#EndTime').val("");
    $('#IsAvailable').val("");
   
    $('#Date').css('border-color', 'lightgrey');
    $('#StartTime').css('border-color', 'lightgrey');
    $('#EndTime').css('border-color', 'lightgrey');
    $('#IsAvailable').css('border-color', 'lightgrey');
}


function Update() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var empObj = {
        Id: $('#AppointmentId').val(),
        Date: $('#Date').val(),
        StartTime: $('#StartTime').val(),
        EndTime: $('#EndTime').val(),
        IsAvailable: $('#IsAvailable').val(),
    };
    debugger;
    $.ajax({
        url: "/VIP/Update",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
            $('#AppointmentId').val(""); /// i 423
            $('#Date').val("");
            $('#StartTime').val("");
            $('#EndTime').val("");
            $('#IsAvailable').val("");
        },
        error: function (errormessage) {
           // alert(errormessage.responseText);
        }
    });
}


function validate() {
    var isValid = true;
   
    if ($('#Date').val().trim() == "") {
        $('#Date').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Date').css('border-color', 'lightgrey');
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
    if ($('#IsAvailable').val().trim() == "") {
        $('#IsAvailable').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#IsAvailable').css('border-color', 'lightgrey');
    }
    return isValid;
}