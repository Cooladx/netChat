import { useState } from "react";
import "./App.css";
import { HubConnection } from "@microsoft/signalr";
import Button from "./components/websocketButton/button.tsx";
import Input from "./components/inputBox/input.tsx";
import MessageButton from "./components/submitMessageButton/messageButton.tsx";
import MessageBox from "./components/messageBox/messageBox.tsx";
function App() {
  /* App manages connection, username, and message state for these children components under app. */
  const [connection, setConnection] = useState<HubConnection | null>(null);
  const [username, setUsername] = useState<string>("");
  const [message, setMessage] = useState<string>("");
  return (
    <>
      <Header />
      <div>
        <SocketButton connection={connection} setConnection={setConnection} />
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
