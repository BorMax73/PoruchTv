var connection = new signalR.HubConnectionBuilder()
    .withUrl('/notification')
    .build();
async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        console.log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");

        setTimeout(start, 5000);
    }
};

connection.onclose(async () => {
    await start();
});
start();
connection.on('FriendCount', function (count) {

    document.getElementById("friends-counter").innerText = count;
});
connection.on('invite', function (message, key, url) {
    /*alert("asd");*/
    document.getElementById("Message").style.visibility = 'visible';
    document.getElementById("Message").name = url;
    
    
});
var elements = document.getElementsByClassName("invite");
//$('#js-showMe').on('click', function (e) {
//    var url = document.getElementById("Message").getAttribute("name");
//    window.location.replace(url);
//    closeMessage($(this).closest('.Message'));
//});
//function closeMessage(el) {
//    document.getElementById("Message").style.visibility = 'hidden';
//    //el.addClass('is-hidden');
//}
document.getElementById("js-showMe").addEventListener("click", function (e) {
    var url = document.getElementById("Message").name;
    document.getElementById("Message").style.visibility = 'hidden';
     window.location.replace("/Home");
    
});
var linkUrl = window.location.href;
var myFunction = function () {
    var attribute = this.getAttribute("name");
    let xhr = new XMLHttpRequest();
    xhr.open("POST", "/Room/Invite?name=" + attribute + "&url=" + linkUrl);
    xhr.setRequestHeader("Accept", "application/json");
    xhr.setRequestHeader("Content-Type", "application/json");

    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4) {
            console.log(xhr.status);
            console.log(xhr.responseText);
        }
    };

    let data = `{
  "name": ${attribute}
   }`;

    xhr.send(data);
};

for (var i = 0; i < elements.length; i++) {
    elements[i].addEventListener('click', myFunction, false);
}