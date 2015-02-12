function filterSections() {
    return {
        SemesterId: $("#SemesterId").val()
    };
}

$.validator.setDefaults({
    ignore: ""
});

function onAdditionalData() {
    return {
        UserName: $("#UserAutoComplete").val(),
        SectionId: $("#SectionId").val()
    };
}

function onUserSelect(e) {
    var dataItem = this.dataItem(e.item.index());
    var UserId = dataItem.Id;
    var UserName = dataItem.UserName;
    var bool = true;

    $("#Users > option").each(function () {
        if (this.value == UserId) {
            bool = false;
            return false;
        }
    });

    if (bool == true) {
        $('#Users').append($('<option/>', {
            value: UserId,
            text: UserName
        }));
    }
    return false;
}

function onUserClose() {
    $('#UserAutoComplete').val('');
}

function onSectionChange() {
    var SectionId = $("#SectionId").val();
    if (SectionId) {
        $("#UserAutoComplete").prop('disabled', false);
    } else {
        $("#UserAutoComplete").prop('disabled', true);
    }
}

function onSemesterChange() {
    var SemesterId = $("#SemesterId").val();
    if (!SemesterId) {
        $("#UserAutoComplete").prop('disabled', true);
    }
}

function onFiltering() {
    var SectionId = $("#SectionId").val();
    if (!SectionId) {
        $("#UserAutoComplete").prop('disabled', true);
    }
}

$("#deleteUser").click(function (event) {
    $('#Users option:selected').each(function () {
        $(this).remove();
    });
    return false;
});

$('#Users option:selected').each(function () {
    $(this).remove();
});

$(document).ready(function () {
    onSectionChange();
})