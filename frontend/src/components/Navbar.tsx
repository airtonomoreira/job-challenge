"use client";
import { useEffect, useState } from "react";
import Link from "next/link";
import { useRouter } from "next/navigation";
import { Card } from "./ui/card";
import { Button } from "./ui/button";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuTrigger,
} from "./ui/dropdown-menu";
import { Menu } from "lucide-react";

const Navbar = () => {
  const router = useRouter();
  const [isAuthenticated, setIsAuthenticated] = useState(false);

  useEffect(() => {
    const token = localStorage.getItem("token");
    setIsAuthenticated(!!token);
  }, []);

  const handleLogin = () => {
    localStorage.setItem("token", "fake-token");
    setIsAuthenticated(true);
    router.push("/");
  };

  const handleLogout = () => {
    localStorage.removeItem("token");
    setIsAuthenticated(false);
    router.push("/login");
  };

  const pages = [{ id: "1", title: "Employee", route: "/employee" }];

  return (
    <Card className="container bg-card py-3 px-4 border-0 flex items-center justify-between gap-6 rounded-2xl mt-5">
      <Link href="/" className="text-primary cursor-pointer font-bold">
        K2Partinering
      </Link>

      <div className="flex items-center gap-10">
        {isAuthenticated && (
          <ul className="hidden md:flex items-center gap-10 text-card-foreground">
            {pages.map((page) => (
              <li key={page.id}>
                <Link href={page.route} className="hover:text-primary">
                  {page.title}
                </Link>
              </li>
            ))}
          </ul>
        )}

        <div className="flex items-center">
          {isAuthenticated ? (
            <Button
              variant="secondary"
              className="hidden md:block px-2"
              onClick={handleLogout}
            >
              Logout
            </Button>
          ) : (
            <Button
              variant="secondary"
              className="hidden md:block px-2"
              onClick={handleLogin}
            >
              Login
            </Button>
          )}

          <div className="flex md:hidden mr-2 items-center gap-2">
            <DropdownMenu>
              <DropdownMenuTrigger asChild>
                <Button variant="outline" size="icon">
                  <Menu className="h-5 w-5" />
                </Button>
              </DropdownMenuTrigger>
              <DropdownMenuContent align="end">
                {pages.map((page) => (
                  <DropdownMenuItem key={page.id}>
                    <Link href={page.route} className="w-full">
                      {page.title}
                    </Link>
                  </DropdownMenuItem>
                ))}
                {isAuthenticated ? (
                  <DropdownMenuItem onClick={handleLogout}>
                    Logout
                  </DropdownMenuItem>
                ) : (
                  <DropdownMenuItem onClick={handleLogin}>
                    Login
                  </DropdownMenuItem>
                )}
              </DropdownMenuContent>
            </DropdownMenu>
          </div>
        </div>
      </div>
    </Card>
  );
};

export default Navbar;
