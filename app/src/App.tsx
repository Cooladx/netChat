import { useState } from "react";
import "./App.css";
import * as signalR from "@microsoft/signalr";
import Button from "./components/websocketButton";
function App() {
  const [count, setCount] = useState(0);

 
  return (
    <>
      <div>
        <Button />
      </div>
    </>
  );
}

export default App;
