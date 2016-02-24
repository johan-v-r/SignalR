/// <reference path="bower_components/jquery/dist/jquery.js" />

// http://www.asp.net/signalr/overview/deployment/tutorial-signalr-self-host

$(function() {
   
    $("#test").text('awe');
    
    
    //Set the hubs URL for the connection
    $.connection.hub.url = "http://localhost:9000/signalr";
    
    // Start the connection.
    $.connection.hub.start().done(function () {
        $('#sendmessage').click(function () {
            // Call the Send method on the hub.
            chat.server.send($('#displayname').val(), $('#message').val());
            // Clear text box and reset focus for next comment.
            $('#message').val('').focus();
        });
    });
    // Declare a proxy to reference the hub.
    var chat = $.connection.testHub;
    
    // Create a function that the hub can call to broadcast messages.
    chat.client.addMessage = function (name, message) {
        // Html encode display name and message.
        var encodedName = $('<div />').text(name).html();
        var encodedMsg = $('<div />').text(message).html();
        // Add the message to the page.
        $('#discussion').append('<li><strong>' + encodedName
            + '</strong>:&nbsp;&nbsp;' + encodedMsg + '</li>');
    };
    // Get the user name and store it to prepend to messages.
    $('#displayname').val('test');
    // Set initial focus to message input box.
    $('#message').focus();
    
});