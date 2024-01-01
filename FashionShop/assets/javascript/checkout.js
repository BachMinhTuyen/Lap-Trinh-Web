// Get Provinces API
var provincesSelect = document.querySelector('#select-province-city');

// Lấy tên tỉnh/ thành phố
var getProvincesApi = 'https://provinces.open-api.vn/api/';

$.ajax({
    url: getProvincesApi,
    type: 'GET',
    success: function (res) {
        for (var i = 0; i < res.length; i++) {
            var provinceOption = document.createElement('option');
            provinceOption.value = res[i].code;
            provinceOption.text = res[i].name;

            provincesSelect.add(provinceOption);
        }
        console.log('Đọc API Provinces thành công!')
    },
    error: function (error) {
        console.log('Đã có lỗi khi đọc API:', error);
    }
});



//Lấy tên quận/ huyện của tỉnh/ thành phố vừa chọn
var getDistrictsApi = 'https://provinces.open-api.vn/api/d/';

$.ajax({
    url: getDistrictsApi,
    type: 'GET',
    success: function (res) {
        var districtsSelect = document.querySelector('#select-district');

        // Thêm các option mới lúc mở trang web
        for (var i = 0; i < res.length; i++) {
            var districtsOption = document.createElement('option');
            districtsOption.value = res[i].code;
            districtsOption.text = res[i].name;

            districtsSelect.add(districtsOption);
        }

        //Set phí vận chuyển
        // 1.Hà Nội - 48.Đà Nẵng - 79.Thành phố HCM
        if (provincesSelect.value == 1 || provincesSelect.value == 48 || provincesSelect.value == 79) {
            document.getElementById('delivery-fee--first').textContent = '75,000đ'
            document.getElementById('delivery-fee--second').textContent = '25,000đ'
            document.getElementById('delivery-fee--third').textContent = '15,000đ'
        }
        else {
            document.getElementById('delivery-fee--first').textContent = 'Không hỗ trợ'
            document.getElementById('delivery-fee--second').textContent = '32,000đ'
            document.getElementById('delivery-fee--third').textContent = '20,000đ'
        }

        console.log('Đọc API Provinces - quận/huyện thành công!');
    },
    error: function (error) {
        console.log('Đã có lỗi khi đọc API:', error);
    }
});



//Sự kiện change khi thay đổi dữ liệu của select tỉnh/thành phố
provincesSelect.addEventListener('change', () => {
    $.ajax({
        url: getDistrictsApi,
        type: 'GET',
        success: function (res) {
            var districtsSelect = document.querySelector('#select-district');

            // Xoá toàn bộ option
            while (districtsSelect.firstChild) {
                districtsSelect.removeChild(districtsSelect.firstChild);
            }

            // Thêm các option mới
            for (var i = 0; i < res.length; i++) {
                if (provincesSelect.value == res[i].province_code) {
                    var districtsOption = document.createElement('option');
                    districtsOption.value = res[i].code;
                    districtsOption.text = res[i].name;

                    districtsSelect.add(districtsOption);
                }
            }
            console.log('Đọc API Provinces - quận/huyện thành công! Làm mới!');
        },
        error: function (error) {
            console.log('Đã có lỗi khi đọc API:', error);
        }
    });
});



// Thông tin phương thức vận chuyển được lựa chọn
var deliveryMethods = document.querySelectorAll('input[name="delivery-method"]');

// Set phí vận chuyển và tổng tiền cần thanh toán
// ban đầu dựa trên thành phố ban đầu được chọn
document.getElementById('delivery_fee').textContent = '25,000đ';
document.getElementById('delivery_name').textContent = "Giao Hàng Nhanh";
updateTotalAmount();
deliveryMethods.forEach(function (radio) {
    radio.addEventListener('change', function () {

        var selectedDeliveryMethod = document.querySelector('input[name="delivery-method"]:checked');
        var deliveryName = selectedDeliveryMethod.nextElementSibling.textContent.trim();
        var deliveryFee = selectedDeliveryMethod.closest('.form-group').querySelector('.fee').textContent.trim();

        // Cập nhật thông tin trên giao diện
        document.getElementById('delivery_name').textContent = deliveryName;
        if (deliveryFee == 'Không hỗ trợ')
            document.getElementById('delivery_fee').textContent = '0đ';
        else
            document.getElementById('delivery_fee').textContent = deliveryFee;

        //Cập nhật lại tổng tiền cần thanh toán
        updateTotalAmount();
    });
});



// Thông tin phương thức thanh toán được lựa chọn
var paymentMethod = document.querySelectorAll('input[name="payment-method"]');
// Set phương thức vận chuyển ban đầu
document.getElementById('payment_name').textContent = 'Thanh toán khi nhận hàng';
paymentMethod.forEach(function (radio) {
    radio.addEventListener('change', function () {
        
        var selectedPaymentMethod = document.querySelector('input[name="payment-method"]:checked');
        var paymentName = selectedPaymentMethod.nextElementSibling.textContent.trim();

        // Cập nhật thông tin trên giao diện
        document.getElementById('payment_name').textContent = paymentName;
    });
});



//Thông tin phí vận chuyển dựa theo select tỉnh/ thành phố thay đổi
function updateDeliveryFee() {
    var provinceCitySelect = document.getElementById('select-province-city');
    var deliveryFeeFirstElement = document.getElementById('delivery-fee--first');
    var deliveryFeeSecondElement = document.getElementById('delivery-fee--second');
    var deliveryFeeThirdElement = document.getElementById('delivery-fee--third');

    var selectedProvinceCity = provinceCitySelect.value;

    // 1.Hà Nội - 48.Đà Nẵng - 79.Thành phố HCM
    if (selectedProvinceCity == 1 || selectedProvinceCity == 48 || selectedProvinceCity == 79) {
        var element = document.querySelector('.delivery-method .form-group')
        element.style.backgroundColor = '#fff';
        document.getElementById('delivery-method--first').removeAttribute('disabled');

        deliveryFeeFirstElement.textContent = '75,000đ';
        deliveryFeeSecondElement.textContent = '25,000đ';
        deliveryFeeThirdElement.textContent = '15,000đ';
    } else {
        var element = document.querySelector('.delivery-method .form-group')
        element.style.backgroundColor = 'lightgray';
        element.style.color = 'gray';
        document.getElementById('delivery-method--first').setAttribute('disabled', 'true');
        deliveryFeeFirstElement.textContent = 'Không hỗ trợ';
        deliveryFeeSecondElement.textContent = '32,000đ';
        deliveryFeeThirdElement.textContent = '20,000đ';
    }
}



//Tính tổng tiền cần thanh toán = tiền tạm tính + phí vận chuyển
function updateTotalAmount() {
    var subtotalElement = document.getElementById('subtotal');
    var deliveryFeeElement = document.getElementById('delivery_fee');
    var totalAmountElement = document.getElementById('total_amount');

    // Lấy giá trị tiền tạm tính và phí vận chuyển để tính tổng tiền cần thanh toán
    var subtotal = parseFloat(subtotalElement.textContent.replace('đ', '').replaceAll(',', ''));
    var deliveryFee = parseFloat(deliveryFeeElement.textContent.replace('đ', '').replaceAll(',', ''));

    var totalAmount = subtotal + deliveryFee;

    // Cập nhật giá trị tổng tiền
    totalAmountElement.textContent = numberWithCommas(totalAmount);
}

// Hàm thêm dấu phẩy ngăn cách hàng nghìn
function numberWithCommas(x) {
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}

//function submitForm() {
//    console.log('Vào hàm submitForm thành công')

    //var quanHuyen = document.querySelector('#select-district').options[document.querySelector('#select-district').selectedIndex].text
    //var tinhThanhPho = document.querySelector('#select-province-city').options[document.querySelector('#select-province-city').selectedIndex].text
    //var diaChi = document.querySelector('#checkout-form input[name="dia-chi"]').value + ' - ' + quanHuyen + ' - ' + tinhThanhPho;

//}
// Sự kiện submit form
var form = document.getElementById('checkout-form');
form.addEventListener('submit', function (event) {
    // Ngăn chặn việc submit mặc định của form
    event.preventDefault();
    console.log("Send mail success")
    var data = {
        tongTienThanhToan: document.getElementById('total_amount').textContent.replace('đ', '').replaceAll(',', ''),
        GhiChu: document.getElementById('note').textContent,
        phuongThucThanhToan: document.getElementById('payment_name').textContent,
        phuongThucVanChuyen: document.getElementById('delivery_name').textContent,
        phiVanChuyen: document.getElementById('delivery_fee').textContent.replace('đ', '').replaceAll(',', '')
    }

    var actionLink = '/Cart/Order?tongTienThanhToan=' + data.tongTienThanhToan + '&GhiChu=' + data.GhiChu + '&phuongThucThanhToan=' + data.phuongThucThanhToan + '&phuongThucVanChuyen=' + data.phuongThucVanChuyen + '&phiVanChuyen=' + data.phiVanChuyen;
    console.log(actionLink)
    form.setAttribute('action', actionLink)
    console.log(form)
    sendMail()
    //form.submit();
})
function sendMail() {
    var params = {
        customer_name: document.getElementById('customer_name').value,
        customer_email: document.getElementById('customer_email').value,
        product_name: document.getElementById('product_name').innerText,
        product_info: document.getElementById('product_info').innerText,
        product_quantity: document.getElementById('product_quantity').innerText,
        delivery_fee: document.getElementById('delivery_fee').innerText,
        delivery_name: document.getElementById('delivery_name').innerText,
        payment_name: document.getElementById('payment_name').innerText,
        total: document.getElementById('total_amount').innerText
    }
    emailjs.send('service_kecfp56', 'template_xjcnu87', params)
        .then(function (response) {
            console.log('Email sent successfully:', response);
            alert('Đặt hàng thành công!' + response.status);
            var form = document.getElementById('checkout-form');
            form.submit();
        }, function (error) {
            console.log('Error sending email:', error);
            alert('Đặt hàng thất bại!' + error.status);
        });
}