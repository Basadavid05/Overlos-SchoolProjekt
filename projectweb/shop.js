let allProducts = [];
let cart = [];

function fetchData(url, callback) {
    fetch(url)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            callback(data);
        })
        .catch(error => {
            console.error('Error fetching data:', error);
        });
}

function renderProducts(products) {
    const productsContainer = document.getElementById('product-container');
    productsContainer.innerHTML = '';
    products.forEach(product => {
        productsContainer.innerHTML += `
            <div class="pro" data-product-id="${product.id}">
                <img src="${product.image_url}" alt="${product.name}">
                <div class="des">
                    <h5>${product.name}</h5>
                    <p>${product.description}</p>
                    <p>$${product.price}</p>
                    <a class="add-to-cart" data-product-id="${product.id}">Add to Cart</a>
                </div>
            </div>`;
    });

    const addToCartButtons = document.querySelectorAll('.add-to-cart');
    addToCartButtons.forEach(button => {
        button.addEventListener('click', addToCart);
    });
}

function fetchAllProducts() {
    fetchData('shopapi.php', data => {
        allProducts = data;
        console.log(allProducts);
        renderProducts(allProducts);
    });
}

function addToCart(event) {
    const productId = event.target.getAttribute('data-product-id');
    const product = allProducts.find(product => product.id === productId);
    if (product) {
        cart.push(product);
        console.log('Product added to cart:', product);
        renderCart();
        saveCartToLocalStorage();
    } else {
        console.error('Product not found in allProducts array');
    }
}

function renderCart() {
    const cartContainer = document.getElementById('cart-container');
    const cartAmount = document.querySelector('.cartAmount');
    const totall = document.getElementById('total');
    cartContainer.innerHTML = '';
    let total = 0;
    cart.forEach((product, index) => {
        cartContainer.innerHTML += `
            <div class="cart-item">
                <h5>${product.name}</h5>
                <p>$${product.price}</p>
                <button class="remove-from-cart" data-index="${index}"><ion-icon name="trash"></ion-icon></button>
            </div>`;
            total += parseInt(product.price);
    });

    totall.innerHTML = 'Total:' + `$` + total;

    const removeButtons = document.querySelectorAll('.remove-from-cart');
    removeButtons.forEach(button => {
        button.addEventListener('click', removeFromCart);
    });
    cartAmount.textContent = cart.length;

    cartContainer.scrollTop = cartContainer.scrollHeight;
}

function removeFromCart(event) {
    const index = event.target.getAttribute('data-index');
    cart.splice(index, 1);
    renderCart();
    saveCartToLocalStorage();
}

function saveCartToLocalStorage() {
    localStorage.setItem('cart', JSON.stringify(cart));
}

function loadCartFromLocalStorage() {
    const savedCart = localStorage.getItem('cart');
    if (savedCart) {
        cart = JSON.parse(savedCart);
        renderCart();
    }
}

function filterProducts(products, searchQuery) {
    return products.filter(product =>
        product.name.toLowerCase().includes(searchQuery)
    );
}

document.getElementById('search_bar').addEventListener('input', function() {
    const searchQuery = this.value.trim().toLowerCase();
    const filteredProducts = filterProducts(allProducts, searchQuery);
    renderProducts(filteredProducts);
});

const categoryButtons = document.querySelectorAll('.butt');
categoryButtons.forEach(button => {
    button.addEventListener('click', function(event) {
        const category = event.target.textContent.toLowerCase();
        const filteredProducts = filterProductsByCategory(allProducts, category);
        renderProducts(filteredProducts);
    });
});

function filterProductsByCategory(products, category) {
    return products.filter(product =>
        product.category.toLowerCase() === category
    );
};

const showAll = document.getElementById('show-all-products');
showAll.addEventListener('click', function() {
    renderProducts(allProducts);
});

function renderAllProducts() {
    renderProducts(allProducts);
}

document.getElementById('checkout').addEventListener('click', function() {
    const buy = document.getElementById("buy");
    if (cart.length === 0) {
        alert('Your cart is empty. Please add some items before checkout.');
        return;
    }
    else{
        buy.style.display = 'block';
    }
});

document.getElementById('close-icon').addEventListener('click', function() {
    const buy = document.getElementById("buy");
    buy.style.display = 'none';
});

document.getElementById("buy-form").addEventListener('click', function(event) {
    const buy = document.getElementById("buy");
    event.preventDefault();

    const codes = cart.map(product =>product.code).join(', ');

    document.getElementById("codes").textContent = codes;

    cart = [];
    saveCartToLocalStorage();
    renderCart();

    alert('Thank you for your purchase!');
    buy.style.display = 'none';
});

document.addEventListener("DOMContentLoaded", function(){
    const showcart = document.getElementById('showcart');
    const cartContainer = document.getElementById('cart-container'); // Corrected selector
    const icon = document.querySelector('#showcart ion-icon');
    const cartAmount = document.querySelector('.cartAmount');
    const total = document.getElementById("total");
    const checkout = document.getElementById("checkout");
    let isOpen = false;
    showcart.onclick = function(){
        if (!isOpen) {
            cartContainer.style.display = 'block'; // Show the cart container
            cartAmount.style.display = 'none';
            icon.setAttribute('name', 'close');
            total.style.display = 'inline';
            checkout.style.display = 'inline';
            isOpen = true;
        } else {
            cartContainer.style.display = 'none'; // Hide the cart container
            cartAmount.style.display = 'block';
            icon.setAttribute('name', 'cart-outline');
            total.style.display = 'none';
            checkout.style.display = 'none';
            isOpen = false;
        }
    };
});


document.addEventListener("DOMContentLoaded", function(){
    const shopmenu = document.getElementById('shopmenu');
    const menu = document.querySelector('.menu');
    let isOpen = false;
    shopmenu.onclick = function(){
        if (!isOpen) {
            menu.style.display = 'block';
            isOpen = true;
        } else {
            menu.style.display = 'none';
            isOpen = false;
        }
    };
});


window.onload = function() {
    fetchAllProducts();
    loadCartFromLocalStorage();
};

function deleteProduct(productId) {
    if (confirm("Are you sure you want to delete this product?")) {
        fetch("shopapi.php", {
            method: "DELETE",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({ id: productId })
        })
        .then(response => {
            if (response.ok) {
                alert("Product deleted successfully");       
            } else {
                alert("Error deleting product");
            }
        })
        .catch(error => {
            console.error("Error:", error);
        });
    }
}