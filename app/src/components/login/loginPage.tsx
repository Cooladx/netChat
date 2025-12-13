// src/components/login/loginPage.tsx
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import "./login.css";

export default function LoginPage({
  setLoggedIn,
  setUsername,
  setCurrentUserId,
}: {
  setLoggedIn: React.Dispatch<React.SetStateAction<boolean>>;
  setUsername: React.Dispatch<React.SetStateAction<string>>;
  setCurrentUserId: React.Dispatch<React.SetStateAction<string>>;
}) {
  const [localUsername, setLocalUsername] = useState("");
  const [localPassword, setLocalPassword] = useState("");
  const [error, setError] = useState("");
  const navigate = useNavigate(); 

  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!localUsername.trim() || !localPassword.trim()) {
      setError("Please enter username and password");
      return;
    }

    try {
      const res = await fetch("http://localhost:5019/User/Login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ UserName: localUsername, password: localPassword }),
      });

      if (!res.ok) {
        const data = await res.json();
        setError(data.message || "Login failed");
        return;
      }

      const data = await res.json();

      // Set global state
      setUsername(data.username);
      setCurrentUserId(data.userId);
      setLoggedIn(true);

      // Redirect to home immediately
      navigate("/home"); 
    } catch (err) {
      console.error(err);
      setError("Network error");
    }
  };

  return (
    <div className="login-box">
      <h2>Login Form</h2>
      {error && <p style={{ color: "red" }}>{error}</p>}
      <form onSubmit={handleLogin}>
        <input
          type="text"
          placeholder="Username"
          value={localUsername}
          onChange={(e) => setLocalUsername(e.target.value)}
        />
        <input
          type="password"
          placeholder="Password"
          value={localPassword}
          onChange={(e) => setLocalPassword(e.target.value)}
        />
        <br />
        <button type="submit">Login</button>
        <p className="loginLabel">
          Don't have an account? <a href="/register">Sign up</a>
        </p>
      </form>
    </div>
  );
}
