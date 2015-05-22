$(document).ready(function () {
    $('#form').validate().settings.ignore = "*";
    if (periodNumber == 1) {
        $('#instructions').modal('show');
    }

    $('.i-checks').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
    });

    $('.animation_select').click(function () {
        //$('#animation_box').removeAttr('class').attr('class', '');
        var animation = $(this).attr("data-animation");
        $('#animation').addClass('animated');
        $('#animation').addClass(animation);
        return false;
    });
});