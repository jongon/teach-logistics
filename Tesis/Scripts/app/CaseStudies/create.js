//El estilo de radio button
$('.i-checks').iCheck({
    checkboxClass: 'icheckbox_square-green',
    radioClass: 'iradio_square-green',
});

//Selects de KendoUI
function filterSections() {
    return {
        SemesterId: $("#SemesterId").val()
    };
}

//hack para mejorar la validación de los select
$.validator.setDefaults({
    ignore: ""
});

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
    $('.wizard .content').animate({ height: $('.body.current').outerHeight() }, "fast");
}

//Configuración de jQuery steps
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
        var form = $(this);
        form.validate().settings.ignore = ":disabled,:hidden";

        // Always allow going backward even if the current step contains invalid fields!
        if (currentIndex > newIndex)
        {
            return true;
        }

        if (currentIndex < newIndex) {
            // To remove error styles
            $(".body:eq(" + newIndex + ") label.error", form).remove();
            $(".body:eq(" + newIndex + ") .error", form).removeClass("error");
        }

        // Disable validation on fields that are disabled or hidden.
        //if (currentIndex == 0 && form.valid()) {
        //    var sectionId = $("#sectionId").val();

        //}

        if (currentIndex == 2) {
            var selected = ($("input:radio[name*= 'ChargeTypeName']:checked").val());
            if (selected == 'xml') {
                //Cargar el form
            }
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
            var selected = ($("input:radio[name*= 'ChargeTypeName']:checked").val());
            if (priorIndex > currentIndex && selected === "form") {
                $(this).steps("previous");
            } else if (selected === "form") {
                $(this).steps("next");
            }
        } else if (currentIndex == 3) {
            var selected = ($("input:radio[name*= 'ChargeTypeName']:checked").val());
            if (selected == "xml") {
                $(this).steps("previous");
            } else {
                $("#form .actions li[class='disabled']").css("display", "block");
                $("#form .actions li[class='disabled']").removeClass('disabled');
                $("#form .actions a[href='#next']").text('Siguiente');
            }
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

//Validación si el archivo es o no de extensión xml
function isXml(input) {
    var value = input.value;
    var str = value.substr(value.lastIndexOf('.')).toLowerCase();
    var res = str == '.xml';
    if (!res) {
        if (str !== '') {
            alert("El archivo debe tener XML");
        }
        input.value = "";
    }
    return res;
}