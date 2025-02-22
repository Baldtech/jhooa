export function loadMobileMenu() {
    const mobileMenuButton = document.querySelector(".mobile-menu-button");
    const mobileMenu = document.querySelector(".navigation-menu");
    if (mobileMenuButton === null || mobileMenu === null) {
        return;
    }

    mobileMenuButton.addEventListener("click", makeBackgroundYellow);

    function makeBackgroundYellow() {
        mobileMenu.classList.toggle("hidden");
    }
    
}
