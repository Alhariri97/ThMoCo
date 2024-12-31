$(document).ready(function () {
    $(".add-to-cart-form").on("submit", function (e) {
        e.preventDefault(); // Prevent the default form submission

        var form = $(this);
        var actionUrl = form.attr("action");
        var formData = form.serialize();

        $.ajax({
            type: "POST",
            url: actionUrl,
            data: formData,
            success: function (response) {
                // Optional: Update UI, e.g., show a success message
                alert("Product added to cart!");
            },
            error: function () {
                alert("An error occurred while adding the product to the cart.");
            }
        });
    });
});
