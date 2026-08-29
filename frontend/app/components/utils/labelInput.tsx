import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { cn } from "@/lib/utils";

export interface LabelInputProps {
  id?: string;
  label: string;
  className?: string;
  type?: string;
  defaultValue?: string;
  disabled?: boolean;
}

export function LabelInput({
  id,
  label,
  type = "text",
  className,
  defaultValue,
  disabled = false,
}: LabelInputProps) {
  return (
    <div className={cn(className)}>
      <Label htmlFor={id}>{label}</Label>
      <Input
        name={id}
        type={type}
        defaultValue={defaultValue}
        className={"mt-2"}
        disabled={disabled}
      />
    </div>
  );
}
