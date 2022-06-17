var url;
var name;
var roomId;
var link;
var isPlay;
var seekMain;


async function setSettings() {
    url = document.URL;
    name = "";
    roomId = url.split('?key=')[1].split("&")[0];
    link = url.split('url=')[1].replaceAll("%2F", "/");
    isPlay = false;
    seekMain = 0;
    document.getElementById("if").src = link;
}

setSettings();
var connection = new signalR.HubConnectionBuilder()
    .withUrl('/chat')
    .build();
async function start() {
    try {
        await connection.start();
        name = document.getElementById("userName").innerText;
        if (name === "") {
            do {
                input = prompt("Введіть ім'я");
            } while (input == null || input == "");
            name = input;
        }

        Enter();
        console.log("SignalR Connected.");
        //var isEntered = false;
        //while (isEntered === false) {
        //    var input = prompt("Вкажіть ваше ім'я");
        //    if (input.length > 0) {
        //        isEntered = true;
        //        name = input;
        //        Enter();
        //    }

        //}
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

connection.onclose(async () => {
    await start();
});
start();


window.addEventListener("message",
    function (event) {
        if (event.data.event === 'time' & typeof event.data.time !== 'undefined') {
            seekMain = event.data.time;
            console.log(event.data.time);
        }
        if (event.data.event === 'seek' & typeof event.data.time !== 'undefined') {
            seekMain = event.data.time;
            console.log(event.data.time);
        }

        
        if (event.data.event == 'paused') {
            console.log("Signal pause!");
            try {
                isPlay = false;
                console.log('!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!');

                console.log(roomId);
                console.log('!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!');
                //connection.invoke("SetTime", roomId, seekMain);
                connection.invoke("Pause", roomId, seekMain);
                console.log(roomId);
            } catch (err) {
                console.error(err);
            }

        }
        if (event.data.event == 'resumed') {
            console.log("Signal play!");
            try {
                //connection.invoke("Enter", "test", name);
                console.log('!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!');
                console.log(seekMain);
                console.log(roomId);
                console.log('!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!');
                if (isPlay === false) {
                    connection.invoke("Play", roomId, seekMain);
                }
                
            } catch (err) {
                console.error(err);
            }
        }
    });
connection.on('Play', function (command, seek) {
    // Html encode display name and message.
    seekMain = seek;
    document.getElementById("if").contentWindow.postMessage({ "api": "seek", "set": seek }, "*");
    isPlay = true;
    document.getElementById("if").contentWindow.postMessage({ "api": "play" }, "*");

});
connection.on('SetTime', function (command, time) {

    document.getElementById("if").contentWindow.postMessage({ "api": "seek", "set": time }, "*");
});

connection.on('Pause', function (command, time) {
    seekMain = time;
    document.getElementById("if").contentWindow.postMessage({ "api": "seek", "set": time }, "*");
    document.getElementById("if").contentWindow.postMessage({ "api": "pause" }, "*");

});
connection.on('Send', function (message, username) {

    // создаем элемент <b> для имени пользователя
    let userNameElem = document.createElement("b");
    userNameElem.appendChild(document.createTextNode(username + ': '));

    // создает элемент <p> для сообщения пользователя
    let elem = document.createElement("p");
    elem.appendChild(userNameElem);
    elem.appendChild(document.createTextNode(message));

    var firstElem = document.getElementById("chat").firstChild;
    document.getElementById("chat").insertBefore(elem, firstElem);
});
connection.on('Notify', function (Users) {
    //var users = Object.entries(Users);
    document.getElementById("users").innerHTML = "";
    for (var i = 0; i < Users.length; i++) {
        let user = document.createElement("div");
        user.appendChild(document.createTextNode(Users[i]));
        var firstElem = document.getElementById("users").firstChild;
        document.getElementById("users").insertBefore(user, firstElem);
    }
    //for (const [key, value] of Object.entries(Users)) {
    //    let user = document.createElement("div");
    //    user.appendChild(document.createTextNode(`${key}`));
    //    var firstElem = document.getElementById("users").firstChild;
    //    document.getElementById("users").insertBefore(user, firstElem);
    //    console.log(`${key}: ${value}`);
    //}
});    

async function Enter() {
   await connection.invoke("Enter", roomId, name, seekMain, link);
}

window.addEventListener('beforeunload', function (event) {
    
    connection.invoke("Leave", roomId, name);
}, false);
document.getElementById("sendBtn").addEventListener("click", function (e) {
    let message = document.getElementById("message").value;
    connection.invoke("Send", message, roomId, name);
    document.getElementById("message").value = "";
    // сonnection.invoke("Send", message, roomId, name);
});
document.getElementById("submit-btn").addEventListener("click", function (e) {
    name = document.getElementById('oo').value;
    document.getElementById('first').visibility = "hidden";
    connection.invoke("Enter", roomId, name);
});
//connection.start();



var elements = document.getElementsByClassName("invite");

var linkUrl = window.location.href;
var myFunction = function () {
    var attribute = this.getAttribute("name");
    let xhr = new XMLHttpRequest();
    xhr.open("POST", "/Room/Invite?name=" + attribute + "&key=" + linkUrl.split('?key=')[1].split("&")[0]);
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