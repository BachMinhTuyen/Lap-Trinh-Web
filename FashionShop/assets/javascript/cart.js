const CartApp = {
    changeQty: (inputId, cartItemId, step) => {
        const input = document.getElementById(inputId);
        let newValue = parseInt(input.value) + step;

        if (newValue >= 1) {
            input.value = newValue;
            CartApp.syncWithServer(cartItemId, newValue);
        }
    },

    syncWithServer: (id, soLuong) => {
        $.ajax({
            url: '/Cart/UpdateCart',
            type: 'POST',
            data: { id: id, soLuong: soLuong },
            success: function (res) {
                if (res.success) {
                    location.reload();
                }
            },
            error: () => console.error("Lỗi cập nhật giỏ hàng")
        });
    },

    removeItem: (id) => {
        // Sử dụng confirm nhẹ nhàng hoặc SweetAlert2
        if (!confirm("Bạn muốn bỏ sản phẩm này khỏi giỏ hàng?")) return;

        $.ajax({
            url: '/Cart/DeleteCartItem',
            type: 'POST',
            data: { id: id },
            success: function (res) {
                if (res.success) {
                    $(`#cart-item-${id}`).slideUp(300, function () {
                        $(this).remove();

                        if (res.cartCount === 0) {
                            location.href = '/gio-hang';
                        } else {
                            if (res.totalAmount) {
                                //$('#total-cart-price').text(res.totalAmount.toLocaleString('vi-VN') + 'đ');
                            }
                            //$('.cart-badge').text(res.cartCount);
                        }
                    });
                }
            },
            error: () => alert("Không thể xóa sản phẩm lúc này. Vui lòng thử lại!")
        });
    },

    proceedToCheckout: () => {
        window.location.href = '/thanh-toan-don-hang';
    }
};