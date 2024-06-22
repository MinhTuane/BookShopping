const connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .build();

connection.on("ReceiveMessage", (user, message) => {
    const msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    const encodedMsg = user + ": " + msg;
    const div = document.createElement("div");

    const currentUser = document.getElementById("userInput").value;
    if (user === currentUser) {
        div.classList.add("message", "sent");
    } else {
        div.classList.add("message", "received");
    }

    div.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(div);
});

connection.start().then(() => {
    if (document.getElementById("userInput")) {
        const userName = document.getElementById("userInput").value;
        connection.invoke("JoinGroup", userName).catch(err => console.error(err.toString()));
    } else {
        console.warn("User input element not found. Unable to join group.");
    }
}).catch(err => console.error(err.toString()));

document.getElementById("sendButton").addEventListener("click", event => {
    const recipient = document.getElementById("recipientInput").value;
    const message = document.getElementById("messageInput").value;
    connection.invoke("SendMessageToUser", recipient, message).catch(err => console.error(err.toString()));
    event.preventDefault();
});
