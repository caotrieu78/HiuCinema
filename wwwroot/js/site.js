// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener("DOMContentLoaded", function () {
    var popupMenuToggle = document.getElementById("popupMenuToggle");
    var popupMenu = document.querySelector(".popup-menu");
    var closeBtn = document.createElement("span");

    // Add close button (X) to the popup menu
    closeBtn.innerHTML = "&times;";
    closeBtn.classList.add("close-btn");
    popupMenu.appendChild(closeBtn);

    popupMenuToggle.addEventListener("click", function () {
        popupMenu.style.display = (popupMenu.style.display === "block") ? "none" : "block";
    });

    closeBtn.addEventListener("click", function () {
        popupMenu.style.display = "none";
    });

    // Close the popup menu when clicking outside of it
    document.addEventListener("click", function (event) {
        if (!popupMenu.contains(event.target) && event.target !== popupMenuToggle) {
            popupMenu.style.display = "none";
        }
    });
});
document.addEventListener('DOMContentLoaded', function () {
    var alert = document.querySelector('.alert');

    if (alert) {
        setTimeout(function () {
            alert.style.display = 'none';
        }, 2000);
    }
});