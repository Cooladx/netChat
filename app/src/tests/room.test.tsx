import { test, expect, vi } from "vitest";
import { render, screen, fireEvent } from "@testing-library/react";
import RoomSidebar from "../components/sidebar/roomSidebar";



// Test to do a sample of rooms and should be expected with rendering a room side bar
test("renders room list and calls onSelect with id when clicked", () => {
  const rooms = [
    { id: "r1", name: "General" },
    { id: "r2", name: "Random" },
  ];

  const onSelect = vi.fn();
  render(<RoomSidebar rooms={rooms} onSelect={onSelect} />);

  // Assertion to ensure a room is not empty
  const general = screen.getByText("General");
  expect(general).toBeDefined();

  // Call this room.
  fireEvent.click(general);
  expect(onSelect).toHaveBeenCalledWith("r1");
});
