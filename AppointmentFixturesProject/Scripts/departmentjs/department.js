/// <reference path="jquery-1.9.1.intellisense.js" />

$(document).ready(function () {

    GetPageData(1);
});
function GetPageData(pageNum, pageSize) {
    //After every trigger remove previous data and paging
    $(".tbody").empty();
    $("#paged").empty();
    $.getJSON("/Company/GetPaggedData", { pageNumber: pageNum, pageSize: pageSize }, function (response) {
        var html = "";

        for (var i = 0; i < response.Data.length; i++) {
            html = html + '<tr><td>' + response.Data[i].Name + '</td><td>' + response.Data[i].HOD + '</td>';
            html += '<td>' + response.Data[i].phone + '</td><td>' + response.Data[i].Email + '</td>';
            html += '<td>' + response.Data[i].Details + '</td>';
            html += '<td><a href="#" class="btn btn-info" onclick="return getbyID(' + response.Data[i].Id + ')">Edit</a>    <a href="#" class="btn btn-danger" onclick="Delete(' + response.Data[i].Id + ')">Delete</a></td>';
            html += "</tr>";

        }

        $(".tbody").html(html);
        PaggingTemplate(response.TotalPages, response.CurrentPage);
    });
}
function PaggingTemplate(totalPage, currentPage) {
    var template = "";
    var TotalPages = totalPage;
    var CurrentPage = currentPage;
    var PageNumberArray = Array();


    var countIncr = 1;
    for (var i = currentPage; i <= totalPage; i++) {
        PageNumberArray[0] = currentPage;
        if (totalPage != currentPage && PageNumberArray[countIncr - 1] != totalPage) {
            PageNumberArray[countIncr] = i + 1;
        }
        countIncr++;
    };
    PageNumberArray = PageNumberArray.slice(0, 5);
    var FirstPage = 1;
    var LastPage = totalPage;
    if (totalPage != currentPage) {
        var ForwardOne = currentPage + 1;
    }
    var BackwardOne = 1;
    if (currentPage > 1) {
        BackwardOne = currentPage - 1;
    }

    template = "<p>" + CurrentPage + " of " + TotalPages + " pages</p>"
    template = template + '<ul class="pager">' +
        '<li class="previous"><a href="#" onclick="GetPageData(' + FirstPage + ')"><i class="fa fa-fast-backward"></i>&nbsp;First</a></li>' +
        '<li><select ng-model="pageSize" id="selectedId"><option value="20" selected>20</option><option value="50">50</option><option value="100">100</option><option value="150">150</option></select> </li>' +
        '<li><a href="#" onclick="GetPageData(' + BackwardOne + ')"><i class="glyphicon glyphicon-backward"></i></a>';

    var numberingLoop = "";
    for (var i = 0; i < PageNumberArray.length; i++) {
        numberingLoop = numberingLoop + '<a class="page-number active" onclick="GetPageData(' + PageNumberArray[i] + ')" href="#">' + PageNumberArray[i] + ' &nbsp;&nbsp;</a>'
    }
    template = template + numberingLoop + '<a href="#" onclick="GetPageData(' + ForwardOne + ')" ><i class="glyphicon glyphicon-forward"></i></a></li>' +
        '<li class="next"><a href="#" onclick="GetPageData(' + LastPage + ')">Last&nbsp;<i class="fa fa-fast-forward"></i></a></li></ul>';
    $("#paged").append(template);
    $('#selectedId').change(function () {
        GetPageData(1, $(this).val());
    });
}
function getbyID(MeetID) {

    $('#VIPuser').css('border-color', 'lightgrey');
    $('#Users').css('border-color', 'lightgrey');
    $('#StartTime').css('border-color', 'lightgrey');
    $('#EndTime').css('border-color', 'lightgrey');
    $('#Date').css('border-color', 'lightgrey');
    $.ajax({
        url: "/Company/GetbyIDDepartments/" + MeetID,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

            debugger;
            $('#Id').val(result.Id);
            $('#Name').val(result.Name);
            $('#phone').val(result.phone);
            $('#Email').val(result.Email);
            $('#Details').val(result.Details);
            $('#HOD').val(result.HOD);

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
            url: "/Company/DeleteDepartments/" + ID,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
             swal("Good job!", "Deleted Succesfully", "success")
                GetPageData();
            },
            error: function (result) {
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
    var empObj = {
        ID: $('#Id').val(),
        Name: $('#Name').val(),
        Phone: $('#phone').val(),
        Email: $('#Email').val(),
        HOD: $('#HOD').val(),
        Details: $('#Details').val()
    };
    $.ajax({
        url: "/Company/AddDepartments",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            swal({
                title: "Sweet!",
                text: "New Department Inserted Succesfully",
                imageUrl: 'thumbs-up.jpg'
            });
            GetPageData();
            $('#myModal').modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function clearTextBox() {
    $('#Id').val("");
    $('#Name').val("");
    $('#phone').val("");
    $('#Email').val("");
    $('#Details').val("");
    $('#HOD').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#Id').css('border-color', 'lightgrey');
    $('#phone').css('border-color', 'lightgrey');
    $('#Email').css('border-color', 'lightgrey');
    $('#Details').css('border-color', 'lightgrey');
    $('#HOD').css('border-color', 'lightgrey');
   
}
function Update() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var empObj = {
        Id: $('#Id').val(),
        Name: $('#Name').val(),
        phone: $('#phone').val(),
        Email: $('#Email').val(),
        Details: $('#Details').val(),
        HOD: $('#HOD').val(),
    };
    $.ajax({
        url: "/Company/UpdateDepartments",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",

        success: function (result) {
            swal({
                title: "Sweet!",
                text: "Department Updated Sucessfully",
                imageUrl: 'thumbs-up.jpg'
            });
            GetPageData(); debugger;
            $('#myModal').modal('hide');
            $('#Id').val("");
            $('#Name').val("");
            $('#phone').val("");
            $('#Email').val("");
            $('#Details').val("");
            $('#HOD').val("");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function validate() {
    var isValid = true;
    if ($('#Name').val().trim() == "") {
        $('#Name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Name').css('border-color', 'lightgrey');
    }
    if ($('#phone').val().trim() == "") {
        $('#phone').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#phone').css('border-color', 'lightgrey');
    }
    if ($('#Email').val().trim() == "") {
        $('#Email').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Email').css('border-color', 'lightgrey');
    }
    if ($('#Details').val().trim() == "") {
        $('#Details').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Details').css('border-color', 'lightgrey');
    }
    if ($('#HOD').val().trim() == "") {
        $('#HOD').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#HOD').css('border-color', 'lightgrey');
    }

    return isValid;
}