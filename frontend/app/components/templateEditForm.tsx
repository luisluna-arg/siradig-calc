import { useEffect, useRef, useState } from "react";
import { useToast } from "@/hooks/use-toast";
import { Form, useActionData, useLoaderData } from "@remix-run/react";
import { cn } from "@/lib/utils";
import { showToast } from "@/utils/route/form";
import { Catalog } from "@/data/interfaces/Catalog";
import { Template } from "@/data/interfaces/Template";
import { TemplateSection } from "@/data/interfaces/TemplateSection";
import { RecordField } from "@/data/interfaces/RecordField";
import { Card, CardContent } from "@/components/ui/card";
import { Checkbox } from "@/components/ui/checkbox";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import ComboBox from "@/components/utils/comboBox";
import Hidden from "@/components/utils/hidden";
import ActionButton from "./utils/actionButton";
import { FieldTypeEnum } from "@/data/types/FieldType";

interface LocalLinkProps {
  className?: string;
  section: TemplateSection;
  sectionIndex: number;
  editingTemplate?: Template;
  fieldTypeCatalog: Array<Catalog<number>>;
  handleAddField: (sectionId: string) => void;
  handleRemoveField: (sectionId: string, fieldId: string) => void;
  handleRemoveSection: (sectionId: string) => void;
}

const fontsClass = cn("h-12");
const gapClass = cn("gap-2");
const titleClass = cn("font-semibold", "leading-none", "tracking-tight");
const verticalMargin2Class = cn("my-2");
const verticalMargin4Class = cn("my-4");
const marginLeft2Class = cn("ml-2");
const marginBottom4Class = cn("mb-4");
const centerClass = cn("flex", "items-center");
const centerEndClass = cn(centerClass, "justify-end");
const columnGridClass = cn("grid items-center min-h-12", gapClass);
const oneColumnGridClass = cn(columnGridClass, "grid-cols-1");
const twoColumnGridClass = cn(columnGridClass, "grid-cols-2");

export default function RecordEditForm() {
  const { toast } = useToast();

  const { template, fieldTypeCatalog } = useLoaderData() as {
    template?: Template;
    fieldTypeCatalog?: Array<Catalog<number>>;
  };

  const actionData = useActionData() as any;

  const formRef = useRef<HTMLFormElement>(null);

  const [editingTemplate, setEditingTemplate] = useState<Template>(
    template ?? {
      id: `new-${new Date().getTime()}`,
      name: "",
      description: "",
      sections: [],
    }
  );

  const [editingFieldTypeCatalog, __] = useState(fieldTypeCatalog ?? []);

  function handleClear() {
    if (!editingTemplate) return;

    setEditingTemplate({
      ...editingTemplate,
      sections: [],
    });

    if (formRef.current) {
      const textInputs = formRef.current.querySelectorAll(
        "input[type='text'],input[type='hidden']"
      );
      textInputs.forEach(
        (input: Element) => ((input as HTMLInputElement).value = "")
      );
    }
  }

  function handleAddSection() {
    if (!editingTemplate) return;

    const newSection: TemplateSection = {
      id: `new-${new Date().getTime()}`,
      name: "",
      description: "",
      fields: [],
    };

    setEditingTemplate({
      ...editingTemplate,
      sections: [...editingTemplate.sections, newSection],
    });
  }

  function handleRemoveSection(sectionId: string) {
    if (!editingTemplate) return;

    setEditingTemplate({
      ...editingTemplate,
      sections: [...editingTemplate.sections.filter((s) => s.id !== sectionId)],
    });
  }

  function handleAddField(sectionId: string) {
    if (!editingTemplate) return;

    const updatedSections = [...editingTemplate.sections];
    const targetSectionIndex = updatedSections.findIndex(
      (s) => s.id == sectionId
    );
    if (targetSectionIndex === -1) return;

    const targetSection = updatedSections[targetSectionIndex];

    const newField: RecordField = {
      id: `new-field-${Date.now()}`,
      label: "",
      isRequired: false,
      fieldType: FieldTypeEnum.Text,
      links: [],
    };

    targetSection.fields = [...(targetSection.fields ?? []), newField];
    updatedSections[targetSectionIndex] = targetSection;

    setEditingTemplate({
      ...editingTemplate,
      sections: updatedSections,
    });
  }

  function handleRemoveField(sectionId: string, fieldId: string) {
    if (!editingTemplate) return;

    const updatedSections = [...editingTemplate.sections];
    const targetSectionIndex = updatedSections.findIndex(
      (s) => s.id === sectionId
    );
    if (targetSectionIndex === -1) return;

    const targetSection = updatedSections[targetSectionIndex];
    targetSection.fields = (targetSection.fields ?? []).filter(
      (f) => f.id !== fieldId
    );
    updatedSections[targetSectionIndex] = targetSection;

    setEditingTemplate({
      ...editingTemplate,
      sections: updatedSections,
    });
  }

  useEffect(() => {
    if ((actionData && actionData.status !== 200) || actionData?.errorData) {
      showToast(toast, actionData);
    }
  }, [actionData, toast]); // Runs only when `error` changes

  return (
    <div className={cn(["flex", "justify-center", fontsClass])}>
      <Form ref={formRef} method={"post"}>
        <Hidden name={`id`} defaultValue={editingTemplate?.id} />
        <div className={cn(oneColumnGridClass)}>
          <div
            className={cn([
              centerEndClass,
              gapClass,
              verticalMargin2Class,
              fontsClass,
            ])}
          >
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
        <hr />
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
        <hr />
        <div className={cn(verticalMargin4Class, titleClass)}>Sections</div>
        {editingTemplate?.sections.map((s, i) => {
          return (
            <Section
              section={s}
              sectionIndex={i}
              key={s.id ?? `section-${i}`}
              editingTemplate={editingTemplate}
              fieldTypeCatalog={editingFieldTypeCatalog}
              handleAddField={handleAddField}
              handleRemoveField={handleRemoveField}
              handleRemoveSection={handleRemoveSection}
            />
          );
        })}
        <div
          className={cn([
            centerEndClass,
            gapClass,
            verticalMargin2Class,
            fontsClass,
          ])}
        >
          <ActionButton
            type="add"
            text="Add section"
            onClick={async (e) => {
              e.preventDefault;
              await handleAddSection();
            }}
          />
        </div>
      </Form>
    </div>
  );
}

const Section = ({
  section,
  sectionIndex,
  fieldTypeCatalog,
  handleAddField,
  handleRemoveField,
  handleRemoveSection,
}: LocalLinkProps) => {
  const getInputName = (attribute: string) =>
    `section[${sectionIndex}].${attribute}`;
  return (
    <Card className={cn(marginBottom4Class)}>
      <CardContent className={cn("p-4")}>
        <div className={cn([centerEndClass, gapClass, fontsClass])}>
          <ActionButton
            type="delete"
            onClick={async (e) => {
              e.preventDefault;
              await handleRemoveSection(section.id);
            }}
          />
        </div>
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
                  handleRemoveField={handleRemoveField}
                />
              );
            })}
          </div>
          <div className={cn([centerEndClass, gapClass, fontsClass])}>
            <ActionButton
              type="add"
              text="Add field"
              onClick={async (e) => {
                e.preventDefault;
                handleAddField(section.id);
              }}
            />
          </div>
        </div>
      </CardContent>
    </Card>
  );
};

const Field = ({
  section,
  sectionIndex,
  field,
  fieldIndex,
  fieldTypeCatalog,
  handleRemoveField,
}: {
  section: TemplateSection;
  sectionIndex: number;
  field: RecordField;
  fieldIndex: number;
  fieldTypeCatalog: Array<Catalog<number>>;
  handleRemoveField: (sectionId: string, fieldId: string) => void;
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
        selectedType={field.fieldType}
      />
      <div className={cn(marginLeft2Class, "mt-8")}>
        <ActionButton
          type="delete"
          onClick={async (e) => {
            e.preventDefault;
            handleRemoveField(section.id, field.id);
          }}
        />
      </div>
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
      <Checkbox name={name} defaultChecked={checked} className={"mt-1"} />
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
  console.log(selectedType);
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
      />
    </div>
  );
};
