var startX = 0;
var endX = 0;
var isDragging = false;

export function addHandlers() {
    var carousel = document.querySelector(".carousel");
    if (carousel) {
        carousel.addEventListener("touchstart", startTouch, {passive: true});
        carousel.addEventListener("touchmove", moveTouch, {passive: false});
        carousel.addEventListener("touchend", endTouch);
    }

    // Ensure buttons work on mobile
    document.querySelector(".prev-btn")?.addEventListener("click", function () {
        prevSlide();
    });

    document.querySelector(".next-btn")?.addEventListener("click", function () {
        nextSlide();
    });
}

function startTouch(event) {
    startX = event.touches[0].clientX;
    isDragging = true;
}

function moveTouch(event) {
    if (!isDragging) return;
    event.preventDefault(); // Prevent page scrolling
}

function endTouch(event) {
    if (!isDragging) return;
    isDragging = false;

    endX = event.changedTouches[0].clientX;
    let diff = startX - endX;

    if (Math.abs(diff) > 50) {
        diff > 0 ? nextSlide() : prevSlide();
    }
}

function nextSlide() {
    var carousel = document.querySelector(".carousel");
    if (!carousel) return;
    let slideWidth = carousel.querySelector(".carousel-item").offsetWidth;

    carousel.scrollBy({
        left: slideWidth,
        behavior: "smooth",
    });
}

function prevSlide() {
    var carousel = document.querySelector(".carousel");
    if (!carousel) return;
    let slideWidth = carousel.querySelector(".carousel-item").offsetWidth;

    carousel.scrollBy({
        left: -slideWidth,
        behavior: "smooth",
    });
}