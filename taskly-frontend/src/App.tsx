import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import './App.css';
import './output.css';

import TaskManager from './pages/TaskManager';

function App() {
  return (
    <TaskManager/>
  );
}

export default App;
