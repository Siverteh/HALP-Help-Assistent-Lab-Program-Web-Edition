"use strict";

// Initiate connection
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
connection.start();

function isEmpty(value) {
    if(value === "" || value === null || value === undefined) {
        return true
    } else return false
}

function isValid() {
    const nickname = document.getElementById("nickname").value;
    const description = document.getElementById("description").value;
    const room = document.getElementById("room").value;
    
    
    
    if(isEmpty(nickname) || isEmpty(description) || isEmpty(room)) {
        return false
    } else return true
}

function onCreate(){
    if (!isValid()) {
        return  document.getElementById("validation").textContent  = "Please fill out the empty fields";
    }
    
    document.getElementById("validation").textContent  = "";
    
    const nickname = document.getElementById("nickname").value;
    const description = document.getElementById("description").value;
    const room = document.getElementById("room").value;
    
    connection.invoke(
        "CreateTicket", 
            nickname,
            description,
            room
        )
        .then((id) => window.location.replace(`/ticket/queue/${id}`))
        .catch((err) => console.error(err.toString()));

    document.getElementById("nickname").value = "";
    document.getElementById("description").value = "";
    document.getElementById("room").value = "";
}