
// src/components/sidebar/roomSidebar.tsx


// Expected data for Rooms
type Room = {
  id: string;
  name: string;
};


// A side bar that has rooms. Should have an id of the room and be clickable to go inside the room. 
export default function RoomSidebar({
  rooms = [],
  onSelect = () => {},
}: {
  rooms?: Room[];
  onSelect?: (id: string) => void;
}) {
  return (
    <aside>
      <h2>Rooms</h2>
      <ul>
        {rooms.map((r) => (
          <li key={r.id}>
            <button onClick={() => onSelect(r.id)}>{r.name}</button>
          </li>
        ))}
      </ul>
    </aside>
  );
}
