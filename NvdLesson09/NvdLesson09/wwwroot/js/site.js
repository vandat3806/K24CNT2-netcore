document.querySelectorAll("[data-image-input]").forEach((input) => {
  input.addEventListener("change", () => {
    const preview = input.closest(".upload-zone")?.querySelector("[data-image-preview]");
    const file = input.files?.[0];

    if (!preview || !file) {
      return;
    }

    const reader = new FileReader();
    reader.addEventListener("load", () => {
      preview.replaceChildren();
      const image = document.createElement("img");
      image.src = reader.result;
      image.alt = `Xem trước ${file.name}`;
      preview.appendChild(image);

      const label = document.createElement("small");
      label.textContent = file.name;
      preview.appendChild(label);
    });
    reader.readAsDataURL(file);
  });
});

document.querySelectorAll("[data-dismiss-alert]").forEach((button) => {
  button.addEventListener("click", () => button.closest("[data-auto-dismiss]")?.remove());
});

document.querySelectorAll("[data-auto-dismiss]").forEach((alert) => {
  window.setTimeout(() => alert.remove(), 5000);
});
