const ProductApp = {
    getSelectedValue: (name) => {
        const selected = document.querySelector(`input[name="${name}"]:checked`);
        return selected ? selected.value : null;
    },
    getPurchaseData: () => {
        const config = document.querySelector('#product-config');
        const color = ProductApp.getSelectedValue('color');
        const size = ProductApp.getSelectedValue('size');
        const quantity = parseInt(document.getElementById('quantityInput').value);

        if (!color || !size) {
            showToast.warning("Vui lòng chọn đầy đủ Màu sắc và Kích thước!");
            return null;
        }

        return {
            maSanPham: config.dataset.prodId,
            tenMau: color,
            tenSize: size,
            soLuong: quantity
        };
    },
    changeQuantity: (step) => {
        const input = document.getElementById('quantityInput');
        let newValue = parseInt(input.value) + step;

        if (newValue >= 1) {
            input.value = newValue;
        }
    },
    processOrder: async (actionType) => {
        const data = ProductApp.getPurchaseData();
        if (!data) return;

        const url = actionType === 'BUY_NOW' ? '/Cart/BuyNow' : '/Cart/AddToCart';

        try {
            const response = await fetch(url, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(data)
            });

            const result = await response.json();

            if (result.success) {
                if (actionType === 'BUY_NOW') {
                    // Nếu là Mua ngay -> Chuyển hướng sang trang thanh toán
                    window.location.href = result.redirectUrl;
                } else {
                    showToast.success(result.message);
                    const cartBadge = document.querySelector('.cart-badge');
                    const cartPriceDisplay = document.querySelector('.cart-price-display');
                    if (cartBadge) cartBadge.innerText = result.totalQuantity;
                    if (cartPriceDisplay) cartPriceDisplay.innerText = result.totalPrice;
                }
            } else {
                showToast.error(result.message);
            }
        } catch (error) {
            console.error("Lỗi hệ thống:", error);
        }
    }
}

document.addEventListener('DOMContentLoaded', () => {

    document.querySelectorAll('input[name="color"]').forEach(input => {
        input.addEventListener('change', (e) => {
            document.getElementById('selected-color').innerText = e.target.value;
        });
    });
    document.querySelectorAll('input[name="size"]').forEach(input => {
        input.addEventListener('change', (e) => {
            document.getElementById('selected-size').innerText = e.target.value;
        });
    });

    document.querySelector('#add-to-cart').addEventListener('click', () => {
        const data = ProductApp.getPurchaseData();
        if (data) {
            ProductApp.processOrder('ADD_TO_CART');
        }
    });

    document.querySelector('#to-order').addEventListener('click', () => {
        const data = ProductApp.getPurchaseData();
        if (data) {
            ProductApp.processOrder('BUY_NOW');
        }
    });
})

window.changeQty = ProductApp.changeQuantity;