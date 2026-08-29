import { useEffect, useRef, useState } from "react";
import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
} from "@/components/ui/dialog";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Separator } from "@/components/ui/separator";
import { ComboBox } from "@/components/utils/comboBox";
import { ApiClientProvider } from "@/data/ApiClientProvider";
import { Catalog } from "@/data/interfaces/Catalog";
import { ImportResult } from "@/data/interfaces/ImportResult";
import { Template } from "@/data/interfaces/Template";
import { RecordPostData } from "@/utils/route/interfaces/RecordPostData";
import { cn } from "@/lib/utils";
import { useToast } from "@/hooks/use-toast";
import { XIcon } from "lucide-react";

interface ImportRecordModalProps {
  open: boolean;
  onClose: () => void;
  onSaved: () => void;
}

interface PendingRecord {
  fileName: string;
  title: string;
  fieldValues: Record<string, string>;
  saved: boolean;
  error?: string;
}

export default function ImportRecordModal({ open, onClose, onSaved }: ImportRecordModalProps) {
  const { toast } = useToast();
  const fileInputRef = useRef<HTMLInputElement>(null);

  const [step, setStep] = useState<1 | 2>(1);
  const [templateCatalog, setTemplateCatalog] = useState<Catalog<string>[]>([]);
  const [selectedTemplateId, setSelectedTemplateId] = useState<string | null>(null);
  const [files, setFiles] = useState<File[]>([]);
  const [fullTemplate, setFullTemplate] = useState<Template | null>(null);
  const [pendingRecords, setPendingRecords] = useState<PendingRecord[]>([]);
  const [currentIndex, setCurrentIndex] = useState(0);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    if (!open) return;
    const apiClient = new ApiClientProvider();
    apiClient.Catalogs.getTemplates()
      .then((data) => setTemplateCatalog(data as unknown as Catalog<string>[]))
      .catch(() => setError("Error al cargar los templates"));
  }, [open]);

  useEffect(() => {
    if (!open) {
      setStep(1);
      setSelectedTemplateId(null);
      setFiles([]);
      setFullTemplate(null);
      setPendingRecords([]);
      setCurrentIndex(0);
      setError(null);
      if (fileInputRef.current) fileInputRef.current.value = "";
    }
  }, [open]);

  const handleFilesSelected = (fileList: FileList | null) => {
    setFiles((prev) => [...prev, ...Array.from(fileList ?? [])]);
    if (fileInputRef.current) fileInputRef.current.value = "";
  };

  const handleRemoveFile = (index: number) => {
    setFiles((prev) => prev.filter((_, i) => i !== index));
  };

  const handleImport = async () => {
    if (!selectedTemplateId || files.length === 0) return;
    setLoading(true);
    setError(null);
    try {
      const apiClient = new ApiClientProvider();
      const template = await apiClient.Templates.get(selectedTemplateId);

      const results = await Promise.allSettled(
        files.map((file) => {
          const ext = file.name.split(".").pop()?.toLowerCase();
          return ext === "pdf"
            ? apiClient.Records.importPdf(selectedTemplateId, file)
            : apiClient.Records.importCsv(selectedTemplateId, file);
        })
      );

      const records: PendingRecord[] = results.map((result, i) => {
        const fileName = files[i].name;
        if (result.status === "rejected") {
          const err = result.reason as any;
          const msg = err?.response?.data?.message ?? err?.message ?? "Error al importar";
          return { fileName, title: "", fieldValues: {}, saved: false, error: msg };
        }

        const importResult: ImportResult = result.value;
        const fieldValues: Record<string, string> = {};
        importResult.values.forEach((v) => {
          fieldValues[v.fieldId] = v.value;
        });

        return {
          fileName,
          title: importResult.title || template.name,
          fieldValues,
          saved: false,
        };
      });

      setFullTemplate(template);
      setPendingRecords(records);

      const firstOk = records.findIndex((r) => !r.error);
      if (firstOk === -1) {
        setError("No se pudo importar ninguno de los archivos seleccionados");
        return;
      }

      setCurrentIndex(firstOk);
      setStep(2);
    } catch (err: any) {
      const msg = err.response?.data?.message ?? err.message ?? "Error al importar";
      setError(msg);
      toast({ title: "Error de importación", description: msg, variant: "destructive" });
    } finally {
      setLoading(false);
    }
  };

  const updateCurrentRecord = (updater: (record: PendingRecord) => PendingRecord) => {
    setPendingRecords((prev) =>
      prev.map((r, i) => (i === currentIndex ? updater(r) : r))
    );
  };

  const nextPendingIndex = (from: number) =>
    pendingRecords.findIndex((r, i) => i > from && !r.error && !r.saved);

  const saveRecord = async (index: number): Promise<boolean> => {
    if (!selectedTemplateId) return false;
    const record = pendingRecords[index];
    if (!record || record.error || record.saved) return true;

    try {
      const apiClient = new ApiClientProvider();
      const postData: RecordPostData = {
        templateId: selectedTemplateId,
        title: record.title,
        values: Object.entries(record.fieldValues)
          .filter(([, v]) => v !== "")
          .map(([fieldId, value]) => ({ id: "", fieldId, value })),
      };
      await apiClient.Records.post(postData);
      setPendingRecords((prev) =>
        prev.map((r, i) => (i === index ? { ...r, saved: true } : r))
      );
      return true;
    } catch (err: any) {
      const msg = err.response?.data?.message ?? err.message ?? "Error al guardar el registro";
      setPendingRecords((prev) =>
        prev.map((r, i) => (i === index ? { ...r, error: msg } : r))
      );
      toast({ title: "Error al guardar", description: msg, variant: "destructive" });
      return false;
    }
  };

  const handleSaveCurrent = async () => {
    setLoading(true);
    setError(null);
    const ok = await saveRecord(currentIndex);
    setLoading(false);
    if (!ok) return;

    const next = nextPendingIndex(currentIndex);
    if (next === -1) {
      onSaved();
      onClose();
    } else {
      setCurrentIndex(next);
    }
  };

  const handleSaveAll = async () => {
    setLoading(true);
    setError(null);
    let allOk = true;
    for (let i = 0; i < pendingRecords.length; i++) {
      const record = pendingRecords[i];
      if (record.error || record.saved) continue;
      const ok = await saveRecord(i);
      if (!ok) allOk = false;
    }
    setLoading(false);
    if (allOk) {
      onSaved();
      onClose();
    }
  };

  const canImport = !!selectedTemplateId && files.length > 0 && !loading;
  const currentRecord = pendingRecords[currentIndex];
  const pendingCount = pendingRecords.filter((r) => !r.error && !r.saved).length;
  const canSave = !!currentRecord && !currentRecord.error && !loading;

  return (
    <Dialog open={open} onOpenChange={(isOpen) => !isOpen && onClose()}>
      <DialogContent className={cn("max-w-3xl max-h-[90vh] overflow-y-auto")}>
        <DialogHeader>
          <DialogTitle>
            {step === 1 ? "Importar registros desde archivos" : "Revisar y guardar registros"}
          </DialogTitle>
        </DialogHeader>

        {step === 1 && (
          <div className="flex flex-col gap-6 pt-2">
            <div className="flex flex-col gap-2">
              <Label>Template</Label>
              <ComboBox
                placeholder="Seleccionar template..."
                searchPlaceholder="Buscar template..."
                data={templateCatalog}
                value={selectedTemplateId ?? undefined}
                onSelect={(entry) => setSelectedTemplateId(entry?.id ?? null)}
              />
            </div>
            <div className="flex flex-col gap-2">
              <Label htmlFor="import-file">Archivos (.pdf o .csv)</Label>
              <input
                id="import-file"
                ref={fileInputRef}
                type="file"
                accept=".pdf,.csv"
                multiple
                className="text-sm file:mr-4 file:py-1 file:px-3 file:rounded file:border-0 file:text-sm file:bg-muted file:text-muted-foreground hover:file:bg-muted/80 cursor-pointer"
                onChange={(e) => handleFilesSelected(e.target.files)}
              />
              {files.length > 0 && (
                <ul className="flex flex-col gap-1 mt-2">
                  {files.map((f, i) => (
                    <li
                      key={`${f.name}-${i}`}
                      className="flex items-center justify-between text-sm bg-muted rounded px-2 py-1"
                    >
                      <span className="truncate">{f.name}</span>
                      <Button
                        variant="ghost"
                        size="icon"
                        className="h-6 w-6"
                        onClick={() => handleRemoveFile(i)}
                      >
                        <XIcon className="h-4 w-4" />
                      </Button>
                    </li>
                  ))}
                </ul>
              )}
            </div>
            {error && <p className="text-sm text-destructive">{error}</p>}
            <div className="flex justify-end">
              <Button onClick={handleImport} disabled={!canImport}>
                {loading ? "Importando..." : "Importar"}
              </Button>
            </div>
          </div>
        )}

        {step === 2 && fullTemplate && currentRecord && (
          <div className="flex flex-col gap-6 pt-2">
            <div className="flex items-center justify-between">
              <span className="text-sm text-muted-foreground">
                Registro {currentIndex + 1} de {pendingRecords.length}
                {" — "}
                {currentRecord.fileName}
              </span>
              <div className="flex gap-2">
                <Button
                  variant="outline"
                  size="sm"
                  disabled={currentIndex === 0 || loading}
                  onClick={() => setCurrentIndex((i) => Math.max(0, i - 1))}
                >
                  Anterior
                </Button>
                <Button
                  variant="outline"
                  size="sm"
                  disabled={currentIndex === pendingRecords.length - 1 || loading}
                  onClick={() =>
                    setCurrentIndex((i) => Math.min(pendingRecords.length - 1, i + 1))
                  }
                >
                  Siguiente
                </Button>
              </div>
            </div>

            {currentRecord.error ? (
              <p className="text-sm text-destructive">{currentRecord.error}</p>
            ) : (
              <>
                <div className="flex flex-col gap-2">
                  <Label htmlFor="import-title">Título</Label>
                  <Input
                    id="import-title"
                    type="text"
                    value={currentRecord.title}
                    onChange={(e) =>
                      updateCurrentRecord((r) => ({ ...r, title: e.target.value }))
                    }
                    placeholder="Título del registro"
                  />
                </div>
                <div className="grid grid-cols-2 gap-6">
                  {fullTemplate.sections.map((section) => (
                    <div key={section.id} className="p-4">
                      <h2 className="font-medium">{section.name}</h2>
                      <Separator className="mb-4 mt-1" />
                      <div className="flex flex-col gap-3">
                        {section.fields.map((field) => (
                          <div key={field.id} className="flex flex-col gap-1">
                            <Label className="text-xs text-muted-foreground">{field.label}</Label>
                            <Input
                              type="text"
                              value={currentRecord.fieldValues[field.id] ?? ""}
                              onChange={(e) =>
                                updateCurrentRecord((r) => ({
                                  ...r,
                                  fieldValues: {
                                    ...r.fieldValues,
                                    [field.id]: e.target.value,
                                  },
                                }))
                              }
                            />
                          </div>
                        ))}
                      </div>
                    </div>
                  ))}
                </div>
              </>
            )}

            {error && <p className="text-sm text-destructive">{error}</p>}
            <div className="flex justify-between pt-2">
              <Button variant="outline" onClick={() => setStep(1)} disabled={loading}>
                Atrás
              </Button>
              <div className="flex gap-2">
                {pendingCount > 1 && (
                  <Button variant="outline" onClick={handleSaveAll} disabled={loading}>
                    {loading ? "Guardando..." : `Guardar todos (${pendingCount})`}
                  </Button>
                )}
                <Button onClick={handleSaveCurrent} disabled={!canSave}>
                  {loading ? "Guardando..." : "Guardar"}
                </Button>
              </div>
            </div>
          </div>
        )}
      </DialogContent>
    </Dialog>
  );
}
