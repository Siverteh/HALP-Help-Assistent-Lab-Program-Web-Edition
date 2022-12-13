"use strict";

// Initiate connection
var connection = new signalR.HubConnectionBuilder().withUrl("/helplisthub").build();
connection.start().then(sendCourseCode);

async function sendCourseCode(){
    if (connection._connectionState === "Connected") {
        connection.invoke("AddToGroup", courseCode)
            .catch(function (err) {
            return console.error(err.toString());
        });
    } else {
        setTimeout(() => sendCourseCode(), 500)
    }
}


connection.on("UpdateHelplist", (id, nickname, description, room) => {
    updateCell(id, nickname, description, room);
});

function updateCell(id, nickname, description, room) {
    
    var x = document.getElementsByTagName("tr");
    var i;
    var index = 0; 
    for (i = 0; i < x.length;i++) {
        if (x[i].id === id.toString()) {
            index =  x[i].rowIndex;
            continue;
        }
    }
    //delete row
    let table = document.querySelector('table');
    table.deleteRow(index);
    
    insertCell(id, nickname, description, room, index);
}

// Receive message
connection.on("AddToHelplist", (id, nickname, description, room) => {
    insertCell(id, nickname, description, room);
});

// Inserts a new cell into the table
function insertCell(id, nickname, description, room, index) {
    var tbodyRef = document.getElementById('table').getElementsByTagName('tbody')[0];
    var newRow = tbodyRef.insertRow(!index ? tbodyRef.rows.length : index);
    newRow.id = id;

    // Insert a cell in the row at index 
    var c_nick  = newRow.insertCell(0);
    var c_desc  = newRow.insertCell(1);
    var c_room  = newRow.insertCell(2);
    var c_uaButton  = newRow.insertCell(3);
    c_uaButton.classList.add("tdButton");

    // Create a text node
    var ct_nick  = document.createTextNode(nickname);
    var ct_desc  = document.createTextNode(description);
    var ct_room  = document.createTextNode(room);
    var ct_uaButton = document.createElement("button");
    ct_uaButton.innerHTML = "Archive";
    ct_uaButton.type = "submit";
    ct_uaButton.classList.add("btn");
    ct_uaButton.classList.add("aButton");
    ct_uaButton.addEventListener("click", archive);

    // Append a text node to the cell
    c_nick.appendChild(ct_nick);
    c_desc.appendChild(ct_desc);
    c_room.appendChild(ct_room);
    c_uaButton.appendChild(ct_uaButton);
}

// Receive message
connection.on("RemoveFromHelplist",
    (id) => removeCell(id)
);

connection.on("RemovedByUser",
    (id) => removeCell(id)
);

function removeCell(id) {
    const row = document.getElementById(id);
    row.remove();
}


// Button for archiving student
function archive(event) {
    var tr = event.target.parentNode.parentNode;
    var id = parseInt(tr.id);
    
    connection.invoke("RemoveFromHelplist", id)
        .catch((err) => console.error(err.toString()));
}