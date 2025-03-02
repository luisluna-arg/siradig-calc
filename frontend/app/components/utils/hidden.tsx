export interface ActionButtonProps {
  name: string;
  defaultValue?: string;
}

const Hidden = ({ name, defaultValue }: ActionButtonProps) => {
  return <input type="hidden" name={name} defaultValue={defaultValue} />;
};

export default Hidden;
