import { useState } from "react";

export default function SignUp() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirm, setConfirm] = useState("");

  return (
    <div className="flex items-center justify-center h-screen w-full max-w-full">
      <div className="w-full max-w-md backdrop-blur-lg p-8 text-white">
        <h1 className="text-3xl font-bold text-center mb-6">Taskly Sign Up</h1>
        <form className="space-y-4">
          <input
            type="email"
            placeholder="Email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            className="w-full px-4 py-2 rounded-lg"
          />
          <input
            type="password"
            placeholder="Password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            className="w-full px-4 py-2 rounded-lg"
          />
          <input
            type="password"
            placeholder="Confirm Password"
            value={confirm}
            onChange={(e) => setConfirm(e.target.value)}
            className="w-full px-4 py-2 rounded-lg"
          />
          <button
            type="submit"
            className="w-full py-2"
          >
            Sign Up
          </button>
        </form>
        <p className="text-center text-sm mt-4 opacity-80">
          Already have an account? <span className="underline cursor-pointer">Log in</span>
        </p>
      </div>
    </div>
  );
}
