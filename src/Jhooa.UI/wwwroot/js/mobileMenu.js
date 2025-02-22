
document.addEventListener("DOMContentLoaded", () => {
    const mobileMenuButton = document.querySelector(".mobile-menu-button");
    const mobileMenu = document.querySelector(".navigation-menu");

    if (mobileMenuButton === null || mobileMenu === null) {
        return;
    }

    try {
        mobileMenuButton.removeEventListener("click", makeBackgroundYellow, true);
        mobileMenuButton.removeEventListener("click", makeBackgroundYellow, false);
    } catch (error) {
        console.error(error);
    }
    mobileMenuButton.addEventListener("click", makeBackgroundYellow);

    function makeBackgroundYellow() {
        mobileMenu.classList.toggle("hidden");
    }
});