"use client"; 

import Link from "next/link";
import { useRouter } from "next/navigation";
import {
  Card,
  CardHeader,
  CardTitle,
  CardDescription,
  CardContent,
  CardFooter,
} from "@/components/ui/card";
import { Label } from "@/components/ui/label";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";

export default function LoginPage({ searchParams }: any) {
  const router = useRouter();
  const errormsg = searchParams?.error ? (
    <p className="bg-red-200 p-2 rounded-md">Invalid username or password</p>
  ) : (
    "Enter your email and password to access your account."
  );

  const handleLogin = async (formData: FormData) => {
    const email = formData.get("username") as string;
    const password = formData.get("password") as string;

    try {
      const response = await fetch(
        "http://localhost:5179/api/Auth/generate-token",
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({ email, password }),
        }
      );

      if (response.ok) {
        const data = await response.json();
        localStorage.setItem("token", data.token); 
        router.push("/employee"); 
      } else {
        router.push("/login?error=1");
      }
    } catch (err) {
      router.push("/login?error=1");
    }
  };

  return (
    <div className="flex items-center justify-center min-h-screen bg-background">
      <Card className="w-full max-w-md p-6 md:p-8">
        <CardHeader className="space-y-2">
          <CardTitle className="text-2xl font-bold">K2Partinering</CardTitle>
          <CardDescription>{errormsg}</CardDescription>
        </CardHeader>
        <form action={handleLogin}>
          <CardContent className="space-y-4">
            <div className="space-y-2">
              <Label htmlFor="username">Email</Label>
              <Input
                id="username"
                type="text"
                name="username"
                placeholder="Enter email"
                required
              />
            </div>
            <div className="space-y-2">
              <div className="flex items-center justify-between">
                <Label htmlFor="password">Password</Label>
              </div>
              <Input
                id="password"
                type="password"
                name="password"
                placeholder="Enter password"
                required
              />
            </div>
          </CardContent>
          <CardFooter>
            <Button type="submit">Sign in</Button>
          </CardFooter>
        </form>
      </Card>
    </div>
  );
}
