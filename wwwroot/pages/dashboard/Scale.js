"use strict";

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/hub/scale")
    .build();

connection.start();

var pos = "1";
var current = "Timbangan1";
var curValue = 6800;

connection.on("Timbangan1", function (value) {    
    const div = document.getElementById('berat');    

    if (pos == "1") {        
        curValue = value;
        div.textContent = curValue;
    }
});

connection.on("Timbangan2", function (value) {    
    const div = document.getElementById('berat');    

    if (pos == "2") {        
        curValue = value;
        div.textContent = curValue;
    }
});

connection.on("Timbangan3", function (value) {    
    const div = document.getElementById('berat');       

    if (pos == "3") {        
        curValue = value;
        div.textContent = curValue;
    }
});

//connection.hub.disconnected(function () {
//    setTimeout(function () {
//        connection.hub.start();
//    }, 5000); // Restart connection after 5 seconds.
//});

function ChangePos() {
    curValue = 0;
    var x = document.getElementById("pos").value;
    $('#berat').text(curValue);
    pos = x;
    current = 'Timbangan' + pos;
}

$(document).on('change', '#pos', function () {
    ChangePos();
});