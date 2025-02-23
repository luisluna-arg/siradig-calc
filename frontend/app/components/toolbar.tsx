import { ModeToggle } from "@/components/mode-toggle";
import { cn } from "@/lib/utils";
import { hasHandleWithTitle } from "@/utils";
import { NavLink, useMatches } from "@remix-run/react";
import { ReactNode } from "react";
import {
  NavigationMenu,
  NavigationMenuItem,
  NavigationMenuList,
} from "./ui/navigation-menu";

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
        <span className={cn(["text-lg", "font-bold", "ml-4"])}>
          Siradig Calculator {` | ${title}`}
        </span>
      </NavLink>
      <div className={cn(["justify-center", "flex"])}>
        <NavigationMenu>
          <NavigationMenuList>
            <NavigationMenuItem className={cn(["pl-2"])}>
              <LocalLink to={"/records"}>Records</LocalLink>
            </NavigationMenuItem>
            <NavigationMenuItem className={cn(["pl-2"])}>
              <LocalLink to={"/records/templates"}>Templates</LocalLink>
            </NavigationMenuItem>
            <NavigationMenuItem className={cn(["pl-2"])}>
              <LocalLink to={"/records/conversions"}>Conversions</LocalLink>
            </NavigationMenuItem>
            <NavigationMenuItem className={cn(["pl-2"])}>
              <LocalLink to={"/records/templates/links"}>Links</LocalLink>
            </NavigationMenuItem>
          </NavigationMenuList>
        </NavigationMenu>
      </div>
      <div className={cn(["ml-auto"])}>
        <ModeToggle />
      </div>
    </div>
  );
}
