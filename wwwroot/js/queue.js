"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/helplisthub").build();
connection.start();

function removeTicket(id)
{
    connection.invoke("RemovedByUser", id).catch(function (err) {
        return console.error(err.toString());
    });

    redirect("/");
}

connection.on("Queue",
    (id, count, counter) => updateQueue(id, count, counter)
);
function updateQueue(id, count, counter) {
    const ticketId = document.querySelector('[id^="queue-"]').id.split("-");
    const current = document.getElementById("counter").innerHTML.toString();
    
    if (count === 0){
        if (id.toString() === ticketId[1].toString()) {
            document.getElementById("counter").innerHTML = "Resolved";
        }
        if (current !== "Resolved" && id < ticketId[1]) {
            document.getElementById("counter").innerHTML = current - 1;
        }
    }
    if (count === 1){
        if(current === "Resolved"){
            document.getElementById("counter").innerHTML = counter;
        }
        if (id < ticketId[1]) {
            document.getElementById("counter").innerHTML = (+current) + 1;
        }
    }
}

function redirect(url)
{
    location.replace(url);
}

