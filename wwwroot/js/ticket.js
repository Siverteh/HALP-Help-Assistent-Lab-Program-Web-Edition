function onCreate(){

    document.getElementById("nickname").value = "";
    document.getElementById("description").value = "";
    document.getElementById("room").value = "";
}

function onEdit(id){
    if (!isValid()) {
        return  document.getElementById("validation").textContent  = "Please fill out the empty fields";
    }
    console.log("Hello");

    document.getElementById("validation").textContent  = "";
    
    const nickname = document.getElementById("nickname").value;
    const description = document.getElementById("description").value;
    const room = document.getElementById("room").value;

    connection.invoke(
        "EditTicket",
        id,
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