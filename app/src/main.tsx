import { StrictMode, useState } from "react";
import { createRoot } from "react-dom/client";
import { createBrowserRouter, RouterProvider, Navigate } from "react-router";

import "./index.css";
import App from "./App.tsx";
import RegisterPage from "./components/login/registerPage.tsx";
import LoginPage from "./components/login/loginPage.tsx";
import NotFoundPage from "./components/NotFoundPage/notFound.tsx";

function Root() {
  const [loggedIn, setLoggedIn] = useState(false);
  const [username, setUsername] = useState("");
  const [currentUserId, setCurrentUserId] = useState("");

  const router = createBrowserRouter([
    { path: "/", element: <Navigate to="/home" replace /> },

    {
      path: "/home",
      element: loggedIn ? (
        <App
          loggedIn={loggedIn}
          setLoggedIn={setLoggedIn}
          username={username}
          setUsername={setUsername}
          currentUserId={currentUserId}
          setCurrentUserId={setCurrentUserId}
        />
      ) : (
        <Navigate to="/login" replace />
      ),
    },

    {
      path: "/home/:roomId",
      element: loggedIn ? (
        <App
          loggedIn={loggedIn}
          setLoggedIn={setLoggedIn}
          username={username}
          setUsername={setUsername}
          currentUserId={currentUserId}
          setCurrentUserId={setCurrentUserId}
        />
      ) : (
        <Navigate to="/login" replace />
      ),
    },

    { path: "/login", element: <LoginPage setLoggedIn={setLoggedIn} setUsername={setUsername} setCurrentUserId={setCurrentUserId} /> },
    { path: "/register", element: <RegisterPage setLoggedIn={setLoggedIn} setUsername={setUsername} /> },
    { path: "*", element: <NotFoundPage setLoggedIn={setLoggedIn} /> },
  ]);

  return <RouterProvider router={router} />;
}

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <Root />
  </StrictMode>,
);
