import { Checkbox } from "@/components/ui/checkbox";
import { Label } from "@/components/ui/label";
import { cn } from "@/lib/utils";

export interface LabelCheckboxProps {
  name: string;
  label: string;
  checked: boolean;
  className?: string;
}

export function LabelCheckbox({
  name,
  label,
  checked = false,
  className,
}: LabelCheckboxProps) {
  return (
    <div className={cn("flex", "flex-col", "mt-2", "ml-4", className)}>
      <Label htmlFor={name}>{label}</Label>
      <Checkbox
        name={name}
        defaultChecked={checked}
        className={"mt-1"}
        disabled={true}
      />
    </div>
  );
}
