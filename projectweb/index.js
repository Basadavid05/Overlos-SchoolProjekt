let items = ('.slider .list .item');
let next = document.getElementById('next');
let prev = document.getElementById('prev');
let thumbnails = document.querySelectorAll('.thumbnail .item');

let countItem = items.length;
let itemActive = 0;

next.onclick = function(){
    itemActive = itemActive + 1;
    if(itemActive >= countItem){
        itemActive = 0;
    }
    showSlider();
};

prev.onclick = function(){
    itemActive = itemActive - 1;
    if(itemActive < 0){
        itemActive = countItem - 1;
    }
    showSlider();
};

let autoSlide = setInterval(() => {
    next.click();
}, 7000);

function showSlider(){

    let itemActiveOld = document.querySelector('.slider .list .item.active');
    let thumbnailActiveOld = document.querySelector('.thumbnail .item.active');
    itemActiveOld.classList.remove('active');
    thumbnailActiveOld.classList.remove('active');

    items[itemActive].classList.add('active');
    thumbnails[itemActive].classList.add('active');

    clearInterval(autoSlide);
    autoSlide = setInterval(() => {
        next.click();
    }, 7000);
};

thumbnails.forEach((thumbnail, index) => {
    thumbnail.addEventListener('click', () => {
        itemActive = index;
        showSlider();
    });
});

thumbnails.forEach(function(item) {
    item.addEventListener("mouseenter", function(){
        this.style.cursor = "pointer";
    });

    item.addEventListener("mouseleave", function() {
        this.style.cursor = "auto";
    });
});

if(window.matchMedia("(max-width: 480px)").matches){
    let thumbnails = document.querySelectorAll('.thumbnail .item img');
    thumbnails.forEach(function(thumbnail, index){
            thumbnail.addEventListener('click', function(){
               switch(index){
                   case 0:
                       window.location.href = 'game.php';
                       break;
                   case 1:
                       window.location.href = 'shop.php';
                       break;
               } 
            });
    });
}




