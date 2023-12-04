
//Set display none for banner of other website
window.onload = function() {
    console.log("Trang web đã tải xong!")
    
    setTimeout(() => {
        console.log("Chào mừng tới website shop thời trang")
        document.querySelectorAll('script + div')[0].style.display = 'none'
    }, 1500);
}


