import { useState } from "react";
import "./App.css";
import { HubConnection } from "@microsoft/signalr";
import Button from "./components/websocketButton/socketButton.tsx";
import Input from "./components/inputBox/input.tsx";
import MessageButton from "./components/submitMessageButton/messageButton.tsx";
import MessageBox from "./components/messageBox/messageBox.tsx";

// Mock components to build users with rooms.
import RoomSideBar from "./components/sidebar/roomSidebar.tsx";
import UserSidebar from "./components/sidebar/userSidebar.tsx";

import type { User } from "./components/sidebar/userSidebar"
/*
Parent component to take all children components under it to build the html and js logic.
Will also maintain the state and pass as props to the children components. 
*/
function App() {

  // Example rooms
  const rooms = [
    { id: "r1", name: "General" },
    { id: "r2", name: "Gaming" },
    { id: "r3", name: "Music" },
    { id: "r4", name: "School" },
    { id: "r5", name: "Jobs" },
  ];

  // Test users to display. Has their user id with name. Multiple users can be in a room or just one for now until someone else joins.
 const usersMap: Record<string, User[]> = {
  r1: [{ id: "u1", name: "Alice" }, { id: "u2", name: "Bob" }],
  r2: [{ id: "u3", name: "Carol" }],  
  r3: [{ id: "u3", name: "Mike" }]
};

  // Ensures to get the current room. 
  const [selectedRoomId, setSelectedRoomId] = useState<string | null>(null);

  function handleRoomSelect(id: string) {
    console.log("selected room:", id);
    setSelectedRoomId(id);
  }

  /* App manages connection, username, and message state for these children components under app. */
  const [connection, setConnection] = useState<HubConnection | null>(null);
  const [username, setUsername] = useState<string>("");
  const [message, setMessage] = useState<string>("");
  return (
    <>
      <RoomSideBar rooms={rooms} onSelect={handleRoomSelect} />
      <UserSidebar
        users={selectedRoomId ? (usersMap[selectedRoomId] ?? []) : []}
      />

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
