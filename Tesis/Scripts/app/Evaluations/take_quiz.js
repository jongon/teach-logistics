$(document).ready(function () {
    $('.i-checks').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
    });
});

$("#countdown")
.countdown(dateTime, function (event) {
    $(this).text(
        event.strftime('%H:%M:%S')
    )
    .on('finish.countdown', function () {
        if (!isFinished) {
            //Deshabilitar la validation
            $('#form').validate().settings.ignore = "*";
            $('#RunoutTime').val(true);
            $('#form').submit();
        }
    })
});