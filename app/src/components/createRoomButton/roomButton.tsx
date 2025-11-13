import "./style.css";
import * as signalR from "@microsoft/signalr";
import { HubConnection } from "@microsoft/signalr";
export default function MessageButton({
  buttonText,
  id,
  connection,
  username,
  message,
}: {
  buttonText: string;
  id: string;
  connection: HubConnection | null;
  username: string;
  message: string;
}) {
  async function handleClick() {
    // console.log(username);
    // console.log(message);
    try {
      if (connection != null) {
        

        
      }
    } catch (err) {
      console.error(err);
    }
  }

  return (
    <>
      <button type="button" id={id} onClick={handleClick}>
        {buttonText}
      </button>
    </>
  );
}
