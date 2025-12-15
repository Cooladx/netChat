// src/components/createRoomButton/joinRoomButton.tsx
import { useState } from "react";
import { useNavigate } from "react-router-dom";

export default function JoinRoom({ currentUserId }: { currentUserId: string }) {
  const [roomCode, setRoomCode] = useState("");
  const navigate = useNavigate();

  async function handleJoin() {
    if (!roomCode) return alert("Enter a room code");

    try {
      const res = await fetch(`http://localhost:5019/Room/${roomCode}/Join?userId=${currentUserId}`, {
        method: "POST",
      });

      if (!res.ok) {
        alert("Room not found");
        return;
      }

      // We don't care about the response body — the roomCode *is* the roomId
      console.log("Joined room:", roomCode);

      navigate(`/home/${roomCode}`);
    } catch (err) {
      console.error(err);
      alert("Failed to join room");
    }
  }

  return (
    <div>
      <h3>Join By Room Code</h3>
      <input
        type="text"
        value={roomCode}
        placeholder="Enter room code"
        onChange={(e) => setRoomCode(e.target.value)}
      />
      <button onClick={handleJoin}>Join Room</button>
    </div>
  );
}
