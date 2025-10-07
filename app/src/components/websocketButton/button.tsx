import * as signalR from "@microsoft/signalr";
import { HubConnection } from "@microsoft/signalr";
export default function Button({
  connection,
  setConnection,
}: {
  connection: signalR.HubConnection | null;
  setConnection: React.Dispatch<React.SetStateAction<HubConnection | null>>;
}) {
  async function start() {
    if (connection != null) {
      console.log("you already have a connection!");
      return;
    }
    const socketConnection = new signalR.HubConnectionBuilder()
      .withUrl("http://localhost:5019/hub")
      .configureLogging(signalR.LogLevel.Information)
      .build();

    setConnection(socketConnection);
    socketConnection.onclose(async () => {
      await start();
    });

    try {
      await socketConnection.start();
      console.log("SignalR Connected.");
      console.log(socketConnection);
    } catch (err) {
      console.log(err);
      setTimeout(start, 5000);
    }
  }

  return (
    <>
      <button type="button" onClick={start}>
        {" "}
        Websocket Connect
      </button>
    </>
  );
}
