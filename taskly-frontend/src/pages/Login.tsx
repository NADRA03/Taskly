import { useState } from "react";

export default function Login() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  return (
    <div className="flex items-center justify-center h-screen bg-gradient-to-br from-indigo-600 via-purple-600 to-pink-500">
      <div className="w-full max-w-md bg-white/10 backdrop-blur-lg rounded-2xl shadow-2xl p-8 text-white">
        <h1 className="text-3xl font-bold text-center mb-6">Taskly Login</h1>
        <form className="space-y-4">
          <input
            type="email"
            placeholder="Email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            className="w-full px-4 py-2 rounded-lg bg-white/20 focus:bg-white/30 outline-none placeholder-gray-300"
          />
          <input
            type="password"
            placeholder="Password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            className="w-full px-4 py-2 rounded-lg bg-white/20 focus:bg-white/30 outline-none placeholder-gray-300"
          />
          <button
            type="submit"
            className="w-full py-2 rounded-lg bg-gradient-to-r from-pink-500 to-purple-600 font-semibold hover:scale-[1.02] transition"
          >
            Sign In
          </button>
        </form>
        <p className="text-center text-sm mt-4 opacity-80">
          Don’t have an account? <span className="underline cursor-pointer">Sign up</span>
        </p>
      </div>
    </div>
  );
}
