import { useState } from "react";

interface Task {
  id: number;
  text: string;
  done: boolean;
}

export default function TaskManager() {
  const [tasks, setTasks] = useState<Task[]>([]);
  const [input, setInput] = useState("");

  const addTask = () => {
    if (!input.trim()) return;
    setTasks([...tasks, { id: Date.now(), text: input, done: false }]);
    setInput("");
  };

  const toggleTask = (id: number) => {
    setTasks(tasks.map((t) => (t.id === id ? { ...t, done: !t.done } : t)));
  };

  return (
    <div className="flex flex-col min-h-screen w-full max-w-full mx-0 text-white p-6">
      <header className="title">Taskly Manager</header>

      <main className="flex-1 mt-6 space-y-3 overflow-y-auto">
        {tasks.map((task) => (
          <div
            key={task.id}
            onClick={() => toggleTask(task.id)}
            className={`p-3 rounded-lg cursor-pointer transition-all duration-200 shadow-sm ${
              task.done
                ? "bg-green-600 line-through opacity-80"
                : "bg-gray-700 hover:bg-gray-600"
            }`}
          >
            {task.text}
          </div>
        ))}
      </main>

        <footer className="mt-4 flex gap-3 w-full max-w-full">
        <input
            type="text"
            value={input}
            onChange={(e) => setInput(e.target.value)}
            placeholder="Add a new task..."
            className="input flex-1"
        />
        <button onClick={addTask} className="button">
            Add
        </button>
        </footer>
    </div>
  );
}
