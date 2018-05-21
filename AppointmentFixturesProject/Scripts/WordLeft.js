$('#spnCharLeft').css('display', 'none');
var maxLimit = 100;
$(document).ready(function () {
    $('#Details').keyup(function () {
        var lengthCount = this.value.length;
        if (lengthCount > maxLimit) {

            this.value = this.value.substring(0, maxLimit);
            var charactersLeft = maxLimit - lengthCount + 1;

        }
        else {
            var charactersLeft = maxLimit - lengthCount;
        }
        $('#spnCharLeft').css('display', 'block');
        $('#spnCharLeft').text(charactersLeft + ' Characters left');
    });
});