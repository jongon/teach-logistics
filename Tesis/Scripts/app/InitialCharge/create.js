//Definición de clase de carga Inicial
function InitialCharge() {
    this.productId;
    this.demand;
    this.stdDev;
    this.price;
    this.preparationCost;
    this.annualMaintenanceCost;
    this.preparationTime;
    this.fillTime;
    this.deliveryTime;
    this.securityStock;
    this.initialStock;
}

function resizeJquerySteps() {
    $('.wizard .content').animate({ height: $('.body.current').outerHeight() }, "slow");
}

$(document).ready(function () {

    var stepsSettings = {
        current: "paso actual:",
        pagination: "Paginación",
        finish: "Finalizar",
        next: "Siguiente",
        previous: "Volver",
        loading: "Cargando...",
        cancel: "Cancelar"
    };

    $("#form").steps({
        labels: stepsSettings,
        bodyTag: "fieldset",
        onStepChanging: function (event, currentIndex, newIndex)
        {
            // Always allow going backward even if the current step contains invalid fields!
            if (currentIndex > newIndex)
            {
                return true;
            }

            var form = $(this);

            // Clean up if user went backward before
            if (currentIndex < newIndex)
            {
                // To remove error styles
                $(".body:eq(" + newIndex + ") label.error", form).remove();
                $(".body:eq(" + newIndex + ") .error", form).removeClass("error");
            }

            // Disable validation on fields that are disabled or hidden.
            form.validate().settings.ignore = ":disabled,:hidden";

            if (currentIndex == 0 && form.valid()) {
                var sectionId = $("#sectionId").val();

            }

            // Start validation; Prevent going forward if false
            return form.valid();
        },
        onStepChanged: function (event, currentIndex, priorIndex)
        {
            resizeJquerySteps();
            // Suppress (skip) "Warning" step if the user is old enough.
            if (currentIndex === 2) {
                $("#form .actions a[href='#next']").text('Finalizar');
                var selected = ($("input:radio[name*= 'InitialChargeType']:checked").val());
                if (selected === "xml") {
                    //Cargar el formulario
                } else if (priorIndex > currentIndex && selected === "charge") {
                    $(this).steps("previous");
                } else if (selected === "charge") {
                    $(this).steps("next");
                }
            } else if (currentIndex == 3) {
                $("#form .actions li[class='disabled']").css("display", "block");
                $("#form .actions li[class='disabled']").removeClass('disabled');
                $("#form .actions a[href='#next']").text('Siguiente');
            } else {
                $("#form .actions a[href='#next']").text('Siguiente');
            }
        },
        onFinishing: function (event, currentIndex)
        {
            var form = $(this);

            // Disable validation on fields that are disabled.
            // At this point it's recommended to do an overall check (mean ignoring only disabled fields)
            form.validate().settings.ignore = ":disabled";

            // Start validation; Prevent form submission if false
            return form.valid();
        },
        onFinished: function (event, currentIndex)
        {
            var form = $(this);

            // Submit form input
            form.submit();
        }
    }).validate({
        errorPlacement: function (error, element)
        {
            element.before(error);
        },
        rules: {
            confirm: {
                equalTo: "#password"
            }
        }
    });

    $('.i-checks').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
    });

    // Validación del XML
    jQuery.validator.setDefaults({
        debug: true,
        success: "valid"
    });
    $("#myform").validate({
        rules: {
            field: {
                required: true,
                accept: "audio/*"
            }
        }
    });
});

function isXml(input) {
    var value = input.value;
    var res = value.substr(value.lastIndexOf('.')).toLowerCase() == '.xml';
    if (!res) {
        input.value = "";
    }
    return res;
}