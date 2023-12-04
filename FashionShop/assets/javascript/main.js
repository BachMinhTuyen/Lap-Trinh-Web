// Pre - Next Item Manual
function preNextItem(AreaOfProduct) {
    const carousel = document.querySelector(AreaOfProduct + ' .carousel')
    const items = document.querySelectorAll(AreaOfProduct + ' .product-container')
    const arrowIcons = document.querySelectorAll(AreaOfProduct +' .product__header i')

    let length = items.length
    let itemWidth = items[0].clientWidth
    let itemTranslated = 0

    arrowIcons.forEach(icon => {
        icon.addEventListener('click', () => {
            if (icon.classList.contains('button-prev')) {
                if (itemTranslated == 0) 
                    return;
                itemTranslated += itemWidth
                carousel.style.transform = 'translateX('+ itemTranslated +'px)'
            }
            if (icon.classList.contains('button-next')) {
                if (itemTranslated == (length - 5) * itemWidth * (-1)) 
                    return;
                itemTranslated += -itemWidth
                carousel.style.transform = 'translateX('+ itemTranslated +'px)'
            }
        })
    })
}

// Pre Next New Product
preNextItem('.product--new')
preNextItem('.product--feature')

// Chờ tài liệu HTML được tải xong
document.addEventListener('DOMContentLoaded', function () {
    // Lấy thẻ <li> có lớp 'toggleable'
    var toggleableItems = document.querySelectorAll('.toggleable');
    // Lấy thẻ <li> có lớp 'more'
    var moreItem = document.querySelector('.more');

    // Lấy thẻ <a> trong thẻ <li> 'more'
    var moreLink = moreItem.querySelector('a');
    var label = moreItem.querySelector('label');

    // Thêm sự kiện click cho thẻ <a> 'Xem thêm ...'
    moreLink.addEventListener('click', function () {
        toggleableItems.forEach((item) => {
            // Kiểm tra nếu thẻ toggleable đang ẩn
            if (item.style.display === 'none') {
                // Hiển thị thẻ toggleable
                item.style.display = 'block';
                label.innerHTML = 'Ẩn bớt ...';
            } else {
                // Nếu đang hiển thị, ẩn đi
                item.style.display = 'none';
                label.innerHTML = 'Xem thêm ...';
            }
        });
    });
});