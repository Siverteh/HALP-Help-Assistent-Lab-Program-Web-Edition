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

function redirect(url)
{
    location.replace(url);
}

