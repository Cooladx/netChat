// src/components/createRoomButton/roomButton.tsx
import "./style.css";
import { HubConnection } from "@microsoft/signalr";
import type { Room } from "../../types/Room";

export default function RoomButton({
  buttonText,
  connection,
  onRoomCreated,
  currentUserId,
}: {
  buttonText: string;
  connection: HubConnection | null;
  onRoomCreated: (room: Room) => void;
  currentUserId: string;
}) {
  console.log("RoomButton received currentUserId:", currentUserId);
  async function handleClick() {
    const roomName = prompt("Enter room name:");
    if (!roomName?.trim()) return;

    const res = await fetch("http://localhost:5019/Room", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ roomName, creatorId: currentUserId }),
    });

    if (!res.ok) {
      alert("Failed to create room");
      return;
    }

    const data = await res.json();

    const newRoom: Room = {
      id: data.id,
      name: data.name,
      roomCode: data.roomCode,
    };

    onRoomCreated(newRoom);
  }

  return (
    <button className="msg-btn" onClick={handleClick}>
      {buttonText}
    </button>
  );
}
