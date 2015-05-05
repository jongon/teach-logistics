function filterSections() {
    return {
        SemesterId: $("#SemesterId").val()
    };
}

$.validator.setDefaults({
    ignore: ""
});

$(document).ready(function () {
    $('.i-checks').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
    });
});

function disableFormValidation() {
    $('#SemesterId').rules('remove', 'required');
    $('#SectionId').rules('remove', 'required');
}

function enableFormValidation() {
    $('#SemesterId').rules('add', 'required');
    $('#SectionId').rules('add', 'required');
}

$("input:radio").on('ifChecked', function (event) {
    if ($("input:radio:checked").val() === "Administrador") {
        $('#SemesterId').rules('remove', 'required');
        $('#SectionId').rules('remove', 'required');
        $("#SemesterId").data("kendoDropDownList").enable(false);
        $("#SectionId").data("kendoDropDownList").enable(false);
    } else {
        $('#SemesterId').rules('add', 'required');
        $('#SectionId').rules('add', 'required');
        $("#SemesterId").data("kendoDropDownList").enable(true);
    }
});
