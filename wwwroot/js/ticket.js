"use strict";

// Initiate connection
var connection = new signalR.HubConnectionBuilder().withUrl("/helplisthub").build();
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
    const status = "waiting";
    const created = new Date()
    
    console.log(nickname)
    console.log(description)
    console.log(room)
    console.log(created)
    
    connection.invoke(
        "SendMessage", 
        nickname, description, room)
        .catch(function (err) {
            return console.error(err.toString());
        });
}