// src/components/chat/roomSidebar.tsx
import type { Room } from "../../types/Room";

interface RoomSidebarProps {
  rooms?: Room[];
  onSelect?: (id: string) => void;
}

export default function RoomSidebar({ rooms = [], onSelect }: RoomSidebarProps) {
  return (
    <div className="room-sidebar">
      <h3>Rooms</h3>
      {rooms.length === 0 && <p>No rooms yet</p>}
      <ul>
        {rooms.map((room) => (
          <li key={room.id}>
            <button onClick={() => onSelect?.(room.id)}>{room.name}</button>
          </li>
        ))}
      </ul>
    </div>
  );
}
