
$(document).ready(function () {
    $('#dropdownCompany').on('change', function () {
        var Id = $('#dropdownCompany option:selected').val();
        alert(Id);
        $.ajax({
            type: 'GET',
            data: { Id: Id },
            url: '@Url.Action("loadDepartment","User")',
            success: function (result) {
                alert(result);
                var s = '<option value="-1">Please select Department</option>';
                for (var i = 0; i < result.length; i++) {
                    s += '<option value="' + result[i].Id + ' ">' + result[i].Name + '</option>';
                }
                $('#dropdownDepartment').html(s);
            }
        });
    });

    $('#dropdownDepartment').on('change', function () {
        var Id = $('#dropdownDepartment option:selected').val();
        alert(Id);
        $.ajax({
            type: 'GET',
            data: { Id: Id },
            url: '@Url.Action("loadVIP","User")',
            success: function (result) {
                alert(result);
                var s = '<option value="-1">Please select VIP</option>';
                for (var i = 0; i < result.length; i++) {
                    s += '<option value="' + result[i].Id + ' ">' + result[i].FullName + '</option>';
                }
                $('#dropdownVIP').html(s);
            }
        });
    });

    $('#dropdownVIP').on('change', function () {
        var Id = $('#dropdownVIP option:selected').val();
        $.ajax({
            type: 'GET',
            data: { Id: Id },
            url: '@Url.Action("loadDate","User")',
            success: function (result) {
                alert(result + "diwas");
                var s = '<option value="-1">Please Select Date</option>';
                for (var i = 0; i < result.length; i++) {
                    s += '<option value="' + result[i].Id + ' ">' + result[i].Date + '</option>';
                }
                $('#dropdownDate').html(s);
            }
        });
    });


    $('#dropdownDate').on('change', function () {
        var Id = $('#dropdownDate option:selected').val();
        debugger;
        $.ajax({
            type: 'GET',
            data: { Id: Id },
            url: '@Url.Action("loadTime","User")',
            success: function (result) {
                var s = '<option value="-1">Please Select Date</option>';
                for (var i = 0; i < result.length; i++) {
                    s += '<option value="' + result[i].Id + ' ">' + result[i].StartTime + '--- ' + result[i].EndTime + '</option>';
                }
                $('#dropdownTime').html(s);
            }
        });
    });

    $('#dropdownTime').on('change', function () {
        var Id = $('#dropdownTime option:selected').val();
        debugger;
        $.ajax({
            type: 'GET',
            data: { Id: Id },
            url: '@Url.Action("loadFixedTime","User")',
            success: function (result) {
                var a = result.StartTime;
                var b = result.EndTime;
                var result = intervals(a, b);
                for (var i = 0; i < result.length; i++) {
                    var s = '<option value="-1">Please Select Interval</option>';
                    for (var i = 0; i < result.length; i++) {
                        s += '<option value="' + result[i] + ' ">' + result[i] + '   to   ' + result[i + 1] + '</option>';

                    }
                    $('#dropdownInterval').html(s);
                }
            }
        });
    });


    $('#dropdownInterval').on('change', function () {
        var Id = $('#dropdownInterval option:selected').text();
        debugger;
        $.ajax({
            type: 'GET',
            data: { Id: Id },
            url: '@Url.Action("SaveEndInterval","User")',
            success: function (result) {
                alert(Id);
            }
        });
    });


});

////

function intervals(startString, endString) {
    var start = moment(startString, 'HH:mm:ss a');
    var end = moment(endString, 'HH:mm:ss a');
    start.minutes(Math.ceil(start.minutes() / 15) * 15);
    var result = [];
    var current = moment(start);

    while (current <= end) {
        result.push(current.format('HH:mm:ss'));
        current.add(15, 'minutes');
    }

    return result;
}


