import { useState } from "react";
import "./App.css";
import * as signalR from "@microsoft/signalr";
import { HubConnection } from "@microsoft/signalr";
import Button from "./components/websocketButton";
import Input from "./components/inputBox";
import MessageButton from "./components/submitMessageButton";
import MessageBox from "./components/messageBox";
function App() {
  const [connection, setConnection] = useState<HubConnection | null>(null);
  const [username, setUsername] = useState<string>("");
  const [message, setMessage] = useState<string>("");
  return (
    <>
      <div>
        <Button connection={connection} setConnection={setConnection} />
        <div>
          <Input
            placeholder={"Enter user"}
            onValueChange={setUsername}
            value={username}
          />
        </div>
        <div>
          <Input
            placeholder={"Enter Message"}
            onValueChange={setMessage}
            value={message}
          />
        </div>
        {/* Send state connection, input from username, input from message to button */}
        <MessageButton
          buttonText={"Send Message"}
          id={"sendMessageButton"}
          connection={connection}
          username={username}
          message={message}
        />
        <MessageBox />
      </div>
    </>
  );
}

export default App;
