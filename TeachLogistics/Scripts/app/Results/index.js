$(document).ready(function () {
    selectTable();
});

$("#groups").change(function () {
    selectTable();
});

function selectTable() {
    hideTable();
    var groupId = $("#groups").children(':selected').attr('id');
    $("#" + groupId + ".groups").show();
}

function hideTable() {
    $('.groups').css('display', 'none');
}