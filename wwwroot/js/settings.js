var settingsView = document.getElementById("settings");
var timeeditView = document.getElementById("timeedit");
var usersView = document.getElementById("users");

var settingsButton = document.getElementById("btn_settings");
var timeeditButton = document.getElementById("btn_timeedit")
var usersButton = document.getElementById("btn_users")

if(window.location.hash == '#timeedit'){
    showTimeEdit();
} else {
    showSettings();
}

function showSettings() {
    window.location.hash = "#settings";
    settingsButton.style.backgroundColor = "#004A82";
    settingsView.style.display = "block";
    hideTimeEdit();
}

function hideSettings() {
    settingsButton.style.backgroundColor = "#07c";
    settingsView.style.display = "none";
}

function showTimeEdit() {
    window.location.hash = "#timeedit";
    timeeditButton.style.backgroundColor = "#004A82";
    timeeditView.style.display = "block";
    hideSettings();
}

function hideTimeEdit() {
    timeeditButton.style.backgroundColor = "#07c";
    timeeditView.style.display = "none";
}