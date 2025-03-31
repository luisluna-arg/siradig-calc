export interface ActionButtonProps {
  name: string;
  defaultValue?: string;
}

export function Hidden({ name, defaultValue }: ActionButtonProps) {
  return <input type="hidden" name={name} defaultValue={defaultValue} />;
}
