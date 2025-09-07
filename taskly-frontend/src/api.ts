const API_URL = "https://http://localhost:5193/api/auth"; // Adjust if needed

// ---------------- Login ----------------
export async function login(email: string, password: string) {
  try {
    const res = await fetch(`${API_URL}/login`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ email, password }),
    });

    if (!res.ok) {
      const err = await res.json();
      throw new Error(err.message || "Login failed");
    }

    const data = await res.json();
    // Save token and user ID
    localStorage.setItem("taskly_token", data.access_token);
    localStorage.setItem("taskly_user_id", data.user);
    return data;
  } catch (err: any) {
    console.error("Login error:", err);
    throw err;
  }
}

// ---------------- Sign Up ----------------
export async function signup(email: string, password: string) {
  try {
    const res = await fetch(`${API_URL}/signup`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ email, password }),
    });

    if (!res.ok) {
      const err = await res.json();
      throw new Error(err.message || "Sign up failed");
    }

    const data = await res.json();
    // Optionally save user info
    localStorage.setItem("taskly_user_id", data.user);
    return data;
  } catch (err: any) {
    console.error("Sign up error:", err);
    throw err;
  }
}

// ---------------- Helper: Auth Header ----------------
export function authHeader() {
  const token = localStorage.getItem("taskly_token");
  return token ? { Authorization: `Bearer ${token}` } : {};
}
