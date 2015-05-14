$(document).ready(function () {
    $('#evaluations').DataTable({
        "order": [[0, "desc"]],
        "aoColumns": [
            null,
            { "sType": "date-uk" },
            { "sType": "date-uk" },
            null,
            null,
            null,
            null,
    ]});
});