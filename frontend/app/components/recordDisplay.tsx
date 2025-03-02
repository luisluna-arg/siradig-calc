import { useLoaderData } from "@remix-run/react";
import { Record as DataRecord } from "@/data/interfaces/Record";
import { Label } from "@/components/ui/label";
import { Input } from "@/components/ui/input";
import { cn } from "@/lib/utils";
import { TemplateSection } from "@/data/interfaces/TemplateSection";

export default function RecordDisplay() {
  const { record } = useLoaderData() as {
    isEdit: boolean;
    record: DataRecord;
  };

  const inputContainerClasses = cn([
    "flex",
    "items-center",
    "gap-4",
    "pt-5",
    "px-3",
  ]);
  const inputLabelClasses = cn([
    "flex",
    "w-24 h-full",
    "items-center",
    "justify-end",
  ]);

  return (
    <div
      className={cn([
        "flex",
        "justify-center",
        "items-center",
        "flex-col",
        "h-full",
      ])}
    >
      <div className={inputContainerClasses}>
        <Label htmlFor={`name`} className={inputLabelClasses}>
          Template
        </Label>
        <Input
          id={`name`}
          type="text"
          name={`name`}
          readOnly={true}
          defaultValue={record.template.name}
        />
        <Input
          id={`description`}
          type="text"
          name={`description`}
          readOnly={true}
          defaultValue={record.template.description}
        />
      </div>
      <div className={inputContainerClasses}>
        <Label htmlFor={`title`} className={inputLabelClasses}>
          Title
        </Label>
        <Input
          id={`title`}
          type="text"
          name={`title`}
          readOnly={true}
          defaultValue={record?.title}
        />
      </div>
      <div className={cn(["grid", "grid-cols-2", "gap-6"])}>
        {record.template?.sections?.map((s, i) => (
          <Section
            key={s.id}
            className={"p-4"}
            section={s}
            sectionIndex={i}
            editingRecord={record}
          />
        ))}
      </div>
    </div>
  );
}

interface LocalLinkProps {
  className?: string;
  section: TemplateSection;
  sectionIndex: number;
  editingRecord?: DataRecord;
}

const Section = ({
  section,
  sectionIndex,
  className,
  editingRecord,
}: LocalLinkProps) => (
  <div className={className}>
    <h2>{section.name}</h2>
    <hr className={cn(["pb-4"])} />
    <table>
      <tbody>
        {section.fields.map((f, i) => {
          const fieldValue = editingRecord?.values.find(
            (v) => v.fieldId == f.id
          );
          let value = fieldValue?.value;
          return (
            <tr key={f.id} className={cn(["h-12"])}>
              <td className={"w-80"}>
                <Label htmlFor={`section[${sectionIndex}].values[${i}].value`}>
                  {f.label}
                </Label>
              </td>
              <td className={"w-80"}>
                <Input
                  type="text"
                  readOnly={true}
                  name={`section[${sectionIndex}].values[${i}].value`}
                  defaultValue={value}
                />
              </td>
            </tr>
          );
        })}
      </tbody>
    </table>
  </div>
);
