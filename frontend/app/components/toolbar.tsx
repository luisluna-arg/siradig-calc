import { ModeToggle } from "@/components/mode-toggle";
import { cn } from "@/lib/utils";
import { hasHandleWithTitle } from "@/utils";
import { NavLink, useLocation, useMatches } from "@remix-run/react";
import { ReactNode } from "react";
import {
  NavigationMenu,
  NavigationMenuItem,
  NavigationMenuList,
} from "@/components/ui/navigation-menu";
import NavigationTabs from "@/constants/NavigationTabs";

interface LocalLinkProps {
  to: string;
  children: ReactNode;
}
const LocalLink = ({ children, to }: LocalLinkProps) => (
  <NavLink
    to={to}
    className={({ isActive }) => (isActive ? "nav-link active" : "nav-link")}
  >
    {children}
  </NavLink>
);

export function Toolbar() {
  const matches = useMatches();
  const location = useLocation();
  const currentMatch = matches.find(hasHandleWithTitle);
  const title = currentMatch?.handle.title || "";

  return (
    <div
      className={cn([
        "flex",
        "flex-row",
        "w-full",
        "border-solid",
        "background-card",
        "p-2",
        "items-center",
        "bg-card",
        "text-foreground",
        "border-b",
      ])}
    >
      <NavLink to={"/"}>
        <span className={cn(["text-lg", "font-bold", "mx-2"])}>
          Siradig Calculator {` | ${title}`}
        </span>
      </NavLink>
      <div className={cn(["justify-center", "flex"])}>
        <NavigationMenu>
          <NavigationMenuList>
            {NavigationTabs.map((tab) => (
              <NavigationMenuItem
                key={tab.id}
                className={cn([
                  "px-1 py-2 rounded-md transition-colors",
                  location.pathname === tab.route
                    ? "text-primary"
                    : "hover:text-primary",
                ])}
              >
                <LocalLink to={tab.route}>{tab.label}</LocalLink>
              </NavigationMenuItem>
            ))}
          </NavigationMenuList>
        </NavigationMenu>
      </div>
      <div className={cn(["ml-auto"])}>
        <ModeToggle />
      </div>
    </div>
  );
}
