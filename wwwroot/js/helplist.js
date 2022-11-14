"use strict";

// Initiate connection
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
connection.start().then(sendCourseCode);

async function sendCourseCode(){
    if (connection._connectionState === "Connected") {
        connection.invoke("AddToGroup", courseCode).catch(function (err) {
            return console.error(err.toString());
        });
        console.log("Connected");
    } else {
        setTimeout(() => sendCourseCode(), 500)
    }
}

// Receive message
connection.on("UserAdded",() => {
    console.log("Good to go");
});

// Receive message
connection.on("AddHelplistEntry", (id, nickname, description) => {
    insertCell(nickname, description);
});

// Inserts a new cell into the table
function insertCell(id, nickname, description) {
    var tbodyRef = document.getElementById('table').getElementsByTagName('tbody')[0];
    var newRow = tbodyRef.insertRow(tbodyRef.rows.length);
    newRow.id = id;

    // Insert a cell in the row at index 
    var c_nick  = newRow.insertCell(0);
    var c_desc  = newRow.insertCell(1);
    var c_stat  = newRow.insertCell(2);
    var c_uaButton  = newRow.insertCell(3);
    c_uaButton.classList.add("tdButton");

    // Create a text node
    var ct_nick  = document.createTextNode(nickname);
    var ct_desc  = document.createTextNode(description);
    var ct_stat  = document.createTextNode("Finished");
    var ct_uaButton = document.createElement("button");
    ct_uaButton.innerHTML = "Archive";
    ct_uaButton.type = "submit";
    ct_uaButton.classList.add("btn");
    ct_uaButton.classList.add("aButton");
    ct_uaButton.addEventListener("click", archive);

    // Append a text node to the cell
    c_nick.appendChild(ct_nick);
    c_desc.appendChild(ct_desc);
    c_stat.appendChild(ct_stat);
    c_uaButton.appendChild(ct_uaButton);
}

// Receive message
connection.on("RemoveFromHelplist", (id) => {
    removeCell(id)
});

function removeCell(id) {
    const row = document.getElementById(id);
    row.remove();
}

// Button for archiving student
function archive(event) {
    var tr = event.target.parentNode.parentNode;
    var id = parseInt(tr.id);
    var nickname = tr.children[0].innerText;
    var description = tr.children[1].innerText;
    connection.invoke("AddToArchive", id, courseCode, nickname, description).catch(function (err) {
        return console.error(err.toString());
    });
}

// Button for testing
document.getElementById("sendButton").addEventListener("click", function (event) {
    connection.invoke("AddToHelplist", 100, "Test", "This is a test", courseCode).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});