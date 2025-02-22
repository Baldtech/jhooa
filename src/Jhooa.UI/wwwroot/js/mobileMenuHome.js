export function loadMobileMenu() {
    const mobileMenuButton = document.querySelector(".mobile-menu-button");
    const mobileMenu = document.querySelector(".navigation-menu");

    if (mobileMenuButton === null || mobileMenu === null) {

    } else {
        mobileMenuButton.addEventListener("click", () => {
            mobileMenu.classList.toggle("hidden");
        });
    }
}
