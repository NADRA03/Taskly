import { useState } from "react";

export default function Chatbot() {
  const [messages, setMessages] = useState<string[]>(["👋 Hi! I’m Taskly AI. How would you like to customize your task manager?"]);
  const [input, setInput] = useState("");

  const sendMessage = () => {
    if (!input.trim()) return;
    setMessages([...messages, `You: ${input}`, `🤖 AI: I'll adjust your theme to "${input}"!`]);
    setInput("");
  };

  return (
    <div className="flex flex-col h-screen bg-gradient-to-br from-slate-900 via-purple-900 to-slate-800 text-white">
      <header className="p-4 text-lg font-semibold bg-white/10 backdrop-blur-lg">Taskly Chatbot</header>
      <main className="flex-1 overflow-y-auto p-6 space-y-4">
        {messages.map((msg, i) => (
          <div
            key={i}
            className={`p-3 rounded-xl max-w-[75%] ${msg.startsWith("You:") ? "bg-blue-500 ml-auto" : "bg-gray-700"}`}
          >
            {msg}
          </div>
        ))}
      </main>
      <footer className="p-4 flex gap-2 bg-white/10 backdrop-blur-lg">
        <input
          type="text"
          value={input}
          onChange={(e) => setInput(e.target.value)}
          className="flex-1 px-4 py-2 rounded-lg bg-white/20 outline-none placeholder-gray-300"
          placeholder="Type a message..."
        />
        <button
          onClick={sendMessage}
          className="px-4 py-2 rounded-lg bg-gradient-to-r from-purple-500 to-pink-500 font-semibold"
        >
          Send
        </button>
      </footer>
    </div>
  );
}
