
var UpdateIt=function(Id)
    {
        alert(Id);
        $.ajax({
            type:"POST",
            url:"/VIP/AppointOrCancel",
            date: { ID: Id},
            success:function(result)
            {
                $('#Id').val(result.Id);
                $('#Date').val(result.Date);
                $('#StartTime').val(result.StartTime);
                $('#EndTime').val(result.EndTime);
                $('#IsCanceled').val(result.IsCanceled);
            }
    }


    function Update() {
        var empObj = {
            Id: $('#Id').val(),
            Date: $('#Date').val(),
            StartTime: $('#StartTime').val(),
            EndTime: $('#EndTime').val(),
            IsAvailable: $('#IsCanceled').val(),
        };
        $.ajax({
            url: "/VIP/Update",
            data: JSON.stringify(empObj),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                $('#Id').val("");
                $('#Date').val("");
                $('#StartTime').val("");
                $('#EndTime').val("");
                $('#IsCanceled').val("");
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }