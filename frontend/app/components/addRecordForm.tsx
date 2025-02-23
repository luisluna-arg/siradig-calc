import { useRef, useState } from "react";
import { Catalog } from "@/data/interfaces/Catalog";
import { cn } from "@/lib/utils";
import { Form, useLoaderData, useNavigate } from "@remix-run/react";
import { RecordsAddLoaderData } from "@/routes/records.add";
import { TemplateSection } from "@/data/interfaces/TemplateSection";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import ComboBox from "./utils/comboBox";
import ActionButton from "./utils/actionButton";

/* TOOD There's an error here 
 * hmr-runtime:266 Warning: A component is changing a controlled input to be uncontrolled. 
 * This is likely caused by the value changing from a defined to undefined, which should not happen. 
 * Decide between using a controlled or uncontrolled input element for the lifetime of the component. 
 * More info: https://reactjs.org/link/controlled-components Error Component Stack
*/

interface LocalLinkProps {
  section: TemplateSection;
  sectionIndex: number;
  className?: string;
}

const Section = ({ section, sectionIndex, className }: LocalLinkProps) => (
  <div className={className}>
    <h2>{section.name}</h2>
    <hr className={cn(["pb-4"])} />
    <table>
      <tbody>
        {section.fields.map((f, i) => {
          const inputName = f.id;
          return (
            <tr key={f.id} className={cn(["h-12"])}>
              <td className={"w-80"}>
                <Label htmlFor={`section[${sectionIndex}].values[${i}].value`}>
                  {f.label}
                </Label>
              </td>
              <td className={"w-80"}>
                <Input
                  type="hidden"
                  name={`section[${sectionIndex}].values[${i}].fieldId`}
                  value={inputName}
                />
                <Input
                  id={`${inputName}`}
                  type="text"
                  name={`section[${sectionIndex}].values[${i}].value`}
                />
              </td>
            </tr>
          );
        })}
      </tbody>
    </table>
  </div>
);

export default function AddRecordForm() {
  const loaderData = useLoaderData() as RecordsAddLoaderData;
  const navigate = useNavigate();
  const formRef = useRef<HTMLFormElement>(null);

  const [selectedTemplate, setSelectedTemplate] = useState(
    loaderData?.template?.id ?? null
  );

  function onSelect(data?: Catalog | null) {
    if (data) {
      setSelectedTemplate(data?.id);
      navigate(`/records/add?templateId=${data?.id}`);
    }
  }

  function handleClear() {
    if (formRef.current) {
      const textInputs = formRef.current.querySelectorAll(
        "input[type='text'],input[type='hidden']"
      );
      textInputs.forEach(
        (input: Element) => ((input as HTMLInputElement).value = "")
      );
    }
  }

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
      <Form ref={formRef} method="post">
        <div className={cn(["flex", "flex-row", "justify-between", "pt-5"])}>
          <div
            className={cn([
              "gap-6",
              "flex",
              "justify-between",
              "gap-4",
              "px-2",
            ])}
          >
            <ComboBox
              placeholder={"Select template..."}
              searchPlaceholder={"Search template..."}
              data={loaderData.templateCatalog}
              value={selectedTemplate}
              name={"templateId"}
              onSelect={onSelect}
            />
          </div>
          <div className={cn(["gap-6", "flex", "justify-between", "gap-4"])}>
            <ActionButton
              type="clear"
              text="Clear"
              onClick={async () => {
                await handleClear();
              }}
            />
            <ActionButton type="submit" text="Submit" />
          </div>
        </div>
        <div
          className={cn([
            "flex",
            "flex-row",
            "justify-between",
            "pt-5",
            "px-3",
          ])}
        >
          <table>
            <tbody>
              <tr className={cn(["h-12"])}>
                <td className={"w-80"}>
                  <Label htmlFor={`title`}>Title</Label>
                </td>
                <td className={"w-80"}>
                  <Input id={`title`} type="text" name={`title`} />
                </td>
              </tr>
            </tbody>
          </table>
        </div>
        <div className={cn(["grid", "grid-cols-2", "gap-6"])}>
          {loaderData?.template?.sections?.map((s, i) => (
            <Section
              key={s.id}
              section={s}
              sectionIndex={i}
              className={"p-4"}
            />
          ))}
        </div>
      </Form>
    </div>
  );
}
