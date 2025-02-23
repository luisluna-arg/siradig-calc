import { Button } from "@/components/ui/button";
import { cn } from "@/lib/utils";
import { CirclePlus, Eraser, SendHorizonal, Trash2Icon } from "lucide-react";

export type ActionButtonType = "add" | "delete" | "submit" | "clear";

const icons: Record<ActionButtonType, JSX.Element> = {
  add: <CirclePlus />,
  delete: <Trash2Icon />,
  submit: <SendHorizonal />,
  clear: <Eraser />
};

const buttonClasses: Record<ActionButtonType, Array<string>> = {
  add: ["bg-green-100", "hover:bg-green-400", "hover:text-white"],
  delete: ["bg-red-100", "hover:bg-red-600", "hover:text-white"],
  submit: ["bg-green-100", "hover:bg-green-600", "hover:text-white"],
  clear: ["bg-amber-100", "hover:bg-amber-400", "hover:text-white"],
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
      type={type === "submit" ? type : undefined}
    >
      {icon} {text ?? ""}
    </Button>
  );
};

export default ActionButton;
