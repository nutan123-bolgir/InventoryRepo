// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


let elementsToToggle = document.querySelectorAll(".sidebar, .home-section, .home-section nav ");
let sidebarBtn = document.querySelector(".sidebarBtn");

sidebarBtn.onclick = function () {
    elementsToToggle.forEach(element => {
        element.classList.toggle("active");
    });
}
