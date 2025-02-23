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

export interface ComboBoxProps {
  data: Array<Catalog>;
  value?: string;
  name?: string;
  placeholder: string;
  searchPlaceholder: string;
  onSelect?: ((data?: Catalog | null) => void) | undefined;
}

function findDataEntryByValue(data: Array<Catalog>, value?: string) {
  return data.find((entry) => entry.id === value);
}

function findDataEnryByLabel(data: Array<Catalog>, label?: string) {
  return data.find((entry) => entry.label === label);
}

const ComboBox = ({
  data,
  value,
  name,
  placeholder = "Select...",
  searchPlaceholder = "Search...",
  onSelect,
}: ComboBoxProps) => {
  const [open, setOpen] = React.useState(false);
  const [selectedValue, setSelectedValue] = React.useState(value);
  const [comboLabel, setComboValue] = React.useState(
    findDataEntryByValue(data, value)?.label ?? ""
  );

  function privateOnSelect(currentLabel: string) {
    setComboValue(currentLabel === comboLabel ? "" : currentLabel);
    const dataEntry = findDataEnryByLabel(data, currentLabel);

    setOpen(false);

    if (name) {
      setSelectedValue(dataEntry?.id);
    }

    if (onSelect) {
      onSelect(currentLabel === comboLabel ? null : dataEntry);
    }
  }

  return (
    <div>
      <Popover open={open} onOpenChange={setOpen}>
        <PopoverTrigger asChild>
          <Button
            variant="outline"
            role="combobox"
            aria-expanded={open}
            className="w-[200px] justify-between"
          >
            {findDataEnryByLabel(data, comboLabel)?.label ?? placeholder}
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
                    key={entry.id}
                    value={entry.label}
                    onSelect={privateOnSelect}
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
      {name ? <input type="hidden" name={name} value={selectedValue} /> : <></>}
    </div>
  );
};

export default ComboBox;
