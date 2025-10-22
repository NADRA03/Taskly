import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import "./App.css";
import "./output.css";

import TaskManager from "./pages/TaskManager";
import Login from "./pages/Login";
import SignUp from "./pages/SignIn";

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/login" element={<Login />} />
        <Route path="/" element={<TaskManager />} />
        <Route path="/signup" element={<SignUp />} />
      </Routes>
    </Router>
  );
}

export default App;
