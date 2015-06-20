$(document).ready(function () {
    // activate Nestable for list 1
    $('#nestable').nestable({
        group: 0
    });
    $('#nestable').nestable('collapseAll');
});

$(".dd-nodrag").on("mousedown", function (event) { // mousedown prevent nestable click
    event.preventDefault();
    return false;
});