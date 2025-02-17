import { Button } from "@/components/ui/button";
import { cn } from "@/lib/utils";
import { CirclePlus, Trash2Icon } from "lucide-react";

export type ActionButtonType = "add" | "delete";

const icons: Record<ActionButtonType, JSX.Element> = {
  add: <CirclePlus />,
  delete: <Trash2Icon />,
};

const buttonClasses: Record<ActionButtonType, Array<string>> = {
  add: ["hover:bg-green-400", "hover:text-white"],
  delete: ["hover:bg-red-600", "hover:text-white"],
};

export interface ActionButtonProps {
  type: ActionButtonType;
  onClick?: (e?: any) => Promise<void>;
}

const ActionButton = ({ type, onClick }: ActionButtonProps) => {
  const icon = icons[type];

  const classes = buttonClasses[type];

  return (
    <Button
      variant={"outline"}
      size="icon"
      className={cn(classes)}
      onClick={onClick}
    >
      {icon}
    </Button>
  );
};

export default ActionButton;
