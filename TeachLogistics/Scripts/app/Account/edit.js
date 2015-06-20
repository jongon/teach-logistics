function filterSections() {
    return {
        SemesterId: $("#SemesterId").val()
    };
}
$.validator.setDefaults({
    ignore: ""
});
$("#Password").removeAttr("data-val-required");
$("#ConfirmPassword").removeAttr("data-val-required");