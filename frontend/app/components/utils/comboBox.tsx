"use client";

import * as React from "react";
import { Check, ChevronsUpDown } from "lucide-react";

import { cn } from "@/lib/utils";
import { Button } from "@/components/ui/button";
import {
  Command,
  CommandEmpty,
  CommandGroup,
  CommandInput,
  CommandItem,
  CommandList,
} from "@/components/ui/command";
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from "@/components/ui/popover";
import { Catalog } from "@/data/interfaces/Catalog";

export interface ComboBoxProps<T extends Object> {
  data: Array<Catalog<T>>;
  value?: T;
  name?: string;
  placeholder: string;
  searchPlaceholder: string;
  className?: string | string[];
  disabled?: boolean;
  onSelect?: (data?: Catalog<T> | null) => void;
}

function findDataEntryByValue<T extends Object>(
  data: Catalog<T>[],
  value?: T
): Catalog<T> | undefined {
  return data.find((entry) => entry.id === value);
}

function findDataEntryByLabel<T extends Object>(
  data: Catalog<T>[],
  label?: string
): Catalog<T> | undefined {
  return data.find((entry) => entry.label === label);
}

const ComboBox = <T extends Object>({
  data,
  value,
  name,
  className,
  placeholder = "Select...",
  searchPlaceholder = "Search...",
  disabled = false,
  onSelect,
}: ComboBoxProps<T>) => {
  const [open, setOpen] = React.useState(false);
  const [selectedValue, setSelectedValue] = React.useState<T | undefined>(
    value
  );
  const [comboLabel, setComboLabel] = React.useState(
    findDataEntryByValue(data, value)?.label ?? ""
  );

  function privateOnSelect(currentLabel: string) {
    setComboLabel(currentLabel === comboLabel ? "" : currentLabel);
    const dataEntry = findDataEntryByLabel(data, currentLabel);

    setOpen(false);

    if (name) {
      setSelectedValue(dataEntry?.id as T);
    }

    if (onSelect) {
      onSelect(currentLabel === comboLabel ? null : dataEntry);
    }
  }

  return (
    <div className={cn(className)}>
      <Popover open={open} onOpenChange={setOpen}>
        <PopoverTrigger asChild>
          <Button
            variant="outline"
            role="combobox"
            aria-expanded={open}
            disabled={disabled}
            className="w-[200px] justify-between text-black dark:text-white dark:hover:text-black"
          >
            {findDataEntryByLabel(data, comboLabel)?.label ?? placeholder}
            <ChevronsUpDown className="opacity-50" />
          </Button>
        </PopoverTrigger>
        <PopoverContent className="w-[200px] p-0">
          <Command>
            <CommandInput placeholder={searchPlaceholder} className="h-9" />
            <CommandList>
              <CommandEmpty>No entry found.</CommandEmpty>
              <CommandGroup>
                {data.map((entry) => (
                  <CommandItem
                    key={entry.id.toString()}
                    value={entry.label}
                    onSelect={privateOnSelect}
                    disabled={disabled}
                  >
                    {entry.label}
                    <Check
                      className={cn(
                        "ml-auto",
                        comboLabel === entry.label ? "opacity-100" : "opacity-0"
                      )}
                    />
                  </CommandItem>
                ))}
              </CommandGroup>
            </CommandList>
          </Command>
        </PopoverContent>
      </Popover>
      {name ? (
        <input type="hidden" name={name} value={selectedValue?.toString()} />
      ) : (
        <></>
      )}
    </div>
  );
};

export default ComboBox;
