'use strict';


document.addEventListener("DOMContentLoaded", function () {
    // Chờ đợi sự kiện khi trang đã tải xong
    var button = document.getElementById("playPauseButton");
    var videoState = localStorage.getItem('videoPlayed');

    if (videoState === 'true') {
        updateButton(button, true); // Cập nhật nút khi video đã được phát
    } else {
        updateButton(button, false); // Cập nhật nút khi video chưa được phát
    }
});

function updateButton(button, isVideoPlayed) {
    // Cập nhật nội dung của nút dựa trên trạng thái của video
    if (isVideoPlayed) {
        button.innerHTML = '<ion-icon name="pause"></ion-icon><span>Dừng Trailer</span>';
    } else {
        button.innerHTML = '<ion-icon name="play"></ion-icon><span>Xem Trailer</span>';
    }
}

function playPause() {
    var video = document.getElementById("myVideo");
    var image = document.getElementById("bannerImage");
    var button = document.getElementById("playPauseButton");

    if (video.paused) {
        video.style.display = "block";
        image.style.display = "none";
        video.play();
        button.innerHTML = '<ion-icon name="pause"></ion-icon><span>Dừng Trailer</span>';
        localStorage.setItem('videoPlayed', 'true');
    } else {
        video.pause();
        video.style.display = "none";
        image.style.display = "block";
        button.innerHTML = '<ion-icon name="play"></ion-icon><span>Xem Trailer</span>';
        localStorage.setItem('videoPlayed', 'false');
    }
    button.blur(); // Xóa focus khỏi nút sau khi click
}
/**
 * navbar variables
 */

const navOpenBtn = document.querySelector("[data-menu-open-btn]");
const navCloseBtn = document.querySelector("[data-menu-close-btn]");
const navbar = document.querySelector("[data-navbar]");
const overlay = document.querySelector("[data-overlay]");

const navElemArr = [navOpenBtn, navCloseBtn, overlay];

for (let i = 0; i < navElemArr.length; i++) {

  navElemArr[i].addEventListener("click", function () {

    navbar.classList.toggle("active");
    overlay.classList.toggle("active");
    document.body.classList.toggle("active");

  });

}



/**
 * header sticky
 */

const header = document.querySelector("[data-header]");

window.addEventListener("scroll", function () {

  window.scrollY >= 10 ? header.classList.add("active") : header.classList.remove("active");

});



/**
 * go top
 */


document.addEventListener("DOMContentLoaded", function () {
    // Lắng nghe sự kiện khi nút "Go to Top" được nhấp
    document.getElementById("goTopBtn").addEventListener("click", function () {
        window.scrollTo({
            top: 0,
            behavior: "smooth"
        });
    });

    // Lắng nghe sự kiện cuộn trang
    window.addEventListener("scroll", function () {
        var goTopBtn = document.getElementById("goTopBtn");
        if (window.scrollY >= 500) {
            goTopBtn.classList.add("active");
        } else {
            goTopBtn.classList.remove("active");
        }
    });
});


(function scrollBarPercent() {   
    const percent = document.querySelector(".progress-bar .percent");
     function percentPage() {
        const scrollY = window.scrollY,
            bodyHeight = document.documentElement.scrollHeight,
            screenHeight = document.documentElement.clientHeight, 
            progressBar = `${(scrollY / (bodyHeight - screenHeight)) * 100}`;
        percent.style.width = `${progressBar < 100 ? progressBar : 100}%`;
    }
    percentPage();
    window.addEventListener("scroll", percentPage);
})();
