$(function () {
    console.log("document_ready");
    $(document).on("click", ".edit-product-button", function () {
        console.log("you just clicked diddnt you" + $(this).val());
        var productID = $(this).val();
        $.ajax({
            type: 'POST', // was json type
            data: {
                "id": productID
            },
            url: '/product/ShowOneProductJSON',
            success: function (data) {
                console.log(data)
                $("#modal-input-id").val(data.id); //buradki idye dikakt et
                $("#modal-input-name").val(data.name);
                $("#modal-input-price").val(data.price);
                $("#modal-input-description").val(data.description);
            }
        });
    });

    
    function addToCart(productId) {
        $.ajax({
            type: 'POST',
            url: '/Cart/AddToCart',
            data: { productId: productId },
            success: function (response) {
                // Check if the addition to the cart was successful
                if (response.success) {
                    // Get the product details from the response
                    var product = response.product;

                    // Redirect to the details page
                    console.log('Redirecting to details page...');
                    window.location.href = '/product/showDetails/' + product.Id;
                } else {
                    // Handle the case where the addition to the cart failed
                    console.error('Error adding product to cart:', response.error);
                }
            },
            error: function (error) {
                // Handle the case where the AJAX request itself fails
                console.error('Error adding product to cart:', error);
            }
        });
    }


    
    
    

    $("#save-button").click(function () {
        var product = {
            "Id": $("#modal-input-id").val(),
            "Name": $("#modal-input-name").val(),
            "Price": $("#modal-input-price").val(),
            "Description": $("#modal-input-description").val(),
        };
        console.log("saved..");
        console.log(product);

        // save updated in databse using controller
        $.ajax({
            type: 'POST', //was json type
            data: product,
            url: '/product/ProcessEditReturnPartial',
            success: function (data) {
                console.log(data);
                $("#card-number-" + product.Id).html(data).hide().fadeIn(2000);
            }
        });
    });
});