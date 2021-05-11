$(function () {
    getProducts()
    function getProducts() {
        var id = $('#product_rows').data('id');
        var discontinued = $('#Discontinued').prop('checked') ? "" : "/discontinued/false";
        $.getJSON({
            url: "../../api/category/" + id + "/product" + discontinued,
            success: function (response, textStatus, jqXhr) {
                $('#product_rows').html("");
                for (var i = 0; i < response.length; i++){
                    var css = response[i].discontinued ? " class=\"discontinued\"" : "";
                    var row = "<tr" + css + " data-id=\"" + response[i].productId + "\" data-name=\"" + response[i].productName + "\" data-price=\"" + response[i].unitPrice + "\">"
                        // + "<a asp-action='SingleProduct' asp-route-id=\"" + response[i].productId + "\">"
                        + "<td>" + response[i].productName + "</td>"
                        + "<td class=\"text-right\">$" + response[i].unitPrice.toFixed(2) + "</td>"
                        + "<td class=\"text-right\">" + response[i].unitsInStock + "</td>"
                        // + "</a>"
                        + "</tr>";

                    $('#product_rows').append(row);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                // log the error to the console
                console.log("The following error occured: " + textStatus, errorThrown);
            }
        });
    }
    $('#CategoryId').on('change', function(){
        $('#product_rows').data('id', $(this).val());
        getProducts();
    });
    $('#Discontinued').on('change', function(){
        getProducts();
    });

    // delegated event listener
    $('#product_rows').on('click', 'tr', function(){
        // make sure a customer is logged in
        // if ($('#User').data('customer').toLowerCase() == "true"){
        //     $('#ProductId').html($(this).data('id'));
        //     $('#ProductName').html($(this).data('name'));
        //     $('#UnitPrice').html($(this).data('price').toFixed(2));
        //     // calculate and display total in modal
        //     $('#Quantity').change();
        //     $('#cartModal').modal();
        // } else {
        //     toast("Access Denied", "You must be signed in as a customer to access the cart.");
        // }
        var id = $(this).data('id');
        window.location.href = "/Product/singleproduct/" + id;

    });



    
});
