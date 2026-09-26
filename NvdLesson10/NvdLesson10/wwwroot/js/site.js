document.querySelectorAll("[data-toggle-password]").forEach((button) => {
  button.addEventListener("click", () => {
    const input = button.closest(".password-field")?.querySelector("[data-password-input]");
    if (!input) return;
    const showPassword = input.type === "password";
    input.type = showPassword ? "text" : "password";
    button.textContent = showPassword ? "Ẩn" : "Hiện";
  });
});

document.querySelectorAll("[data-dismiss-alert]").forEach((button) => {
  button.addEventListener("click", () => button.closest("[data-auto-dismiss]")?.remove());
});

document.querySelectorAll("[data-auto-dismiss]").forEach((alert) => {
  window.setTimeout(() => alert.remove(), 5000);
});
