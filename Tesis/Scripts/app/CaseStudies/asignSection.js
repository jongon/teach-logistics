$.validator.setDefaults({
    ignore: ""
});

function filterSections() {
    return {
        SemesterId: $("#SemesterId").val()
    };
}

function onSemesterChange() {
    var SemesterId = $("#SemesterId").val();
}