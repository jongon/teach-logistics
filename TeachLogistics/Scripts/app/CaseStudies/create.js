
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
    this.ProductId = $('#ProductId').val();
    this.Demand = $('#Demand').val();
    this.StdDev = $('#Stddev').val();
    this.Price = $('#Price').val();
    this.SecurityStock = $('#SecurityStock').val();
    this.InitialStock = $('#InitialStock').val();
    this.FillTime = $("input:radio[name*= 'FillTimeOption']:checked").val();
    this.DeliveryTime = $("input:radio[name*= 'DeliveryTimeOption']:checked").val();
    this.PreparationTime = $("input:radio[name*= 'PreparationTimeOption']:checked").val();
}

var initialCharges = [];

function disableFormValidation() {
    $('#ProductId').rules('remove', 'required');
    $('#Demand').rules('remove', 'required');
    $('#Stddev').rules('remove', 'required');
    $('#Price').rules('remove', 'required');
    $('#SecurityStock').rules('remove', 'required');
    $('#InitialStock').rules('remove', 'required');
    $('#FillTimeOption').rules('remove', 'required');
    $('#DeliveryTimeOption').rules('remove', 'required');
    $('#PreparationTimeOption').rules('remove', 'required');
}

function enabledFormValidation() {
    $('#ProductId').rules('add', 'required');
    $('#Demand').rules('add', 'required');
    $('#Stddev').rules('add', 'required');
    $('#Price').rules('add', 'required');
    $('#SecurityStock').rules('add', 'required');
    $('#InitialStock').rules('add', 'required');
    $('#FillTimeOption').rules('add', 'required');
    $('#DeliveryTimeOption').rules('add', 'required');
    $('#PreparationTimeOption').rules('add', 'required');
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
        initialCharges.push(new InitialCharge());
        $("#ProductId option[value='" + $('#ProductId').val() + "']").remove();
        $('#ProductId').val('');
        $('#Demand').val('');
        $('#Stddev').val('');
        $('#Price').val('');
        $('#SecurityStock').val('');
        $('#InitialStock').val('');
        $('input[name=PreparationTimeOption').iCheck('uncheck');
        $('input[name=DeliveryTimeOption]').iCheck('uncheck');
        $('input[name=FillTimeOption]').iCheck('uncheck');
        $('#alert').show();
        $('#alert').delay(2000).fadeOut();
        $('.has-success').removeClass('has-success');
    }
    resizeJquerySteps();
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
                if (currentIndex == 4) {
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
            //Nombrar "Siguiente" el botón ya que aveces puede cambiar
            $("#form .actions a[href='#']").attr('href', '#next');
            $("#form .actions a[href='#next']").text('Siguiente');
            $('.has-success').removeClass('has-success');
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
            } else if ((currentIndex == 4 || currentIndex == 3) && selected === 'xml') {
                $(this).steps("previous");
            } else if (currentIndex == 4 && selected === 'form') {
                $("#form .actions li:eq('1')").removeAttr('aria-hidden');
                $("#form .actions li:eq('1')").removeAttr('aria-disabled');
                $("#form .actions li:eq('1')").attr('class', '');
                $("#form .actions li:eq('1')").attr('style', 'display: block;');
                $("#form .actions a[href='#next']").text('Agregar otro');
                $("#form .actions a[href='#next']").attr('onclick', 'addInitialCharge()');
                $("#form .actions a[href='#next']").attr('id', 'test');
                $("#form .actions a[href='#next']").attr('href', '#');
            }
            resizeJquerySteps();
        },
        onFinishing: function (event, currentIndex)
        {
            var form = $(this);
            // Disable validation on fields that are disabled.
            // At this point it's recommended to do an overall check (mean ignoring only disabled fields)
            form.validate().settings.ignore = ":disabled";

            var selected = ($("input:radio[name*= 'ChargeTypeName']:checked").val());
            if (initialCharges.length > 0 && $('#ProductId option').length <= 1 && selected === 'form') {
                $("#InitialCharges").val(JSON.stringify(initialCharges));
                form.validate().settings.ignore = "*";
                form.submit();
            }
            if (form.valid()) {
                return true;
            } else {
                resizeJquerySteps();
                return false;
            }
            //return form.valid();
        },
        onFinished: function (event, currentIndex)
        {
            var form = $(this);
            // Start validation; Prevent form submission if false
            var selected = ($("input:radio[name*= 'ChargeTypeName']:checked").val());
            initialCharges.push(new InitialCharge());
            $("#InitialCharges").val(JSON.stringify(initialCharges));
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