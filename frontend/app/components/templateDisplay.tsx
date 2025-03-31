import { useEffect, useRef, useState } from "react";
import { Form, useActionData, useLoaderData } from "@remix-run/react";
import { Card, CardContent } from "@/components/ui/card";
import { Checkbox } from "@/components/ui/checkbox";
import { ComboBox } from "@/components/utils/comboBox";
import { Hidden } from "@/components/utils/hidden";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Separator } from "@/components/ui/separator";
import { Catalog } from "@/data/interfaces/Catalog";
import { RecordField } from "@/data/interfaces/RecordField";
import { Template } from "@/data/interfaces/Template";
import { TemplateSection } from "@/data/interfaces/TemplateSection";
import { useToast } from "@/hooks/use-toast";
import { cn } from "@/lib/utils";
import { showToast } from "@/utils/route/form";

interface LocalLinkProps {
  className?: string;
  section: TemplateSection;
  sectionIndex: number;
  editingTemplate?: Template;
  fieldTypeCatalog: Array<Catalog<number>>;
}

const fontsClass = cn("h-12");
const gapClass = cn("gap-2");
const titleClass = cn("font-semibold", "leading-none", "tracking-tight");
const verticalMargin4Class = cn("my-4");
const marginBottom4Class = cn("mb-4");
const centerClass = cn("flex", "items-center");
const columnGridClass = cn("grid items-center min-h-12", gapClass);
const oneColumnGridClass = cn(columnGridClass, "grid-cols-1");
const twoColumnGridClass = cn(columnGridClass, "grid-cols-2");

export default function TemplateDisplay() {
  const { toast } = useToast();

  const { template, fieldTypeCatalog } = useLoaderData() as {
    template?: Template;
    fieldTypeCatalog?: Array<Catalog<number>>;
  };

  const actionData = useActionData() as any;

  const formRef = useRef<HTMLFormElement>(null);

  const [editingTemplate, _] = useState<Template>(
    template ?? {
      id: `new-${new Date().getTime()}`,
      name: "",
      description: "",
      sections: [],
    }
  );

  const [editingFieldTypeCatalog, __] = useState(fieldTypeCatalog ?? []);

  useEffect(() => {
    if ((actionData && actionData.status !== 200) || actionData?.errorData) {
      showToast(toast, actionData);
    }
  }, [actionData, toast]); // Runs only when `error` changes

  return (
    <div className={cn(["flex", "justify-center", fontsClass])}>
      <Form ref={formRef} method={"post"}>
        <Hidden name={`id`} defaultValue={editingTemplate?.id} />
        <Separator />
        <div className={cn(twoColumnGridClass)}>
          <LabelInput
            id="name"
            label="Name"
            type="text"
            defaultValue={editingTemplate?.name}
          />
          <LabelInput
            id={"description"}
            label="Description"
            type="text"
            defaultValue={editingTemplate?.description}
          />
        </div>
        <Separator />
        <div className={cn(verticalMargin4Class, titleClass)}>Sections</div>
        {editingTemplate?.sections.map((s, i) => {
          return (
            <Section
              section={s}
              sectionIndex={i}
              key={s.id ?? `section-${i}`}
              editingTemplate={editingTemplate}
              fieldTypeCatalog={editingFieldTypeCatalog}
            />
          );
        })}
      </Form>
    </div>
  );
}

const Section = ({
  section,
  sectionIndex,
  fieldTypeCatalog,
}: LocalLinkProps) => {
  const getInputName = (attribute: string) =>
    `section[${sectionIndex}].${attribute}`;
  return (
    <Card className={cn(marginBottom4Class)}>
      <CardContent className={cn("p-4")}>
        <div className={cn(oneColumnGridClass, "")}>
          <Hidden name={getInputName("id")} defaultValue={section?.id} />
          <LabelInput
            id={getInputName("name")}
            label={"Name"}
            defaultValue={section?.name}
          />
          <div className={cn(twoColumnGridClass, "")}>
            {section.fields.map((field, fieldIndex) => {
              return (
                <Field
                  key={field.id ?? `field-${fieldIndex}`}
                  section={section}
                  sectionIndex={sectionIndex}
                  field={field}
                  fieldIndex={fieldIndex}
                  fieldTypeCatalog={fieldTypeCatalog}
                />
              );
            })}
          </div>
        </div>
      </CardContent>
    </Card>
  );
};

const Field = ({
  sectionIndex,
  field,
  fieldIndex,
  fieldTypeCatalog,
}: {
  section: TemplateSection;
  sectionIndex: number;
  field: RecordField;
  fieldIndex: number;
  fieldTypeCatalog: Array<Catalog<number>>;
}) => {
  const getInputName = (attribute: string) =>
    `section[${sectionIndex}].fields[${fieldIndex}].${attribute}`;
  return (
    <div className={cn(["flex", "flex-row", centerClass, gapClass])}>
      <Hidden name={getInputName("id")} defaultValue={field?.id} />
      <LabelInput
        id={getInputName("label")}
        label="Label"
        defaultValue={field.label}
        className={"my-0"}
      />
      <LabelCheckbox
        name={getInputName("isRequired")}
        label={"Required"}
        checked={field.isRequired}
      />
      <LabelComboBox
        name={getInputName("fieldType")}
        label={"Type"}
        fieldTypeCatalog={fieldTypeCatalog}
        selectedType={field.fieldType.id}
      />
    </div>
  );
};

const LabelInput = ({
  id,
  label,
  type = "text",
  className,
  defaultValue,
}: {
  id?: string;
  label: string;
  className?: string;
  type?: string;
  defaultValue?: string;
}) => {
  return (
    <div className={cn(verticalMargin4Class, className)}>
      <Label htmlFor={id}>{label}</Label>
      <Input
        name={id}
        type={type}
        defaultValue={defaultValue}
        className={"mt-2"}
        disabled={true}
      />
    </div>
  );
};

const LabelCheckbox = ({
  name,
  label,
  checked = false,
  className,
}: {
  name: string;
  label: string;
  checked: boolean;
  className?: string;
}) => {
  return (
    <div
      className={cn("flex", "flex-col", "mt-2", "ml-4", gapClass, className)}
    >
      <Label htmlFor={name}>{label}</Label>
      <Checkbox
        name={name}
        defaultChecked={checked}
        className={"mt-1"}
        disabled={true}
      />
    </div>
  );
};

const LabelComboBox = ({
  name,
  label,
  className,
  selectedType,
  fieldTypeCatalog,
}: {
  name: string;
  label: string;
  className?: string;
  selectedType: number;
  fieldTypeCatalog: Array<Catalog<number>>;
}) => {
  return (
    <div
      className={cn("flex", "flex-col", "mt-2", "ml-4", gapClass, className)}
    >
      <Label htmlFor={name}>{label}</Label>
      <ComboBox
        placeholder={"Select field type..."}
        searchPlaceholder={"Search field type..."}
        data={fieldTypeCatalog}
        value={selectedType ?? undefined}
        name={name}
        disabled={true}
      />
    </div>
  );
};
