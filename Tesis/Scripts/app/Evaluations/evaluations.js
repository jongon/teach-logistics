$(document).ready(function () {
    $('#evaluations').DataTable({

    });
});

$('.quizButton').click(function () {
    var evaluation = $(this).data('evaluation');
    var href = $('#takeQuiz').attr('href');
    $('#takeQuiz').attr('href', href + '/' + evaluation);
});