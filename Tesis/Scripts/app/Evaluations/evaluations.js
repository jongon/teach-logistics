$(document).ready(function () {
    $('#evaluations').DataTable({
        "order": [[0, "desc"]],
        "aoColumns": [
            null,
            null,
            { "sType": "date-uk" },
            null,
            null,
            null,
            { "sType": "date-uk" },
            null,
        ]
    });
});

$('.quizButton').click(function () {
    var evaluation = $(this).data('evaluation');
    var href = $('#takeQuiz').attr('href');
    $('#takeQuiz').attr('href', href + '/' + evaluation);
});