"use strict";

// Initiate connection
var connection = new signalR.HubConnectionBuilder().withUrl("/helplisthub").build();
connection.start().then(sendCourseCode);

async function sendCourseCode(){
    if (connection._connectionState === "Connected") {
        connection.invoke("AddToGroup", courseCode).catch(function (err) {
            return console.error(err.toString());
        });
    } else {
        setTimeout(() => sendCourseCode(), 500)
    }
}

// Inserts a new cell into the table
connection.on("AddToArchive", (id, nickname, description, status, room) => {
    insertCell(id, nickname, description, status, room);
});

// Inserts a new cell into the table
function insertCell(id, nickname, description, status, room) {
    var tbodyRef = document.getElementById('table').getElementsByTagName('tbody')[0];
    var newRow = tbodyRef.insertRow(tbodyRef.rows.length);
    newRow.id = id;

    // Insert a cell in the row at index 
    var c_nick  = newRow.insertCell(0);
    var c_desc  = newRow.insertCell(1);
    var c_room  = newRow.insertCell(2);
    var c_stat  = newRow.insertCell(3);
    var c_uaButton  = newRow.insertCell(4);
    c_uaButton.classList.add("tdButton");

    // Create a text node
    var ct_nick  = document.createTextNode(nickname);
    var ct_desc  = document.createTextNode(description);
    var ct_room  = document.createTextNode(room);
    var ct_stat  = document.createTextNode(status);
    var ct_uaButton = document.createElement("button");
    ct_uaButton.innerHTML = "Unarchive";
    ct_uaButton.type = "submit";
    ct_uaButton.classList.add("btn");
    ct_uaButton.classList.add("uaButton");
    ct_uaButton.addEventListener("click", unArchive);

    // Append a text node to the cell
    c_nick.appendChild(ct_nick);
    c_desc.appendChild(ct_desc);
    c_room.appendChild(ct_room);
    c_stat.appendChild(ct_stat);
    c_uaButton.appendChild(ct_uaButton);
}

// Receive message
connection.on("RemoveFromArchive",
    (id) => removeCell(id)
);

function removeCell(id) {
    const row = document.getElementById(id);
    row.remove();
}

// Button for unarchive
function unArchive(event) {
    var tr = event.target.parentNode.parentNode;
    var id = parseInt(tr.id);

    connection.invoke("RemoveFromArchive", id)
        .catch(err => console.error(err.toString()));
}
