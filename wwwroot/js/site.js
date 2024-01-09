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
   


//// Function to fetch and display products
//function loadProducts() {
//    $.ajax({
//        url: '/Product/GetProducts', // Adjust the URL based on your routing
//        method: 'GET',
//        success: function (products) {
//            // Clear existing products
//            $('#products .card-content').empty();

//            // Loop through the products and append them to the container
//            products.forEach(function (product) {
//                var productHtml = `
//                        <div class="row">
//                            <img src="${product.ImageUrl}" alt="${product.ProductName}">
//                            <div class="card-body">
//                                <h3>${product.ProductName}</h3>
//                                <p>${product.Description}</p>
//                                <h5>Price $${product.Price}</h5>
//                                <button>Add To Cart</button>
//                            </div>
//                        </div>`;
//                $('#products .card-content').append(productHtml);
//            });
//        },
//        error: function (error) {
//            console.error('Error fetching products:', error);
//        }
//    });
//}

//// Call loadProducts when the page loads
//$(document).ready(function () {
//    loadProducts();
//});
