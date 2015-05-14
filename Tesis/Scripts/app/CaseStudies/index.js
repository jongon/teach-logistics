$(document).ready(function () {
    $('#caseStudy').DataTable({
        "order": [[0, "desc"]],
        "aoColumns": [
            null,
            { "sType": "date-uk" },
            null,
            null,
            null,
        ]
    });
});