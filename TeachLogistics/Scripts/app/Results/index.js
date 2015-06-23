$(document).ready(function () {
    selectTable(GroupId);
});

$("#groups").change(function () {
    selectTable("");
});

function selectTable(GroupId) {
    hideTable();
    if (GroupId == "") {
        var groupId = $("#groups").children(':selected').val();
    }
    else {
        var groupId = GroupId;
        $('#groups').val(groupId);
    }
    $("#" + groupId + ".groups").show();
}

function hideTable() {
    $('.groups').css('display', 'none');
}