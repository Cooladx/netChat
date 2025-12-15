import "./style.css";
import { HubConnection } from "@microsoft/signalr";

/* Function to create a button that will send message from a user to the dotnet server. 
Client needs existing connection or this will not work. server will recieve that message and run method SendMessage.*/
export default function MessageButton({
  buttonText,
  id,
  connection,
  username,
  message,
  roomId,
}: {
  buttonText: string;
  id: string;
  connection: HubConnection | null;
  username: string;
  message: string;
  roomId: string;
}) {
  // Function to run when client clicks send message button.
  // it will run a method from the hubs class in the asp.net core backend.
  // The server will return a promise upon completing this request from the client.
  async function handleClick() {
    try {
      if (connection != null) {
        console.log("Sending to server:", username, message);
        await connection.invoke("SendMessage", roomId, username, message);
      }
    } catch (err) {
      console.error(err);
    }
  }

  return (
    <>
      <button type="button" id={id} onClick={handleClick} className="msg-btn">
        {buttonText}
      </button>
    </>
  );
}
