// src/components/login/registerPage.tsx
import { useState } from "react";
import "./login.css";

export default function RegisterPage({
    setLoggedIn,
    setUsername,
}: {
    setLoggedIn: React.Dispatch<React.SetStateAction<boolean>>;
    setUsername: React.Dispatch<React.SetStateAction<string>>;
}) {
    const [localUsername, setLocalUsername] = useState("");
    const [localPassword, setLocalPassword] = useState("");
    const [error, setError] = useState<string>("");

    const handleRegister = async (e: React.FormEvent) => {
        e.preventDefault();

        if (!localUsername.trim() || !localPassword.trim()) {
            setError("Username and password are required.");
            return;
        }

        try {
            const res = await fetch("http://localhost:5019/User/Register", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ UserName: localUsername, Password: localPassword }),

            });

            if (!res.ok) {
                const data = await res.json();
                setError(data.message || "Registration failed");
                return;
            }

            const data = await res.json();
            setUsername(data.username);
            alert(`Account created successfully! Welcome, ${data.username}. Please go to login page to enter in your username and password.`);

        } catch (err) {
            console.error(err);
            setError("Network error");
        }
    };

    return (
        <div className="login-box">
            <h2>Register</h2>
            {error && <p style={{ color: "red" }}>{error}</p>}
            <form onSubmit={handleRegister}>
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
                <button type="submit">Sign Up</button>
                <p className="loginLabel">
                    Already have an account? <a href="/login">Login</a>
                </p>

            </form>
        </div>
    );
}
