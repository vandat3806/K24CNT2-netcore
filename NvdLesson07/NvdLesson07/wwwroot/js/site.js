document.addEventListener("DOMContentLoaded", () => {
    const currentPath = window.location.pathname.toLowerCase();

    document.querySelectorAll(".site-header .nav-link").forEach((link) => {
        const linkPath = new URL(link.href).pathname.toLowerCase();
        const isHome = linkPath === "/" && currentPath === "/";
        const isSection = linkPath !== "/" && currentPath === linkPath;

        if (isHome || isSection) {
            link.classList.add("active");
        }
    });

    const alert = document.querySelector(".status-alert");
    if (alert) {
        window.setTimeout(() => alert.remove(), 7000);
    }
});
