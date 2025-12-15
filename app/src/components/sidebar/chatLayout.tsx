// src/components/chat/chatLayout.tsx
import { useState, useEffect } from "react";
import RoomSidebar from "./roomSidebar";
import RoomButton from "../createRoomButton/roomButton";
import JoinRoom from "../createRoomButton/joinRoomButton"
import { HubConnection } from "@microsoft/signalr";
import { useNavigate, useParams } from "react-router-dom";
import type { Room } from "../../types/Room";

interface ChatLayoutProps {
  connection: HubConnection | null;
  currentUserId: string;
  myRooms: { roomCode: string; name: string }[];
  onRoomCreated?: (room: Room) => void;
}

export default function ChatLayout({
  connection,
  currentUserId,
  myRooms,
  onRoomCreated,
}: ChatLayoutProps) {
  const { roomId } = useParams<{ roomId: string }>();
  const navigate = useNavigate();
  const [selectedRoom, setSelectedRoom] = useState<string | null>(roomId || null);

  useEffect(() => {
    setSelectedRoom(roomId || null);
  }, [roomId]);

  function handleRoomSelect(id: string) {
    setSelectedRoom(id);
    navigate(`/home/${id}`);
  }

  function handleNewRoom(room: Room) {
    if (onRoomCreated) onRoomCreated(room);
    handleRoomSelect(room.roomCode);
  }

  // Map App's myRooms to Room[]
  const roomsForSidebar: Room[] = myRooms.map((r) => ({
    id: r.roomCode,
    roomCode: r.roomCode,
    name: r.name,
  }));

  return (
    <div className="layout">
      <RoomSidebar rooms={roomsForSidebar} onSelect={handleRoomSelect} />
      <JoinRoom currentUserId={currentUserId} />
      <RoomButton
        buttonText="Create Room"
        connection={connection}
        onRoomCreated={handleNewRoom}
        currentUserId={currentUserId}
      />

      <div style={{ padding: "20px" }}>
        {selectedRoom ? (
          <h3>Selected Room: {selectedRoom}</h3>
        ) : (
          <h3>No room selected</h3>
        )}
      </div>
    </div>
  );
}
