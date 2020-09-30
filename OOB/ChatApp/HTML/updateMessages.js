var interval = setInterval(function () {

    var xhttp = new XMLHttpRequest();
    xhttp.open("GET", "/users", false);
    xhttp.send();
    document.getElementById("Users").innerHTML = xhttp.responseText;
}, 1000);

var currentMessageHtml = document.getElementById("messageWindow").innerHTML;
var interval = setInterval(function () {

    var xhttp = new XMLHttpRequest();
    xhttp.open("POST", "/messages", false);
    xhttp.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
    xhttp.send("userID" + "=" + document.getElementById("hdnUserID").value);
    
    if (xhttp.responseText != currentMessageHtml) {
        document.getElementById("messageWindow").innerHTML = xhttp.responseText;
        var objDiv = document.getElementById("messageWindow");
        objDiv.scrollTop = objDiv.scrollHeight;
        currentMessageHtml = xhttp.responseText;
    }
}, 1000);