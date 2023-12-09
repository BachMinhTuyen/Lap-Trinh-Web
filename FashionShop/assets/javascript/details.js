// Khi load xong nội dung trang web
document.addEventListener("DOMContentLoaded", function () {

    // Xử lý size
    var sizeOptions = document.querySelectorAll('.box-size');
    sizeOptions[0].classList.add('active');

    var sizeSelected = document.querySelector('.size-selected');
    sizeSelected.innerText = sizeOptions[0].innerText;

    // Xử lý color
    var colorOptions = document.querySelectorAll('.box-color');
    colorOptions[0].classList.add('active');

    var colorSelected = document.querySelector('.color-selected');
    colorSelected.innerText = colorOptions[0].innerText;

    // Gán giá trị ban đầu
    var newSize = sizeOptions[0].innerText;
    var newColor = colorOptions[0].innerText;

    // Lặp qua từng ô chọn size
    sizeOptions.forEach(function (sizeOption) {
        sizeOption.addEventListener('click', function () {

            newSize = sizeOption.innerText;

            sizeSelected.innerText = newSize;

            sizeOptions.forEach(function (option) {
                option.classList.remove('active');
            });

            sizeOption.classList.add('active');
        });
    });

   // Lặp qua từng ô chọn color
    colorOptions.forEach(function (colorOption) {

        colorOption.addEventListener('click', function () {

            newColor = colorOption.innerText;

            document.querySelector('.color-selected').innerText = newColor;

            colorOptions.forEach(function (option) {
                option.classList.remove('active');
            });

            colorOption.classList.add('active');
        });
    });

    //Sự kiện khi click vào nút "thêm vào giỏ hàng"
    document.querySelector('#add-to-cart').addEventListener('click', () => {
        var productID = document.querySelector('#product-ID span').innerText;
        var soLuong = document.getElementById('quantityInput').value;

        var addToCartLink = '/Cart/AddToCart?maSanPham=' + productID + '&tenMau=' + newColor + '&tenSize=' + newSize + '&soLuong=' + soLuong + '&stringURL=' + window.location.href;
        $("#add-to-cart").attr('href', addToCartLink)
    })

    //Sự kiện khi click vào nút "mua ngay"
    document.querySelector('#to-order').addEventListener('click', () => {
        var productID = document.querySelector('#product-ID span').innerText;
        var soLuong = document.getElementById('quantityInput').value;

        var toOrderLink = '/Cart/CheckoutWithOneProduct?maSanPham=' + productID + '&tenMau=' + newColor + '&tenSize=' + newSize + '&soLuong=' + soLuong;
        $("#to-order").attr('href', toOrderLink)
    })
});

// Hàm giảm số lượng
function decreaseQuantity() {
    var quantityInput = document.getElementById('quantityInput');
    var currentValue = parseInt(quantityInput.value, 10);
    if (currentValue > 1) {
        quantityInput.value = currentValue - 1;
        document.getElementById('quantity-selected').innerText = currentValue - 1;
    }
}

// Hàm tăng số lượng
function increaseQuantity() {
    var quantityInput = document.getElementById('quantityInput');
    var currentValue = parseInt(quantityInput.value, 10);

    quantityInput.value = currentValue + 1;
    document.getElementById('quantity-selected').innerText = currentValue + 1;
}