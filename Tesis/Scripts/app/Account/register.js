function filterSections() {
    return {
        SemesterId: $("#SemesterId").val()
    };
}

$.validator.setDefaults({
    ignore: ""
});