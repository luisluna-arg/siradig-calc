import { Catalog } from "@/data/interfaces/Catalog";

export function enumToValueLabel<T extends object>(enumObj: T): Catalog[] {
  return (
    Object.keys(enumObj)
      // Filter out numeric keys (useful for numeric enums)
      .filter((key) => isNaN(Number(key)))
      .map((key) => ({
        id: enumObj[key as keyof T] as string,
        label: key,
      }))
  );
}
