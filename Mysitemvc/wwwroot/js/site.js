$(function () {
    console.log("document_ready");
    $(document).on("click", ".edit-product-button", function ()
    {
        console.log("you just clicked diddnt you" + $(this).val());
        var productID = $(this).val();
        $.ajax({
            type: 'Json',
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
        })
    })
    $("#save-button").click(function () {
        var product = {
            "Id": $("#modal-input-id").val(),
            "Name": $("#modal-input-name").val(),
            "Price": $("#modal-input-price").val(),
            "Description": $("#modal-input-description").val(),
        };
        console.log("saved..");
        console.log(Product);

        // save updated in databse using controller
        $.ajax({
            type: 'json',
            data: Product,
            url: '/product/ProcessEditReturnPartial',
            success: function (data) {
                console.log(data);
                $("#card-number-" + Product.Id).html(data).hide().fadeIn(2000);
            }
        })
    })
});