import { useRef } from "react";
import { Form, useLoaderData } from "@remix-run/react";
import { Card, CardContent } from "@/components/ui/card";
import { Label } from "@/components/ui/label";
import { Separator } from "@/components/ui/separator";
import { Hidden } from "@/components/utils/hidden";
import { LabelComboBox } from "@/components/utils/labelCombobox";
import { LabelInput } from "@/components/utils/labelInput";
import { Catalog } from "@/data/interfaces/Catalog";
import { RecordTemplateFieldLink } from "@/data/interfaces/RecordTemplateFieldLink";
import { TemplateLink } from "@/data/interfaces/TemplateLink";
import { cn } from "@/lib/utils";

const fontsClass = cn("h-12");
const gapClass = cn("gap-2");
const verticalMargin4Class = cn("my-4");
const marginBottom2Class = cn("mb-2");
const marginBottom4Class = cn("mb-4");
const centerClass = cn("flex", "items-center");
const columnGridClass = cn("grid", "items-center", "min-h-12", gapClass);
const threeColumnGridClass = cn(columnGridClass, "grid-cols-3");

export function TemplateLinkForm() {
  const { templateLink, fieldTypeCatalog } = useLoaderData() as {
    templateLink: TemplateLink;
    fieldTypeCatalog: Array<Catalog<number>>;
  };

  const formRef = useRef<HTMLFormElement>(null);

  const getMainInputName = (attribute: string) => `${attribute}`;

  return (
    <div className={cn(["flex", "justify-center", fontsClass])}>
      <Form ref={formRef} method={"post"}>
        <Hidden name={getMainInputName(`id`)} defaultValue={templateLink?.id} />
        <Separator />
        <div className={cn(threeColumnGridClass)}>
          <Hidden
            name={getMainInputName(`leftTemplate.id`)}
            defaultValue={templateLink?.id}
          />
          <LabelInput
            id={getMainInputName("leftTemplate.name")}
            label="Left Template"
            type="text"
            defaultValue={templateLink?.leftTemplate.name}
          />
          <Label className={cn(centerClass, "justify-center", "mt-8")}>
            To
          </Label>
          <Hidden
            name={getMainInputName(`rightTemplate.id`)}
            defaultValue={templateLink?.id}
          />
          <LabelInput
            id={getMainInputName(`rightTemplate.name`)}
            label="Right Template"
            type="text"
            defaultValue={templateLink.rightTemplate.name}
          />
        </div>
        <div className={cn(verticalMargin4Class)}>Fields</div>
        <Separator className={cn(marginBottom4Class)} />
        <div>
          {templateLink.recordTemplateFieldLinks.map(
            (l: RecordTemplateFieldLink, linkIndex: number) => {
              return (
                <FieldLink
                  link={l}
                  linkIndex={linkIndex}
                  fieldTypeCatalog={fieldTypeCatalog}
                />
              );
            }
          )}
        </div>
      </Form>
    </div>
  );
}

interface FieldLinkProps {
  link: RecordTemplateFieldLink;
  linkIndex: number;
  fieldTypeCatalog: Array<Catalog<number>>;
}

const FieldLink = ({ link, linkIndex, fieldTypeCatalog }: FieldLinkProps) => {
  const baseName = `recordTemplateFieldLinks[${linkIndex}]`;
  const getInputName = (attribute: string) => `${baseName}.${attribute}`;
  return (
    <>
      <Card className={cn(marginBottom4Class)}>
        <CardContent className={cn("p-4")}>
          <Hidden name={getInputName(`id`)} defaultValue={link?.id} />
          <div className={cn(threeColumnGridClass)}>
            <div>
              <Label>{"Right field"}</Label>
            </div>
            <div className={cn("col-span-2")}>
              <Label>{"Left fields"}</Label>
            </div>
          </div>
          <div className={cn(threeColumnGridClass)}>
            <div className={cn("flex", "flex-col", "items-start", "h-full")}>
              <Hidden
                name={getInputName(`id`)}
                defaultValue={link.rightField?.id}
              />
              <LabelInput
                id={getInputName("rightField.label")}
                label="Label"
                defaultValue={link.rightField.label}
                className={cn(marginBottom2Class)}
              />
              <LabelComboBox
                name={getInputName("fieldType")}
                className={cn(["ml-auto", "mr-auto", "mt-2"])}
                label={"Type"}
                fieldTypeCatalog={fieldTypeCatalog}
                selectedType={link.rightField.fieldType.id}
              />
            </div>
            <div className={cn("col-span-2", "flex-col")}>
              {link.leftFields.map((lf, lfIndex) => {
                const getInputName = (attribute: string) =>
                  `${baseName}.leftFields[${lfIndex}].${attribute}`;
                return (
                  <div
                    className={cn(
                      "flex",
                      "flex-row",
                      "items-start",
                      "h-full",
                      marginBottom2Class,
                      gapClass
                    )}
                  >
                    <Hidden name={getInputName(`id`)} defaultValue={lf?.id} />
                    <LabelInput
                      id={getInputName("label")}
                      label="Label"
                      defaultValue={lf.label}
                      className={"my-0"}
                    />
                    <LabelComboBox
                      name={getInputName("fieldType")}
                      label={"Type"}
                      fieldTypeCatalog={fieldTypeCatalog}
                      selectedType={lf.fieldType.id}
                    />
                  </div>
                );
              })}
            </div>
          </div>
        </CardContent>
      </Card>
    </>
  );
};
