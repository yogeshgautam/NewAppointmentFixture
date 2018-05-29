/// <reference path="../new/employee.js" />
$(document).ready(function () {
    
    loadData();
});

function GetPageData(pageNum, pageSize) {
    //After every trigger remove previous data and paging
    $(".tbody").empty();
    $("#paged").empty();
    $.getJSON("/User/GetPaggedData", { pageNumber: pageNum, pageSize: pageSize }, function (response) {
        var html = '';
        $.each(result, function (key, item) {
            html += '<tr>';
            html += '<td>' + item.Id + '</td>';

            var getdate = parseInt(item.Date.replace("/Date(", "").replace(")/", ""));
            var date = new Date(getdate).toLocaleDateString();
            html += '<td>' + date + '</td>';

            html += '<td>' + item.FromTime + '</td>';
            html += '<td>' + item.ToTime + '</td>';

            html += (item.IsCanceled == true) ? "<td><b>Canceled</b></td>" : "<td><b>Scheduled</b></td>";
           

            html += '<td>' + item.AppointmentTo + '</td>';
            html += '<td><a href="#" class="btn btn-info" onclick="return GetDetailsOfUser(' + item.Id + ')" >Details</a></td>'
            html += '</tr>'
        });

        $(".tbody").html(html);
        //var currentPage = response.currentPage;
        //alert(response.currentPage);
        //alert(response.totalPages);
        PaggingTemplate(response.TotalPages, response.CurrentPage);
    }
    );
}

function loadData()
{
    
    $.ajax({
        url: "/User/ViewAppointmentList",
        type:"GET",
        contentType: "json",
        dataType: "json",
     
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {     
                html += '<tr>';
                html += '<td>' + item.Id + '</td>';

                var getdate = parseInt(item.Date.replace("/Date(", "").replace(")/", ""));
                var date = new Date(getdate).toLocaleDateString();
                html += '<td>' + date + '</td>';
                
                html += '<td>' + item.FromTime + '</td>';
                html += '<td>' + item.ToTime + '</td>';
                
                html += (item.IsCanceled == true) ? "<td><b>Canceled</b></td>" : "<td><b>Scheduled</b></td>";
                //if (item.IsCanceled.val() == "true")
                //{
                //    html += '<td>' + Canceled + '</td>';

                //} else {
                //    html += '<td>' + Scheduled + '</td>';
                        
                //}
               
                html += '<td>' + item.AppointmentTo + '</td>';
                html += '<td><a href="#" class="btn btn-info" onclick="return GetDetailsOfUser(' + item.Id + ')" >Details</a></td>'
                html+='</tr>'
            });
            $('.tbody').html(html);
        },
        error:function(errormessage){
            alert(errormessage.responseText);
        }
    })
   
    
}

function GetDetailsOfUser(ID)
{
    $.ajax({
        url: "/User/ViewAppointmentById/" + ID,
        type: "GET",
        contentType: "json",
        dataType: "json",
        
        success: function (result) {
            
            
            var getdate = parseInt(result.Date.replace("/Date(", "").replace(")/", ""));
            var date = new Date(getdate).toLocaleDateString();
            $('#Date').text(date+"");
        
            $('#FromTime').text(result.FromTime+"");
            $('#ToTime').text(result.ToTime+"");
            $('#AppointmentTo').text( result.AppointmentTo+"");
            $('#CompanyName').text(result.Department.lstCompany.Name+"");
            $('#DepartmentName').text(result.Department.Name+"");
            $('#Status').text( (result.IsCanceled == true) ? "Canceled" : "Scheduled");
            $('#Details').text(result.Details+"");
            $('#myModal').modal('show');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    })

  
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
    $('#selectedId').change(function () {
        GetPageData(1, $(this).val());
    });
}

