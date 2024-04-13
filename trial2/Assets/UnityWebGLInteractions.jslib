mergeInto(LibraryManager.library, {
  // Existing function to send messages to Unity
  SendMessageToUnity: function(message) {
    var msg = Pointer_stringify(message);
    SendMessage('Stone', 'OnReceiveMessageFromJS', msg);
  },

  // New function to be called from Unity to register a callback or perform other actions
  RegisterCallback: function() {
    // JavaScript code for RegisterCallback goes here
    console.log("RegisterCallback called from Unity");

    // Example action: You might want to set up some event listeners or other initializations here
  }
});
