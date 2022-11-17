var settings = document.getElementById("settings");
var timeedit = document.getElementById("timeedit");

function showSettings() {
    settings.style.display = "block";
    hideTimeEdit();
}

function hideSettings() {
    settings.style.display = "none";
}

function showTimeEdit() {
    timeedit.style.display = "block";
    hideSettings();
}

function hideTimeEdit() {
    timeedit.style.display = "none";
}