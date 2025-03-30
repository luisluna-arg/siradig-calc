import { Button } from "@/components/ui/button";
import { cn } from "@/lib/utils";
import {
  CirclePlus,
  Eraser,
  PencilIcon,
  SendHorizonal,
  Trash2Icon,
} from "lucide-react";

export type ActionButtonType = "add" | "edit" | "delete" | "submit" | "clear";

const icons: Record<ActionButtonType, JSX.Element> = {
  add: <CirclePlus />,
  edit: <PencilIcon />,
  delete: <Trash2Icon />,
  submit: <SendHorizonal />,
  clear: <Eraser />,
};

const commonButtonClass = cn([
  "text-gray-500",
  "dark:text-gray-500",
  "hover:text-white",
  "dark:hover:text-white",
]);

const buttonClasses: Record<ActionButtonType, Array<string>> = {
  add: ["bg-green-100", "hover:bg-green-400", commonButtonClass],
  edit: ["bg-yellow-100", "hover:bg-yellow-400", commonButtonClass],
  submit: ["bg-green-100", "hover:bg-green-400", commonButtonClass],
  clear: ["bg-amber-100", "hover:bg-amber-400", commonButtonClass],
  delete: ["bg-red-300", "hover:bg-red-600", commonButtonClass],
};

export interface ActionButtonProps {
  type: ActionButtonType;
  onClick?: (e?: any) => Promise<void>;
  text?: string;
}

const ActionButton = ({ type, text, onClick }: ActionButtonProps) => {
  const icon = icons[type];

  const classes = buttonClasses[type];

  return (
    <Button
      variant={"outline"}
      size={text ? undefined : "icon"}
      className={cn(classes)}
      onClick={onClick}
      type={type === "submit" ? type : "button"}
    >
      {icon} {text ?? ""}
    </Button>
  );
};

export default ActionButton;
