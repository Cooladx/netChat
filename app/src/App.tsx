import { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import "./App.css";
import Header from "./components/header/header.tsx";
import SocketButton from "./components/websocketButton/socketButton.tsx";
import Input from "./components/inputBox/input.tsx";
import MessageButton from "./components/submitMessageButton/messageButton.tsx";
import MessageBox from "./components/messageBox/messageBox.tsx";
import LoginPage from "./components/login/loginPage.tsx";
import ChatLayout from "./components/sidebar/chatLayout.tsx";
import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";

interface AppProps {
  loggedIn: boolean;
  setLoggedIn: React.Dispatch<React.SetStateAction<boolean>>;
  username: string;
  setUsername: React.Dispatch<React.SetStateAction<string>>;
  currentUserId: string;
  setCurrentUserId: React.Dispatch<React.SetStateAction<string>>;
}

export default function App({ loggedIn, setLoggedIn, username, setUsername, currentUserId, setCurrentUserId }: AppProps) {
  const [connection, setConnection] = useState<HubConnection | null>(null);
  const [message, setMessage] = useState("");
  const [myRooms, setMyRooms] = useState<{ roomCode: string; name: string }[]>([]);
  const navigate = useNavigate();

  const { roomId } = useParams<{ roomId: string }>();

  useEffect(() => {
  if (connection) return;

  const newConn = new HubConnectionBuilder()
    .withUrl("http://127.0.0.1:5019/hub")
    .withAutomaticReconnect()
    .build();

  setConnection(newConn);

  newConn
    .start()
    .then(() => console.log("Connected to SignalR hub"))
    .catch(err => console.error("SignalR connection error:", err));

}, []);


    useEffect(() => {
    const savedLoggedIn = localStorage.getItem("loggedIn");
    if (savedLoggedIn === "true") {
      setLoggedIn(true);
      setUsername(localStorage.getItem("username") || "");
      setCurrentUserId(localStorage.getItem("currentUserId") || "");
    }
  }, []);

  if (!loggedIn) {
    return <LoginPage setLoggedIn={setLoggedIn} setUsername={setUsername} setCurrentUserId={setCurrentUserId} />;
  }

  useEffect(() => {
  if (connection && roomId) {
    connection.invoke("JoinRoom", roomId)
      .then(() => console.log(`Joined room ${roomId}`))
      .catch(err => console.error(err));
  }
}, [connection, roomId]);

useEffect(() => {
  if (!connection) return;

  connection.on("ReceiveMessage", (user, message) => {
    const li = document.createElement("li");
    li.textContent = `${user}: ${message}`;
    document.getElementById("msgList")?.appendChild(li);
  });

  return () => {
    connection.off("ReceiveMessage");
  };
}, [connection]);

  return (
    <>
      <Header text="NetChat" />
      <button
        onClick={() => {
          setLoggedIn(false);
          setUsername("");
          setCurrentUserId("");
          navigate("/login");
        }}
      >
        Logout
      </button>

    {<SocketButton connection={connection} setConnection={setConnection} />}
      <ChatLayout connection={connection} currentUserId={currentUserId} myRooms={myRooms} />

      <div>
        <Header text={`Room: ${roomId ?? "Global Chat"}`} />
        <Input placeholder="Enter user" value={username} onValueChange={setUsername} />
        <Input placeholder="Enter Message" value={message} onValueChange={setMessage} />
        <MessageButton buttonText="Send Message" connection={connection} username={username} message={message}   id="sendMessageButton" roomId={roomId ?? "global"} />
        <MessageBox />
      </div>
    </>
  );
}
