/// <reference path="jquery-1.9.1.intellisense.js" />
var globalCurrentPage;
$(document).ready(function () {
    GetPageData(1);
    GetVip();
});

//Load Data function
function GetPageData(pageNum, pageSize) {
    $(".tbody").empty();
    $("#paged").empty();
    $.getJSON("/Company/GetPaggedDataa", { pageNumber: pageNum, pageSize: pageSize }, function (response) {
        var html = "";

        for (var i = 0; i < response.Data.length; i++) {
            html = html + "<tr>"
            html += '<td>' + response.Data[i].VipName + '</td>';
            html += '<td>' + response.Data[i].Users + '</td><td>' + response.Data[i].StartTime + '</td>';
            html += '<td>' + response.Data[i].EndTime + '</td>';
            html += '<td>' + response.Data[i].Date + '</td>';
            html += '<td><a href="#" class="btn btn-info" onclick="return getbyID(' + response.Data[i].ID + ')">Edit</a>    <a href="#" class="btn btn-danger" onclick="Delete(' + response.Data[i].ID + ')">Delete</a></td>';
            html += "</tr>";
        }

        $(".tbody").html(html);
        //var currentPage = response.currentPage;
        //alert(response.currentPage);
        //alert(response.totalPages);
        PaggingTemplate(response.TotalPages, response.CurrentPage);
    }
    );
}
//This is paging temlpate ,you should just copy paste
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
                GetPageData();
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
            GetPageData(1);
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
    $('#ID').val("");
    $('#VIPuser').val("");
    $('#Users').val("");
    $('#StartTime').val("");
    $('#EndTime').val("");
    $('#Date').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#VIPuser').css('border-color', 'lightgrey');
    $('#Users').css('border-color', 'lightgrey');
    $('#StartTime').css('border-color', 'lightgrey');
    $('#EndTime').css('border-color', 'lightgrey');
    $('#Date').css('border-color', 'lightgrey');
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
            GetPageData(); debugger;
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

    //if ($('#VIPuser').val().trim() == "") {
    //    $('#VIPuser').css('border-color', 'Red');
    //    isValid = false;
    //}
    //else {
    //    $('#VIPuser').css('border-color', 'lightgrey');
    //}
    if ($('#Users').val().trim() == "") {
        $('#Users').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Users').css('border-color', 'lightgrey');
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

function GetVip() {

   

    $.ajax({
        url: "/Company/GetVip",

        type: "GET",
        contentType: "json",
        dataType: "json",
        success: function (result) {

            var options = '';
            options += '<select class="form-control"><option value="Select">Select</option>';

            $.each(result, function (key, item) {
                options += '<option value="' + item.Id + '">' + item.FullName + '</option>';
            });
            options += "</select>"

            $('.select').html(options);

        },

        error: function (errormessage) {
            alert(errormessage.responseText);
            alert("ogesh");
        }
    })
}






