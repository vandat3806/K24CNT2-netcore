document.addEventListener("DOMContentLoaded", () => {
    const currentPath = window.location.pathname.toLowerCase();

    document.querySelectorAll(".navbar .nav-link").forEach(link => {
        const linkPath = new URL(link.href).pathname.toLowerCase();
        if (linkPath !== "/" && currentPath.startsWith(linkPath)) {
            link.classList.add("active");
        }
    });
});
