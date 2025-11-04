import { useState } from "react";
import { test, expect } from "vitest";
import { render, screen, fireEvent } from "@testing-library/react";
import RoomSidebar from "../components/sidebar/roomSidebar";
import UserSidebar from "../components/sidebar/userSidebar";




// Function to get the rooms and the users within these rooms
function RoomWithUsers({ rooms, usersMap }: any) {
  const [selectedRoomId, setSelectedRoomId] = useState<string | null>(null);
  return (
    <div>
      <RoomSidebar rooms={rooms} onSelect={(id) => setSelectedRoomId(id)} />
      <UserSidebar
        users={selectedRoomId ? (usersMap[selectedRoomId] ?? []) : []}
      />
      <div data-testid="selected-room">{selectedRoomId ?? "none"}</div>
    </div>
  );
}


// Tests the Room with Users to display 
test("fireEvent: clicking a room shows users", () => {
  const rooms = [
    { id: "r1", name: "General" },
    { id: "r2", name: "Random" },
  ];
  const usersMap = {
    r1: [{ id: "u1", name: "Alice" }],
    r2: [{ id: "u2", name: "Carol" }],
  };

  render(<RoomWithUsers rooms={rooms} usersMap={usersMap} />);

  fireEvent.click(screen.getByText("General"));

  // existence checks for user to be selected in a room
  expect(screen.getByText("Alice")).toBeDefined();
  expect(screen.getByTestId("selected-room").textContent).toBe("r1");
});
