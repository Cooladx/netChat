// src/components/header/header.tsx

import "./style.css";
export default function Header({ text }: { text: string }) {
  return (
    <>
      <h1> {text} </h1>
    </>
  );
}
