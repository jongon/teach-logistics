$(document).ready(function () {
    if (periodNumber == 1) {
        $('#instructions').modal('show');
    }

    $('.i-checks').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
    });
});