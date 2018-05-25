/// <reference path="jquery-1.9.1.intellisense.js" />
//Load Data in Table when documents is ready
var globalCurrentPage;

$(document).ready(function () {

    GetPageData(1);
});
function GetPageData(pageNum, pageSize) {
    //After every trigger remove previous data and paging
    $(".tbody").empty();
    $("#paged").empty();
  
    $.getJSON("/ScheduleVIP/GetPaggedData", { pageNumber: pageNum, pageSize: pageSize }, function (response) {
        var html = "";
         
        for (var i = 0; i < response.Data.length; i++) {
            html = html + '<tr><td>' + (i+1) + '</td>';
            html = html + '<td>' + response.Data[i].Id + '</td><td>' + response.Data[i].Date + '</td>';
            
            html += '<td>' + response.Data[i].StartTime + '</td><td>' + response.Data[i].EndTime + '</td>';
            html += '<td>' + response.Data[i].IsAvailable + '</td>';
            //  html = html + '<td>' + response.Date[i].EndTime + '</td><td>' + response.Data[i].IsAvailable + '</td>';
            html += '<td><a href="#" class="btn btn-info" onclick="return getbyID(' + response.Data[i].Id + ')">Edit</a>    <a href="#" class="btn btn-danger" onclick="Delete(' + response.Data[i].Id + ')">Delete</a></td>';
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

function getbyID(EmpID) {
    $('#dateInvalidSummary').hide();
    $('#timeInvalidSummary').hide();
    $('#Date').css('border-color', 'lightgrey');
    $('#StartTime').css('border-color', 'lightgrey');
    $('#EndTime').css('border-color', 'lightgrey');
    $('#IsAvailable').css('border-color', 'lightgrey');
    $.ajax({
        url: "/ScheduleVIP/getbyID/" + EmpID,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            debugger;
            $('#AppointmentId').val(result.Id);
            $('#Date').val(result.Date);
            $('#StartTime').val(result.StartTime);
            $('#EndTime').val(result.EndTime);
            $('#IsAvailable').val(result.IsAvailable);
            $('#myModal2').modal('show')

            $("#IsAvailable option").filter(function () {
                return this.text == String(result.IsAvailable);
            }).attr('selected', true);

            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            swal("Oops", "We couldn't connect to the server!", "error");
        }
    });
    return false;
}

function Delete(ID) {
    var ans = confirm("Are you sure you want to delete this Record?");

    if (ans) {
        $.ajax({
            url: "/ScheduleVIP/Delete/" + ID,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                GetPageData(1);
                swal(" Deleted Successfully!", "Thank you!", "success")
            },
            error: function (errormessage) {
                swal("Oops", "We couldn't connect to the server!", "error");
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
        url: "/ScheduleVIP/Add",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            debugger;
            if (result == -1) {
                $('#dateInvalidSummary').show();
                $('#timeInvalidSummary').show();

            }
            else {

                GetPageData(1);

                $('#myModal2').modal('hide');
                //put this if modal gets closed but gray color fade still exists 

                $('body').removeClass('modal-open');
                $('.modal-backdrop').remove();
                //end

                sweetAlert
                         ({
                             title: "Inserted!",
                             text: "Your Data Added Successfylly.",
                             type: "success"
                         });
            }
        },
        error: function (errormessage) {
            swal("Oops", "We couldn't connect to the server!", "error");
        }
    });
}

function clearTextBox() {
    $('#btnUpdate').hide();
    $('#btnAdd').show();
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
    $.ajax({
        url: "/ScheduleVIP/Update",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            debugger;
            if (result == -1) {
                $('#dateInvalidSummary').show();
                $('#timeInvalidSummary').show();
            }
            else {
                //loadData();
                alert(globalCurrentPage);
                GetPageData(globalCurrentPage);
                $('#myModal2').modal('hide');
                $('#AppointmentId').val("");
                $('#Date').val("");
                $('#StartTime').val("");
                $('#EndTime').val("");
                $('#IsAvailable').val("");
                swal("Update Successful!", "Appointment Set", "success");
            }
        },
        error: function (errormessage) {
            swal("Oops", "We couldn't connect to the server!", "error");
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


function PaggingTemplate(totalPage, currentPage) {
    var template = "";
    var TotalPages = totalPage;
    var CurrentPage = currentPage;
    var PageNumberArray = Array();
    window.globalCurrentPage = CurrentPage;

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
    $('#selectedId').change(function () {8
        GetPageData(1, $(this).val());
    });
}

