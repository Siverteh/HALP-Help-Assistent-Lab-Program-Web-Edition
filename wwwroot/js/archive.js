"use strict";

// Initiate connection
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
connection.start();

// Inserts a new cell into the table
connection.on("ReceiveMessage", (id, nickname, description) => {
    insertCell(id, nickname, description)
});

// Inserts a new cell into the table
function insertCell(id, nickname, description) {
    var tbodyRef = document.getElementById('table').getElementsByTagName('tbody')[0];
    var newRow = tbodyRef.insertRow(tbodyRef.rows.length);

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
    ct_uaButton.innerHTML = "Unarchive";
    ct_uaButton.type = "submit";
    ct_uaButton.classList.add("btn");
    ct_uaButton.classList.add("uaButton");
    ct_uaButton.addEventListener("click", unArchive);

    // Append a text node to the cell
    c_nick.appendChild(ct_nick);
    c_desc.appendChild(ct_desc);
    c_stat.appendChild(ct_stat);
    c_uaButton.appendChild(ct_uaButton);
}

// Button for testing
document.getElementById("sendButton").addEventListener("click", function (event) {
    connection.invoke("AddHelplistEntry", 100, "Test", "This is a test", roomID).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

// Button for unarchive
function unArchive() {
    connection.invoke("UnArchive", 1, roomID).catch(function (err) {
        return console.error(err.toString());
    });
}
