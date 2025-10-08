import { useState } from "react";
import "./App.css";
import { HubConnection } from "@microsoft/signalr";

// Components to build app.
import Header from "./components/header/header.tsx";
import SocketButton from "./components/websocketButton/socketButton.tsx";
import Input from "./components/inputBox/input.tsx";
import MessageButton from "./components/submitMessageButton/messageButton.tsx";
import MessageBox from "./components/messageBox/messageBox.tsx";


/*
Parent component to take all children components under it to build the html and js logic.
Will also maintain the state and pass as props to the children components. 
*/
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
