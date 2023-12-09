// Hàm giảm số lượng
function decreaseQuantity(inputId) {
    var quantityInput = document.getElementById(inputId);
    var currentValue = parseInt(quantityInput.value, 10);
    if (currentValue > 1) {
        quantityInput.value = currentValue - 1;
    }
}

// Hàm tăng số lượng
function increaseQuantity(inputId) {
    var quantityInput = document.getElementById(inputId);
    var currentValue = parseInt(quantityInput.value, 10);
    quantityInput.value = currentValue + 1;
}

function updateCartItem(cartItem , quantity) {
    var id = cartItem;
    var soLuong = parseInt(document.getElementById(quantity).value, 10);

    // Gọi action cập nhật thông tin (sử dụng Ajax)
    $.ajax({
        url: '/Cart/UpdateCart',
        type: 'POST',
        data: { id: id, soLuong: soLuong },
        success: function (result) {
            location.reload();
            console.log("Cập nhập giỏ hàng thành công!");
        },
        error: function () {
            console.log("Lỗi cập nhập giỏ hàng!");
        }
    });
}