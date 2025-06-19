window.setTheme = function (theme) {
    const body = document.body;

    // Remove all theme classes
    body.classList.remove("theme-light", "theme-dark", "theme-creme", "theme-dark-gold");

    // Add the selected one
    if (["light", "dark", "creme", "dark-gold"].includes(theme)) {
        body.classList.add(`theme-${theme}`);
        localStorage.setItem("preferredTheme", theme);
    } else {
        console.warn(`Unknown theme: ${theme}`);
    }
}

// Optionally auto-apply theme on load
window.applySavedTheme = function () {
    const saved = localStorage.getItem("preferredTheme") || "light";
    window.setTheme(saved);
}
