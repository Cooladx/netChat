import * as signalR from "@microsoft/signalr";

export default function Button() {
  const connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:5019/hub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

  async function start() {
    try {
      await connection.start();
      console.log("SignalR Connected.");
    } catch (err) {
      console.log(err);
      setTimeout(start, 5000);
    }
  }

  connection.onclose(async () => {
    await start();
  });

  return (
    <>
      <button type="button" onClick={start}>
        {" "}
        Websocket Connect
      </button>
    </>
  );
}
