
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
    this.productId = $('#ProductId').val();
    this.demand = $('#Demand').val();
    this.stdDev = $('#Stddev').val();
    this.price = $('#Price').val();
    this.preparationCost = $('#PreparationCost').val();
    this.annualMaintenanceCost = $('#AnnualMaintenanceCost').val();
    this.weeklyMaitenanceCost = $('#WeeklyMaintenanceCost').val();
    this.purchaseOrderRecharge = $('#PurchaseOrderRecharge').val();
    this.courierCharges = $('#CourierCharges').val();
    this.preparationTime = $('#PreparationTime').val();
    this.fillTime = $('#FillTime').val();
    this.deliveryTime = $('#DeliveryTime').val();
    this.securityStock = $('#SecurityStock').val();
    this.initialStock = $('#InitialStock').val();
}

var initialCharges = [];

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
    $('#FillTime').rules('remove', 'required');
    $('#DeliveryTime').rules('remove', 'required');
    $('#SecurityStock').rules('remove', 'required');
    $('#InitialStock').rules('remove', 'required');
}

function enabledFormValidation() {
    $('#ProductId').rules('add', 'required');
    $('#Demand').rules('add', 'required');
    $('#Stddev').rules('add', 'required');
    $('#Price').rules('add', 'required');
    $('#PreparationCost').rules('add', 'required');
    $('#AnnualMaintenanceCost').rules('add', 'required');
    $('#WeeklyMaintenanceCost').rules('add', 'required');
    $('#PurchaseOrderRecharge').rules('add', 'required');
    $('#CourierCharges').rules('add', 'required');
    $('#PreparationTime').rules('add', 'required');
    $('#FillTime').rules('add', 'required');
    $('#DeliveryTime').rules('add', 'required');
    $('#SecurityStock').rules('add', 'required');
    $('#InitialStock').rules('add', 'required');
}

function disableXmlValidation() {
    $('#XmlUpload').rules('remove', 'required');
}

function enabledXmlValidation() {
    $('#XmlUpload').rules('add', 'required');
}


function resizeJquerySteps() {
    $('.wizard .content').animate({ height: $('.body.current').outerHeight() }, "fast");
}

function addInitialCharge() {
    window.scrollTo(0, 0);
    if ($('#form').valid()) {
        $("#ProductId option[value='" + $('#ProductId').val() + "']").remove();
        initialCharges.push(new InitialCharge());
        $('#ProductId').val('');
        $('#Demand').val('');
        $('#Stddev').val('');
        $('#Price').val('');
        $('#PreparationCost').val('');
        $('#AnnualMaintenanceCost').val('');
        $('#WeeklyMaintenanceCost').val('');
        $('#PurchaseOrderRecharge').val('');
        $('#CourierCharges').val('');
        $('#PreparationTime').val('');
        $('#FillTime').val('');
        $('#DeliveryTime').val('');
        $('#SecurityStock').val('');
        $('#InitialStock').val('');
        $('#alert').show();
        $('#alert').delay(2000).fadeOut();
        $('.has-success').removeClass('has-success');
    }
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
                //al pulsar finalizar 
                var selected = ($("input:radio[name*= 'ChargeTypeName']:checked").val());
                if (currentIndex == 2) {
                    if (selected === 'xml') {
                        var form = $(this);
                        form.submit();
                    }
                }
            } else if (newIndex < currentIndex) {
                if (currentIndex == 3) {
                    alert('hola');
                    initialCharges = [];
                }
                return true;
            }
            // Start validation; Prevent going forward if false
            return form.valid();
        },
        onStepChanged: function (event, currentIndex, priorIndex)
        {
            //Ajustar el jquery steps a resolución de todo el formulario
            resizeJquerySteps();
            //Nombrar "Siguiente" el botón ya que aveces puede cambiar
            $("#form .actions a[href='#next']").text('Siguiente');

            var selected = ($("input:radio[name*= 'ChargeTypeName']:checked").val());
            if (currentIndex == 2) {
                if (selected === 'form') {
                    if (priorIndex > currentIndex) {
                        $(this).steps("previous");
                    } else {
                        disableXmlValidation();
                        enabledFormValidation();
                        $(this).steps("next");
                    }
                } else if (selected === 'xml') {
                    //recordar regresar los cambios
                    disableFormValidation();
                    enabledXmlValidation();
                    $("#form .actions a[href='#next']").text('Finalizar');
                }
            } else if (currentIndex == 3 && selected === 'xml') {
                $(this).steps("previous");
            } else if (currentIndex == 3 && selected === 'form') {
                $("#form .actions li:eq('1')").removeAttr('aria-hidden');
                $("#form .actions li:eq('1')").removeAttr('aria-disabled');
                $("#form .actions li:eq('1')").attr('class', '');
                $("#form .actions li:eq('1')").attr('style', 'display: block;');
                $("#form .actions a[href='#next']").text('Agregar otro');
                $("#form .actions a[href='#next']").attr('onclick', 'addInitialCharge()');
                $("#form .actions a[href='#next']").attr('id', 'test');
                $("#form .actions a[href='#next']").attr('href', '#');
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
            initialCharges.push(new InitialCharge());
            console.log(initialCharges);
            $("#initialCharges").val(JSON.stringify(initialCharges));
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
            alert("El archivo debe tener extensión XML");
        }
        input.value = "";
    }
    return res;
}