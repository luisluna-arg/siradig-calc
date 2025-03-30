import { useEffect, useRef, useState } from "react";
import {
  Form,
  useActionData,
  useLoaderData,
  useNavigate,
} from "@remix-run/react";
import { cn } from "@/lib/utils";
import { Catalog } from "@/data/interfaces/Catalog";
import { TemplateSection } from "@/data/interfaces/TemplateSection";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Record as DataRecord } from "@/data/interfaces/Record";
import { Template } from "@/data/interfaces/Template";
import { useToast } from "@/hooks/use-toast";
import ActionButton from "./utils/actionButton";
import ComboBox from "./utils/comboBox";
import Hidden from "./utils/hidden";
import { showToast } from "@/utils/route/form";

/* TOOD There's an error here
 * hmr-runtime:266 Warning: A component is changing a controlled input to be uncontrolled.
 * This is likely caused by the value changing from a defined to undefined, which should not happen.
 * Decide between using a controlled or uncontrolled input element for the lifetime of the component.
 * More info: https://reactjs.org/link/controlled-components Error Component Stack
 */

interface LocalLinkProps {
  className?: string;
  section: TemplateSection;
  sectionIndex: number;
  editingRecord?: DataRecord;
}

export default function RecordEditForm() {
  const navigate = useNavigate();

  const { toast } = useToast();

  const { record, template, templateCatalog } = useLoaderData() as {
    record?: DataRecord;
    template?: Template;
    templateCatalog?: any;
  };

  const actionData = useActionData() as any;

  const formRef = useRef<HTMLFormElement>(null);

  const [selectedTemplate, setSelectedTemplate] = useState(
    template?.id ?? null
  );

  const [editingRecord, _] = useState(record);

  function onSelect(data?: Catalog<string> | null) {
    if (data) {
      setSelectedTemplate(data?.id);
      const url = new URL(window.location.href);
      url.searchParams.set("templateId", data?.id);
      navigate(`${url.pathname}${url.search}`);
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

  useEffect(() => {
    if ((actionData && actionData.status !== 200) || actionData?.errorData) {
      showToast(toast, actionData);
    }
  }, [actionData, toast]); // Runs only when `error` changes

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
      <Form ref={formRef} method={"post"}>
        <Hidden name={`id`} defaultValue={editingRecord?.id} />
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
              data={templateCatalog}
              value={selectedTemplate ?? undefined}
              name={"templateId"}
              onSelect={onSelect}
            />
          </div>
          <div className={cn(["gap-6", "flex", "justify-between", "gap-4"])}>
            <ActionButton
              type="clear"
              text="Clear"
              onClick={async (e) => {
                e.preventDefault;
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
                  <Input
                    id={`title`}
                    type="text"
                    name={`title`}
                    defaultValue={editingRecord?.title}
                  />
                </td>
              </tr>
            </tbody>
          </table>
        </div>
        <div className={cn(["grid", "grid-cols-2", "gap-6"])}>
          {template?.sections?.map((s, i) => (
            <Section
              key={s.id}
              className={"p-4"}
              section={s}
              sectionIndex={i}
              editingRecord={editingRecord}
            />
          ))}
        </div>
      </Form>
    </div>
  );
}

const Section = ({
  section,
  sectionIndex,
  className,
  editingRecord,
}: LocalLinkProps) => {
  const getInputName = (valueIndex: number, attribute: string) =>
    `section[${sectionIndex}].values[${valueIndex}].${attribute}`;
  return (
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
                  <Label htmlFor={getInputName(i, `value`)}>{f.label}</Label>
                </td>
                <td className={"w-80"}>
                  <Hidden
                    name={getInputName(i, `valueId`)}
                    defaultValue={fieldValue?.id}
                  />
                  <Hidden
                    name={getInputName(i, `fieldId`)}
                    defaultValue={fieldValue?.fieldId}
                  />
                  <Input
                    type="text"
                    name={getInputName(i, `value`)}
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
};
