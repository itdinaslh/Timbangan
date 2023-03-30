"use strict";

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/hub/print")
    .build();

connection.start();

connection.on("StatusChange", function (value) {
    if (value == "disconnected") {
        $('#printAlert').show();
    } else {
        $('#printAlert').hide();
    }
});