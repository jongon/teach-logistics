function productViewModel() {
    this.number = $('#Number').val();
    this.name = $('#Name').val();
    this.city = $('#City').val();
    this.distance = $('#Distance').val();
}

var products = [];

$('#next').click(function () {
    if ($('#form').valid()) {
        products.push(new productViewModel());
        $('#Number').val('');
        $('#Name').val('');
        $('#City').val('');
        $('#Distance').val('');
        $('#alert').show();
        $('#alert').delay(3000).fadeOut();
    }
})

$('#form').submit(function () {
    products.push(new productViewModel());
    $("#products").val(JSON.stringify(products));
});