import { Label } from "@/components/ui/label";
import { ComboBox } from "@/components/utils/comboBox";
import { Catalog } from "@/data/interfaces/Catalog";
import { cn } from "@/lib/utils";

export interface LabelComboBoxProps {
  name: string;
  label: string;
  className?: string;
  selectedType: number;
  fieldTypeCatalog: Array<Catalog<number>>;
}

export function LabelComboBox({
  name,
  label,
  className,
  selectedType,
  fieldTypeCatalog,
}: LabelComboBoxProps) {
  return (
    <div className={cn("flex", "flex-col", "mt-2", className)}>
      <Label className={cn("mb-0")} htmlFor={name}>
        {label}
      </Label>
      <ComboBox
        className={cn("mt-2")}
        placeholder={"Select field type..."}
        searchPlaceholder={"Search field type..."}
        data={fieldTypeCatalog}
        value={selectedType ?? undefined}
        name={name}
        disabled={true}
      />
    </div>
  );
}
