import { useState } from "react";
import { useLoaderData, useNavigate } from "@remix-run/react";
import { XIcon } from "lucide-react";
import { cn } from "@/lib/utils";
import { ComboBox } from "@/components/utils/comboBox";
import { Button } from "@/components/ui/button";
import { ActionButton } from "@/components/utils/actionButton";
import { Catalog } from "@/data/interfaces/Catalog";
import { ApiClientProvider } from "@/data/ApiClientProvider";
import { useToast } from "@/hooks/use-toast";

interface ConversionPair {
  record: Catalog<string>;
  template: Catalog<string>;
}

export default function RecordConversionFormAdd() {
  const navigate = useNavigate();
  const { toast } = useToast();

  const { recordCatalog, templateCatalog } = useLoaderData() as {
    recordCatalog: Array<Catalog<string>>;
    templateCatalog: Array<Catalog<string>>;
  };

  const [pairs, setPairs] = useState<ConversionPair[]>([]);
  const [pendingRecordId, setPendingRecordId] = useState<string | null>(null);
  const [pendingTemplateId, setPendingTemplateId] = useState<string | null>(null);
  const [loading, setLoading] = useState(false);
  const [progress, setProgress] = useState({ done: 0, total: 0 });

  const comboButtonClass = cn(["min-w-80"]);

  const canAddPair = !!pendingRecordId && !!pendingTemplateId;

  const handleAddPair = () => {
    if (!pendingRecordId || !pendingTemplateId) return;

    const record = recordCatalog.find((r) => r.id === pendingRecordId);
    const template = templateCatalog.find((t) => t.id === pendingTemplateId);
    if (!record || !template) return;

    const alreadyAdded = pairs.some(
      (p) => p.record.id === record.id && p.template.id === template.id
    );
    if (alreadyAdded) return;

    setPairs((prev) => [...prev, { record, template }]);
    setPendingRecordId(null);
    setPendingTemplateId(null);
  };

  const handleRemovePair = (index: number) => {
    setPairs((prev) => prev.filter((_, i) => i !== index));
  };

  const canConvert = pairs.length > 0 && !loading;

  const handleConvert = async () => {
    setLoading(true);
    setProgress({ done: 0, total: pairs.length });

    const apiClient = new ApiClientProvider();
    let failures = 0;

    for (const pair of pairs) {
      try {
        await apiClient.Conversions.convert(pair.record.id, pair.template.id);
      } catch (error: any) {
        failures++;
        toast({
          title: "Error al convertir",
          description: `${pair.record.label} → ${pair.template.label}: ${
            error.response?.data?.message ?? error.message
          }`,
          variant: "destructive",
        });
      } finally {
        setProgress((p) => ({ ...p, done: p.done + 1 }));
      }
    }

    setLoading(false);

    if (failures < pairs.length) {
      navigate("/records/conversions");
    }
  };

  return (
    <div className={cn(["flex", "flex-col", "gap-6"])}>
      <div className={cn(["grid", "grid-cols-2", "gap-6", "justify-between"])}>
        <ComboBox
          key={`record-${pairs.length}`}
          placeholder={"Seleccionar registro..."}
          searchPlaceholder={"Buscar registro..."}
          className={cn("ml-auto")}
          buttonClassName={cn(comboButtonClass)}
          data={recordCatalog}
          value={undefined}
          onSelect={(entry) => setPendingRecordId(entry?.id ?? null)}
        />
        <ComboBox
          key={`template-${pairs.length}`}
          placeholder={"Seleccionar template destino..."}
          searchPlaceholder={"Buscar template..."}
          className={cn("mr-auto")}
          buttonClassName={cn(comboButtonClass)}
          data={templateCatalog}
          value={undefined}
          onSelect={(entry) => setPendingTemplateId(entry?.id ?? null)}
        />
        <div className={cn(["flex", "flex-row", "justify-end", "col-span-2"])}>
          <ActionButton
            type="add"
            text="Agregar"
            onClick={async (e) => {
              e?.preventDefault?.();
              handleAddPair();
            }}
            disabled={!canAddPair}
          />
        </div>
      </div>

      {pairs.length > 0 && (
        <ul className="flex flex-col gap-1">
          {pairs.map((pair, i) => (
            <li
              key={`${pair.record.id}-${pair.template.id}-${i}`}
              className="flex items-center justify-between text-sm bg-muted rounded px-3 py-2"
            >
              <span className="truncate">
                {pair.record.label} <span className="text-muted-foreground">→</span> {pair.template.label}
              </span>
              <Button
                variant="ghost"
                size="icon"
                className="h-6 w-6"
                onClick={() => handleRemovePair(i)}
              >
                <XIcon className="h-4 w-4" />
              </Button>
            </li>
          ))}
        </ul>
      )}

      <div className={cn(["flex", "flex-row", "items-center", "justify-end", "gap-2"])}>
        {loading && (
          <span className="text-sm text-muted-foreground">
            Convirtiendo {progress.done} de {progress.total}...
          </span>
        )}
        <Button onClick={handleConvert} disabled={!canConvert}>
          {loading
            ? "Convirtiendo..."
            : pairs.length > 1
            ? `Convertir (${pairs.length})`
            : "Convertir"}
        </Button>
      </div>
    </div>
  );
}
