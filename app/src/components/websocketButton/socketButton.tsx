import * as signalR from "@microsoft/signalr";
import { HubConnection } from "@microsoft/signalr";
import "./style.css"
/* 
  Function to create logic for client to make connection to websocket.
  If client has no connection when clicking on button, they will be granted a signalR hub connection. 
  Also, added listener from backend to ingest the message from client and broadcast to all other clients.
*/
export default function SocketButton({
  connection,
  setConnection,
}: {
  connection: signalR.HubConnection | null;
  setConnection: React.Dispatch<React.SetStateAction<HubConnection | null>>;
}) {
  // Function to wait for client to connect to upon clicking button.
  async function start() {
    if (connection != null) {
      console.log("you already have a connection!");
      return;
    }
    // Create socket connection to give client unique id.
    const socketConnection = new signalR.HubConnectionBuilder()
      .withUrl("http://localhost:5019/hub")
      .configureLogging(signalR.LogLevel.Information)
      .build();

    setConnection(socketConnection);

    // Registers a handler that will be invoked when the connection is closed.
    socketConnection.onclose(async () => {
      await start();
    });

    try {
      await socketConnection.start();
      console.log("SignalR Connected.");

      // Add listener so that when server gets the string of user and message, the client should recieve this information in the
      // box below: MESSAGES RECIEVED.
      socketConnection.on("ReceiveMessage", (user, message) => {
        console.log(`Received: ${user} - ${message}`);
        const li = document.createElement("li");
        li.className = "msg-element";
        li.textContent = `${user}: ${message}`;
        document.getElementById("msgList")?.appendChild(li);
      });

      console.log(socketConnection);
    } catch (err) {
      console.log(err);
      setTimeout(start, 5000);
    }
  }

  return (
    <>
      <button type="button" onClick={start} className="socket-btn">
        {" "}
        Websocket Connect
      </button>
    </>
  );
}
