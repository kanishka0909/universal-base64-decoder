// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const toggle = document.getElementById("themeToggle");
const root = document.documentElement;

// Load saved theme
const savedTheme = localStorage.getItem("theme");

if (savedTheme) {
    root.setAttribute("data-theme", savedTheme);
} else {
    // system preference
    const prefersDark = window.matchMedia("(prefers-color-scheme: dark)").matches;
    root.setAttribute("data-theme", prefersDark ? "dark" : "light");
}

// Toggle click
toggle.addEventListener("click", () => {
    const current = root.getAttribute("data-theme");
    const next = current === "dark" ? "light" : "dark";

    root.setAttribute("data-theme", next);
    localStorage.setItem("theme", next);

    toggle.textContent = root.getAttribute("data-theme") === "dark" ? "☀️" : "🌙";
});