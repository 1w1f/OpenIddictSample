import React from "react"
import { RouterProvider, createBrowserRouter, Navigate } from "react-router-dom"
import HomePage from './pages/HomePage'


const App: React.FC = () => {
  const router = createBrowserRouter([{ path: "/", element: <Navigate to='/home' /> }, { path: '/home', element: <HomePage /> }, { path: "*", element: <>404</> }])


  return (
    <RouterProvider router={router} />
  )
}

export default App
