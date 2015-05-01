$(document).ready(function () {
    $('#questions').DataTable({});
    $('input:checkbox').each(function () {
        for (var i = 0; i < questionIds.length; i++) {
            if (questionIds[i] == $(this).val()) {
                $(this).attr('checked', true);
            }
        }
    });
});

