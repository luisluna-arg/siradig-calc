import { useLoaderData } from "@remix-run/react";
import { Card, CardContent } from "@/components/ui/card";
import { Label } from "@/components/ui/label";
import { Separator } from "@/components/ui/separator";
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

export function TemplateLinkFormDisplay() {
  const { templateLink, fieldTypeCatalog } = useLoaderData() as {
    templateLink: TemplateLink;
    fieldTypeCatalog: Array<Catalog<number>>;
  };

  return (
    <div className={cn(["flex", "justify-center", fontsClass])}>
      <div>
        <Separator />
        <div className={cn(threeColumnGridClass)}>
          <LabelInput
            id="leftTemplate.name"
            label="Left Template"
            type="text"
            defaultValue={templateLink?.leftTemplate.name}
          />
          <Label className={cn(centerClass, "justify-center", "mt-8")}>
            To
          </Label>
          <LabelInput
            id="rightTemplate.name"
            label="Right Template"
            type="text"
            defaultValue={templateLink.rightTemplate.name}
          />
        </div>
        <div className={cn(verticalMargin4Class)}>Fields</div>
        <Separator className={cn(marginBottom4Class)} />
        <div>
          {templateLink.recordTemplateFieldLinks.map(
            (l: RecordTemplateFieldLink, linkIndex: number) => (
              <FieldLink
                key={l.id ?? linkIndex}
                link={l}
                linkIndex={linkIndex}
                fieldTypeCatalog={fieldTypeCatalog}
              />
            )
          )}
        </div>
      </div>
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
    <Card className={cn(marginBottom4Class)}>
      <CardContent className={cn("p-4")}>
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
              const getLeftInputName = (attribute: string) =>
                `${baseName}.leftFields[${lfIndex}].${attribute}`;
              return (
                <div
                  key={lf.id ?? lfIndex}
                  className={cn(
                    "flex",
                    "flex-row",
                    "items-start",
                    "h-full",
                    marginBottom2Class,
                    gapClass
                  )}
                >
                  <LabelInput
                    id={getLeftInputName("label")}
                    label="Label"
                    defaultValue={lf.label}
                    className={"my-0"}
                  />
                  <LabelComboBox
                    name={getLeftInputName("fieldType")}
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
  );
};
