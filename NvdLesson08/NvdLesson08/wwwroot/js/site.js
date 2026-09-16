document.querySelectorAll("[data-password-toggle]").forEach((button) => {
  button.addEventListener("click", () => {
    const input = button.parentElement?.querySelector("input");
    if (!input) return;

    const shouldShow = input.type === "password";
    input.type = shouldShow ? "text" : "password";
    button.textContent = shouldShow ? "Ẩn" : "Hiện";
    button.setAttribute("aria-label", shouldShow ? "Ẩn mật khẩu" : "Hiện mật khẩu");
  });
});

window.setTimeout(() => {
  const alertElement = document.querySelector(".alert-wrap .alert");
  if (alertElement && window.bootstrap) {
    window.bootstrap.Alert.getOrCreateInstance(alertElement).close();
  }
}, 5000);
