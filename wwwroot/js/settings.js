"use strict";

// SignalR live updates
// Initiate connection
var connection = new signalR.HubConnectionBuilder().withUrl("/settingshub").build();
connection.start();

connection.on("ShowStudent",(courses, isAdmin) => {
    console.log("ShowStudent received");
    toggleStudassBoxes(courses);
    toggleAdminBox(isAdmin);
});

var userName = null;

function getUserData(event) {
    userName = event.target.value;
    connection.invoke("GetUserData", userName).catch(function (err) {
        return console.error(err.toString());
    });
}

function setStudass(event) {
    var courseCode = event.target.id;
    var isStudass = event.target.checked;
    connection.invoke("SetStudass", userName, courseCode, isStudass).catch(function (err) {
        return console.error(err.toString());
    });
}

function setAdmin(event) {
    var isAdmin = event.target.checked;
    connection.invoke("SetAdmin", userName, isAdmin).catch(function (err) {
        return console.error(err.toString());
    });
}

function toggleStudassBoxes(courses) {
    clearStudassBoxes();
    for (var i in courses) {
        var course = courses[i];
        var studassBox = document.getElementById(course);
        studassBox.checked = true;
    }
}

function toggleAdminBox(isAdmin) {
    var adminBox = document.getElementById("adminCheckBox");
    if (isAdmin) {
        adminBox.checked = true;
    }
    else {
        adminBox.checked = false;
    }
}

function clearStudassBoxes() {
    var checkBoxes = document.getElementsByClassName("studassbox");
    if (checkBoxes.length == 0) {
        return;
    }
    for (var i=0; i < checkBoxes.length; i++) {
        var box = checkBoxes[i];
        box.checked = false;
    }
}

// Timeedit link stuff
function addTimeeditLink(event) {
    var link = document.getElementById("testform").value;
    connection.invoke("AddTimeeditLink", link).catch(function (err) {
        return console.error(err.toString());
    });
}

function removeTimeeditLink(event) {
    var link = event.target.value;
    connection.invoke("RemoveTimeeditLink", link).catch(function (err) {
        return console.error(err.toString());
    });
}


// Showing of the different divs 
var settingsView = document.getElementById("settings");
var timeeditView = document.getElementById("timeedit");
var rolesView = document.getElementById("roles");

var settingsButton = document.getElementById("btn_settings");
var timeeditButton = document.getElementById("btn_timeedit")
var rolesButton = document.getElementById("btn_roles")

if(window.location.hash == '#timeedit'){
    showTimeEdit();
} else if (window.location.hash == '#roles') {
    showRoles();
} else {
    showSettings();
}

function showRoles() {
    window.location.hash = "#roles";
    rolesButton.style.backgroundColor = "#004A82";
    rolesView.style.display = "block";
    hideTimeEdit();
    hideSettings();
}

function hideRoles() {
    rolesButton.style.backgroundColor = "#07c";
    rolesView.style.display = "none";
}

function showSettings() {
    window.location.hash = "#settings";
    settingsButton.style.backgroundColor = "#004A82";
    settingsView.style.display = "block";
    hideTimeEdit();
    hideRoles();
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
    hideRoles();
}

function hideTimeEdit() {
    timeeditButton.style.backgroundColor = "#07c";
    timeeditView.style.display = "none";
}

function searchTable(event) {
    // Declare variables
    var input, filter, table, trs, i, txtValue;
    table = event.target.parentNode.children[1];
    input = event.target;
    filter = input.value.toUpperCase();
    //table = document.getElementById("users_table");
    trs = table.getElementsByTagName("tr");

    // Loop through all list items, and hide those who don't match the search query
    for (i = 0; i < trs.length; i++) {
        var tr = trs[i];
        txtValue = tr.textContent || tr.innerText;
        if (txtValue.toUpperCase().indexOf(filter) > -1) {
            trs[i].style.display = "";
        } else {
            trs[i].style.display = "none";
        }
    }
}