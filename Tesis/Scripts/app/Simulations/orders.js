$(document).ready(function () {
    $('#form').validate().settings.ignore = "*";

    if (periodNumber == 1) {
        $('#instructions').modal('show');
    }

    $('.i-checks').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
    });

    $('#instructions').on('hidden.bs.modal', function (e) {
        var animation = $(this).attr("data-animation");
        $('#animation-case-study').addClass('animated');
        $('#animation-case-study').addClass(animation);
        $('#animation-case-study').popover('show');
        setTimeout(function () {
            $('#animation-case-study').popover('destroy');
            animateGroup(animation);
        }, 2000);
        return false;
    });
});

function animateGroup() {
    $('#animation-group').popover('show');
    setTimeout(function () {
        $('#animation-group').popover('destroy');
    }, 2000);
}

