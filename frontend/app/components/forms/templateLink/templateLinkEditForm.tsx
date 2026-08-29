import { useState } from "react";
import { useLoaderData, useRevalidator } from "@remix-run/react";
import { XIcon } from "lucide-react";
import { Card, CardContent } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { Label } from "@/components/ui/label";
import { Separator } from "@/components/ui/separator";
import { ActionButton } from "@/components/utils/actionButton";
import { ComboBox } from "@/components/utils/comboBox";
import { LabelInput } from "@/components/utils/labelInput";
import { ApiClientProvider } from "@/data/ApiClientProvider";
import { Catalog } from "@/data/interfaces/Catalog";
import { RecordField } from "@/data/interfaces/RecordField";
import { RecordTemplateFieldLink } from "@/data/interfaces/RecordTemplateFieldLink";
import { Template } from "@/data/interfaces/Template";
import { TemplateLink } from "@/data/interfaces/TemplateLink";
import { useToast } from "@/hooks/use-toast";
import { cn } from "@/lib/utils";

const fontsClass = cn("h-12");
const gapClass = cn("gap-2");
const verticalMargin4Class = cn("my-4");
const marginBottom2Class = cn("mb-2");
const marginBottom4Class = cn("mb-4");
const centerClass = cn("flex", "items-center");
const columnGridClass = cn("grid", "items-center", "min-h-12", gapClass);
const threeColumnGridClass = cn(columnGridClass, "grid-cols-3");

const flattenFields = (template: Template): Array<RecordField> =>
  template.sections.flatMap((s) => s.fields);

const toCatalog = (field: RecordField): Catalog<string> => ({
  id: field.id,
  label: field.label,
});

export function TemplateLinkEditForm() {
  const { templateLink } = useLoaderData() as {
    templateLink: TemplateLink;
  };
  const revalidator = useRevalidator();
  const { toast } = useToast();

  const [busy, setBusy] = useState(false);
  const [newRegionRightFieldId, setNewRegionRightFieldId] = useState<string | null>(null);
  const [newRegionLeftFieldId, setNewRegionLeftFieldId] = useState<string | null>(null);

  const leftTemplateId = templateLink.leftTemplate.id;
  const rightTemplateId = templateLink.rightTemplate.id;
  const leftFields = flattenFields(templateLink.leftTemplate);
  const rightFields = flattenFields(templateLink.rightTemplate);

  const linkedRightFieldIds = new Set(
    templateLink.recordTemplateFieldLinks.map((l) => l.rightField.id)
  );
  const availableRightFieldsForNewRegion = rightFields.filter(
    (f) => !linkedRightFieldIds.has(f.id)
  );

  const reportError = (title: string, error: any) => {
    toast({
      title,
      description: error.response?.data?.message ?? error.message,
      variant: "destructive",
    });
  };

  const handleAddFieldToGroup = async (rightFieldId: string, leftFieldId: string) => {
    setBusy(true);
    try {
      const apiClient = new ApiClientProvider();
      await apiClient.TemplateLinks.linkField(
        leftTemplateId,
        rightTemplateId,
        leftFieldId,
        rightFieldId
      );
      revalidator.revalidate();
    } catch (error: any) {
      reportError("Error al vincular el campo", error);
    } finally {
      setBusy(false);
    }
  };

  const handleRemoveFieldFromGroup = async (rightFieldId: string, leftFieldId: string) => {
    setBusy(true);
    try {
      const apiClient = new ApiClientProvider();
      await apiClient.TemplateLinks.unlinkField(
        leftTemplateId,
        rightTemplateId,
        leftFieldId,
        rightFieldId
      );
      revalidator.revalidate();
    } catch (error: any) {
      reportError("Error al desvincular el campo", error);
    } finally {
      setBusy(false);
    }
  };

  const handleAddRegion = async () => {
    if (!newRegionRightFieldId || !newRegionLeftFieldId) return;
    setBusy(true);
    try {
      const apiClient = new ApiClientProvider();
      await apiClient.TemplateLinks.linkField(
        leftTemplateId,
        rightTemplateId,
        newRegionLeftFieldId,
        newRegionRightFieldId
      );
      setNewRegionRightFieldId(null);
      setNewRegionLeftFieldId(null);
      revalidator.revalidate();
    } catch (error: any) {
      reportError("Error al vincular el campo", error);
    } finally {
      setBusy(false);
    }
  };

  return (
    <div className={cn(["flex", "justify-center", fontsClass])}>
      <div className="w-full max-w-4xl">
        <Separator />
        <div className={cn(threeColumnGridClass)}>
          <LabelInput
            label="Template izquierdo"
            type="text"
            defaultValue={templateLink.leftTemplate.name}
            disabled={true}
          />
          <Label className={cn(centerClass, "justify-center", "mt-8")}>
            A
          </Label>
          <LabelInput
            label="Template derecho"
            type="text"
            defaultValue={templateLink.rightTemplate.name}
            disabled={true}
          />
        </div>
        <div className={cn(verticalMargin4Class)}>Campos</div>
        <Separator className={cn(marginBottom4Class)} />
        <div>
          {templateLink.recordTemplateFieldLinks.map((l: RecordTemplateFieldLink) => (
            <FieldLinkGroup
              key={l.id}
              link={l}
              leftFields={leftFields}
              busy={busy}
              onAddField={(leftFieldId) =>
                handleAddFieldToGroup(l.rightField.id, leftFieldId)
              }
              onRemoveField={(leftFieldId) =>
                handleRemoveFieldFromGroup(l.rightField.id, leftFieldId)
              }
            />
          ))}
        </div>

        <Card className={cn(marginBottom4Class)}>
          <CardContent className={cn("p-4", "flex", "flex-col", gapClass)}>
            <Label>Agregar nueva región</Label>
            <div className={cn("flex", "flex-row", "items-center", gapClass)}>
              <ComboBox
                key={`new-region-right-${templateLink.recordTemplateFieldLinks.length}`}
                placeholder="Seleccionar campo derecho..."
                searchPlaceholder="Buscar campo..."
                data={availableRightFieldsForNewRegion.map(toCatalog)}
                value={undefined}
                onSelect={(entry) => setNewRegionRightFieldId(entry?.id ?? null)}
                disabled={busy}
              />
              <ComboBox
                key={`new-region-left-${templateLink.recordTemplateFieldLinks.length}`}
                placeholder="Seleccionar campo izquierdo..."
                searchPlaceholder="Buscar campo..."
                data={leftFields.map(toCatalog)}
                value={undefined}
                onSelect={(entry) => setNewRegionLeftFieldId(entry?.id ?? null)}
                disabled={busy}
              />
              <ActionButton
                type="add"
                text="Agregar"
                disabled={!newRegionRightFieldId || !newRegionLeftFieldId || busy}
                onClick={async (e) => {
                  e?.preventDefault?.();
                  await handleAddRegion();
                }}
              />
            </div>
          </CardContent>
        </Card>
      </div>
    </div>
  );
}

interface FieldLinkGroupProps {
  link: RecordTemplateFieldLink;
  leftFields: Array<RecordField>;
  busy: boolean;
  onAddField: (leftFieldId: string) => void;
  onRemoveField: (leftFieldId: string) => void;
}

const FieldLinkGroup = ({
  link,
  leftFields,
  busy,
  onAddField,
  onRemoveField,
}: FieldLinkGroupProps) => {
  const usedLeftFieldIds = new Set(link.leftFields.map((f) => f.id));
  const availableLeftFields = leftFields.filter((f) => !usedLeftFieldIds.has(f.id));

  return (
    <Card className={cn(marginBottom4Class)}>
      <CardContent className={cn("p-4")}>
        <div className={cn(threeColumnGridClass)}>
          <div>
            <Label>{"Campo derecho"}</Label>
            <div className="text-sm mt-2">{link.rightField.label}</div>
          </div>
          <div className={cn("col-span-2")}>
            <Label>{"Campos izquierdos"}</Label>
          </div>
        </div>
        <div className={cn(threeColumnGridClass)}>
          <div />
          <div className={cn("col-span-2", "flex", "flex-col", gapClass)}>
            {link.leftFields.map((lf) => (
              <div
                key={lf.id}
                className="flex items-center justify-between text-sm bg-muted rounded px-3 py-2"
              >
                <span className="truncate">{lf.label}</span>
                <Button
                  variant="ghost"
                  size="icon"
                  className="h-6 w-6"
                  disabled={busy}
                  onClick={() => onRemoveField(lf.id)}
                >
                  <XIcon className="h-4 w-4" />
                </Button>
              </div>
            ))}
            <ComboBox
              key={`${link.id}-${link.leftFields.length}`}
              placeholder="Agregar campo izquierdo..."
              searchPlaceholder="Buscar campo..."
              className={cn(marginBottom2Class)}
              data={availableLeftFields.map(toCatalog)}
              value={undefined}
              onSelect={(entry) => entry && onAddField(entry.id)}
              disabled={busy}
            />
          </div>
        </div>
      </CardContent>
    </Card>
  );
};
