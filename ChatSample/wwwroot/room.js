var url = document.URL;
var roomId = url.split('?key=')[1].split("&")[0];
var link = url.split('url=')[1].replaceAll("%2F", "/");
var isPlay = false;
document.getElementById("if").src = link;
var connection = new signalR.HubConnectionBuilder()
    .withUrl('/chat')
    .build();
var seekMain = 0;
window.addEventListener("message",
    function (event) {
        /*console.log(event.data.event + "ddddddddddddddddddddddddddddddd");*/
        if (event.data.event === 'time' & typeof event.data.time !== 'undefined') {
            seekMain = event.data.time;
            console.log(event.data.time);
        }
        if (event.data.event === 'seek' & typeof event.data.time !== 'undefined') {
            seekMain = event.data.time;
            console.log(event.data.time);
        }
        if (event == 'Seeked') {
            console.log("SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS");
        }
        //if (event.data.event === 'seek' & typeof event.data.time !== 'undefined' & event.data.playing == 'false') {
        //    console.log("SLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLL");
        //    seekMain = event.data.time;
        //    try {
        //        connection.invoke("SetTime", seekMain);
        //        //document.getElementById("if").contentWindow.postMessage({ "api": "time","set":seekMain }, "*");

        //    } catch (err) {
        //        console.error(err);
        //    }
        //    console.log(event.data.time);
        //}
        var err;
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
var name = '';

document.addEventListener('DOMContentLoaded',
    function () {

       

    });



window.addEventListener('beforeunload', function (event) {
    
    connection.invoke("Leave", roomId, "name1");
}, false);

document.getElementById("submit-btn").addEventListener("click", function (e) {
    var name1 = document.getElementById('oo').value;
    document.getElementById('first').visibility = "hidden";
    connection.invoke("Enter", roomId, name1);
});
connection.start();