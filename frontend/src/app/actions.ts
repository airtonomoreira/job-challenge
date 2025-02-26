"use server";

export async function login(formData: FormData) {
  const email = formData.get("username") as string;
  const password = formData.get("password") as string;

  const response = await fetch(
    "http://localhost:5179/api/Auth/generate-token",
    {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        accept: "*/*",
      },
      body: JSON.stringify({ email, password }),
    }
  );

  if (!response.ok) {
    return { error: "Invalid username or password" };
  }

  const data = await response.json();
  const token = data.token;

  if (typeof window !== "undefined") {
    localStorage.setItem("token", token);
  }

  return { success: true };
}
