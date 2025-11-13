// src/components/sidebar/userSidebar.tsx
export type User = { id: string; name: string };

export default function UserSidebar({ users = [] }: { users?: User[] }) {
  return (
    <aside>
      <h2>Users</h2>
      <ul>
        {users.map((u) => (
          <li key={u.id}>{u.name}</li>
        ))}
      </ul>
    </aside>
  );
}
