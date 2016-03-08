/// <reference path="bower_components/jquery/dist/jquery.js" />

// http://www.asp.net/signalr/overview/deployment/tutorial-signalr-self-host
// http://www.asp.net/signalr/overview/guide-to-the-api/hubs-api-guide-javascript-client

var username;

$(function() {
  username = prompt('Enter your name:', '');
  $('#displayname').val(username);
  $('#message').focus();
  
  // start the hub connections
  generatedProxy();
  manualProxy();  
});

// display message
var addMessage = function(name, message) {
  var encodedName = $('<div />').text(name).html();
  var encodedMsg = $('<div />').text(message).html();
  $('#discussion').append('<li><strong>' + encodedName
      + '</strong>:&nbsp;&nbsp;' + encodedMsg + '</li>');
}

var generatedProxy = function() {
  // By default, the hub location is the current server
  // if you are connecting to a different server, specify the URL before calling the start method, 
  $.connection.hub.url = "http://localhost:9000/signalr";
  
  // Declare a proxy to reference the hub.
  var generatedProxyHub = $.connection.generatedProxyHub;

  // Create a function that the hub can call to broadcast messages.
  generatedProxyHub.client.addMessage = function (name, message) {
    addMessage(name, message);
  };
  
  // add querystring to send data to server on connect
  $.connection.hub.qs = { 'user' : username };
  
  // Start the connection.
  $.connection.hub.start().done(function () {
    $('#sendmessage').click(function () {
      // Call the Send method on the hub.
      generatedProxyHub.server.send($('#displayname').val(), $('#message').val());
      // Clear text box and reset focus for next comment.
      $('#message').val('').focus();
    });
  });
}

var manualProxy = function() {
  // declare connection and proxy to communicate with the hub
  var hubConnection = $.hubConnection("http://localhost:9000/signalr");
  var hubProxy = hubConnection.createHubProxy("ManualProxyHub");
  
  hubProxy.on("addManualMessage", function(name, message) {
    addMessage(name, message);
  });
  
  // add querystring to send data to server on connect
  hubConnection.qs = { 'manualProxyUser' : username };
  
  hubConnection.start().done(function () {
    $('#sendManualProxyMessage').click(function () {
      // Call the Push method on the manual proxy hub.
      hubProxy.invoke('ManualProxyPush', $('#displayname').val(), $('#message').val());
      
      // Clear text box and reset focus for next comment.
      $('#message').val('').focus();
    });
  });
}