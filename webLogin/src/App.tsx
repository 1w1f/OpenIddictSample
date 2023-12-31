import React from "react";
import {
  RouterProvider,
  createBrowserRouter,
  Navigate,
} from "react-router-dom";
import LoginPage from "./pages/LoginPage";
import "./css/index.css";
import ConsentPage from "./pages/ConsentPage";

const App: React.FC = () => {
  const router = createBrowserRouter([
    { path: "/", element: <Navigate to="/login" /> },
    { path: "/login", element: <LoginPage /> },
    { path: "/consent", element: <ConsentPage /> },
    { path: "*", element: <>404</> },
  ]);

  return <RouterProvider router={router} />;
};

export default App;
