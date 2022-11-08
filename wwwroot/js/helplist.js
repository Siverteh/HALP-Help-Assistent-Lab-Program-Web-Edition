function changeStatusText()
{
    var elem = document.getElementById("btn-status");
    if (elem.value=="Waiting") elem.value = "Finished";
    else elem.value = "Waiting";
}

$('.btn-status').click(function(e) {
    console.log(e);
    console.log(e.currentTarget);
    console.log($(e.currentTarget));
});