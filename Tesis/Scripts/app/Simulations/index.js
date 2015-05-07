$(document).ready(function () {
    $('#simulations').DataTable({

    });
});

$('.simulation').click(function(event) {
    var section = $(this).data("section");
    $('form #DisableId').val(section);
    $('form #EnableId').val(section);
});