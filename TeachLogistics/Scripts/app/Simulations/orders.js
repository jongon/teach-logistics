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

$("#results").click(function () {
    window.location.assign(resultsUrl);
});

$('input:radio').on('ifChecked', function () {
    calculateTimeAndCost(this, null);
});

$('.quantity-order').on('keyup change', function () {
    if ($(this).val() != 0 && !isNaN($(this).val())) {
        calculateTimeAndCost(null, this);
    } else {
        timeAndCostInZero(this);
    }
});

function calculateTimeAndCost(selected, quantity) {
    var price = 0;
    var delivery = 0;
    var total_cost = 0;
    var quantity;
    if (selected == null) {
        price = $(quantity).parents('.order-panel').first().children('.costs').children('.Price').val();
        delivery = $(quantity).parents('.order-panel').first().children('p').children('.delivery');
        total_cost = $(quantity).parents('.order-panel').first().children('p').children('.total-cost');
        selected = $(quantity).parents('.order-panel').find("input:radio[name$='OrderMethodOption']:checked").first();
        selectedValue = $(selected).val();
        if ($(quantity).val() == 0 || isNaN($(quantity).val()) || selected === undefined) {
            timeAndCostInZero(quantity);
            return;
        }
        quantity = $(quantity).val();
    } else if (quantity == null) {
        price = $(selected).parents('.order-panel').first().children('.costs').children('.Price').val();
        delivery = $(selected).parents('.order-panel').first().children('p').children('.delivery');
        total_cost = $(selected).parents('.order-panel').first().children('p').children('.total-cost');
        quantity = $(selected).parents('.order-panel').find(".quantity-order").first();
        if ($(quantity).val() == 0 || isNaN($(quantity).val()) || $(selected).val() === undefined) {
            timeAndCostInZero(quantity);
            return;
        }
        quantity = $(quantity).val();
        selectedValue = $(selected).val();
    }

    var deliveryTime;
    var cost;
    if (selectedValue === "Normal") {
        var ordinaryOrderCost = $(selected).parents('.order-panel').first().children('.costs').children('.OrdinaryOrderCost').val();
        var ordinaryOrderTime = $(selected).parents('.order-panel').first().children('.costs').children('.OrdinaryOrderTime').val();
        $(delivery).text(Number(periodNumber) + Number(ordinaryOrderTime));
        if (Number(ordinaryOrderCost) == 0)
            $(total_cost).text((Number(price)) * (Number)(quantity));
        else
            $(total_cost).text((Number(price) * Number(quantity)) + ordinaryOrderCost);
    } else if (selectedValue === "Fast") {
        var fastOrderCost = $(selected).parents('.order-panel').first().children('.costs').children('.FastOrderCost').val();
        var fastOrderTime = $(selected).parents('.order-panel').first().children('.costs').children('.FastOrderTime').val();
        $(delivery).text( Number(periodNumber) + Number(fastOrderTime));
        $(total_cost).text( (Number(price) * Number(quantity)) + Number(fastOrderCost));
    } else if (selectedValue === "Courier") {
        var courierOrderCost = $(selected).parents('.order-panel').first().children('.costs').children('.CourierOrderCost').val();
        var courierOrderTime = $(selected).parents('.order-panel').first().children('.costs').children('.CourierOrderTime').val();
        deliveryTime = Number(periodNumber) + Number(courierOrderTime);
        cost = (parseFloat(courierOrderCost) + Number(price)) * Number(quantity);
        $(delivery).text( deliveryTime );
        $(total_cost).text(parseFloat(cost).toFixed(2));
    } else if (selectedValue === "FastCourier") {
        var courierOrderCost = $(selected).parents('.order-panel').first().children('.costs').children('.CourierOrderCost').val();
        var fastCourierCost = $(selected).parents('.order-panel').first().children('.costs').children('.FastCourierCost').val();
        var fastCourierTime = $(selected).parents('.order-panel').first().children('.costs').children('.FastCourierTime').val();
        console.log(courierOrderCost);
        deliveryTime = Number(periodNumber) + Number(fastCourierTime);
        cost = ((parseFloat(courierOrderCost) + Number(price)) * Number(quantity)) + Number(fastCourierCost);
        $(delivery).text( deliveryTime );
        $(total_cost).text( parseFloat(cost).toFixed(2));
    }
}

function timeAndCostInZero(element) {
    var delivery = $(element).parents('.order-panel').first().children('p').children('.delivery');
    var total_cost = $(element).parents('.order-panel').first().children('p').children('.total-cost');
    $(delivery).text('');
    $(total_cost).text('0');
}