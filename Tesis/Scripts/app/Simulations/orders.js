MathJax.Hub.Config({
    "HTML-CSS": { linebreaks: { automatic: true, width: "60% container" } },
    SVG: { linebreaks: { automatic: true } },
    menuSettings: {
        context: "browser"
    }
});

$(document).ready(function () {

    MathJax.Hub.Config({
        "HTML-CSS": { linebreaks: { automatic: true } },
        SVG: { linebreaks: { automatic: true } }
    });

    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": false,
        "progressBar": true,
        "positionClass": "toast-top-right",
        "preventDuplicates": true,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };

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

$('#animation-group').click(function () {
    var members;
    for (var i = 0; i < group.length; i++) {
        if (i == 0 )
            members = group[i].FirstName + " " + group[i].LastName + "</br>";
        else
            members += group[i].FirstName + " " + group[i].LastName + "</br>";
    }
    toastr["info"](
        members,
        "Integrantes"
    );
});

$(".order").click(function () {
    var order = $(this).data('order');
    $('#OrdinaryOrderCost').text($('#' + order + ' .OrdinaryOrderCost').val());
    $('#CourierOrderCost').text($('#' + order + ' .CourierOrderCost').val());
    $('#FastOrderCost').text($('#' + order + ' .FastOrderCost').val());
    $('#FastCourierCost').text($('#' + order + ' .FastCourierCost').val());
    $('#OrdinaryOrderTime').text($('#' + order + ' .OrdinaryOrderTime').val());
    $('#CourierOrderTime').text($('#' + order + ' .CourierOrderTime').val());
    $('#FastOrderTime').text($('#' + order + ' .FastOrderTime').val());
    $('#FastCourierTime').text($('#' + order + ' .FastCourierTime').val());
});

$("#demands").click(function () {
    window.location.assign(demandsUrl);
});