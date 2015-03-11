
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

function disableFormValidation() {
    $('#ProductId').rules('remove', 'required');
    $('#Demand').rules('remove', 'required');
    $('#Stddev').rules('remove', 'required');
    $('#Price').rules('remove', 'required');
    $('#PreparationCost').rules('remove', 'required');
    $('#AnnualMaintenanceCost').rules('remove', 'required');
    $('#WeeklyMaintenanceCost').rules('remove', 'required');
    $('#PurchaseOrderRecharge').rules('remove', 'required');
    $('#CourierCharges').rules('remove', 'required');
    $('#PreparationTime').rules('remove', 'required');
    $('#PreparationTime').rules('remove', 'required');
    $('#DeliveryTime').rules('remove', 'required');
    $('#SecurityStock').rules('remove', 'required');
    $('#InitialStock').rules('remove', 'required');
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

$(document).ready(function () {
    $("#form").steps({
        labels: stepsSettings,
        bodyTag: "fieldset",
        onStepChanging: function (event, currentIndex, newIndex)
        {
            var form = $(this);
            form.validate().settings.ignore = ":disabled,:hidden";

            //Al pulsar NEXT
            if (currentIndex < newIndex) {
                // To remove error styles
                $(".body:eq(" + newIndex + ") label.error", form).remove();
                $(".body:eq(" + newIndex + ") .error", form).removeClass("error");
                if (currentIndex == 2) {
                    //Deshabilitar la validación de los campos de formulario
                    disableFormValidation();
                }
            } else if (newIndex < currentIndex) {
                if (newIndex == 0 || newIndex == 1) {
                    return true;
                }
            }

            // Disable validation on fields that are disabled or hidden.
            //if (currentIndex == 0 && form.valid()) {
            //    var sectionId = $("#sectionId").val();

            //}
            // Start validation; Prevent going forward if false
            return form.valid();
        },
        onStepChanged: function (event, currentIndex, priorIndex)
        {
            //Ajustar el jquery steps a resolución de todo el formulario
            resizeJquerySteps();
            $("#form .actions a[href='#next']").text('Siguiente');

            if (currentIndex == 2) {
                var selected = ($("input:radio[name*= 'ChargeTypeName']:checked").val());
                if (selected === 'form') {
                    $('#XmlUpload').rules('remove', 'required');
                    $(this).steps("next");
                } else if (selected == 'xml') {
                    //Cambiar el atributo del botón a submit
                    //recordar regresar los cambios
                    //$("#form .actions a[href='#next']").attr('type', 'submit');
                    //$("#form .actions a[href='#next']").text('Finalizar');
                    $("#form .actions a[href='#next']").replaceWith(
                        '<input class="btn btn-primary btn-xs wizard-btn" type ="submit" value="Finalizar"/>');
                    $('#XmlUpload').rules('add', 'required');
                }
            }

            //// Suppress (skip) "Warning" step if the user is old enough.
            //if (currentIndex === 2) {
            //    
            //    var selected = ($("input:radio[name*= 'ChargeTypeName']:checked").val());
            //    if (priorIndex > currentIndex && selected === "form") {
            //        $(this).steps("previous");
            //    } else if (selected === "form") {
            //        $(this).steps("next");
            //    }
            //} else if (currentIndex == 3) {
            //    var selected = ($("input:radio[name*= 'ChargeTypeName']:checked").val());
            //    if (selected == "xml") {
            //        $(this).steps("previous");
            //    } else {
            //        $("#form .actions li[class='disabled']").css("display", "block");
            //        $("#form .actions li[class='disabled']").removeClass('disabled');
            //        $("#form .actions a[href='#next']").text('Siguiente');
            //    }
            //} else {
            //    
            //}
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
        }
    });

    //El estilo de radio button
    $('.i-checks').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
    });
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