
//Set display none for banner of other website
window.onload = function() {
    console.log("Trang web đã tải xong!")
    
    setTimeout(() => {
        console.log("Chào mừng tới website shop thời trang")
    }, 1500);
}

$(document).ready(function () {
    toastr.options = {
        "closeButton": true,
        "progressBar": true,
        "positionClass": "toast-top-right",
        "showDuration": "3000",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };

    window.showToast = {
        success: function (msg) { toastr.success(msg); },
        error: function (msg) { toastr.error(msg); },
        info: function (msg) { toastr.info(msg); },
        warning: function (msg) { toastr.warning(msg); }
    };
});
