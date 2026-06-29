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

interface ImportRecordModalProps {
  open: boolean;
  onClose: () => void;
  onSaved: () => void;
}

export default function ImportRecordModal({ open, onClose, onSaved }: ImportRecordModalProps) {
  const { toast } = useToast();
  const fileInputRef = useRef<HTMLInputElement>(null);

  const [step, setStep] = useState<1 | 2>(1);
  const [templateCatalog, setTemplateCatalog] = useState<Catalog<string>[]>([]);
  const [selectedTemplateId, setSelectedTemplateId] = useState<string | null>(null);
  const [file, setFile] = useState<File | null>(null);
  const [fullTemplate, setFullTemplate] = useState<Template | null>(null);
  const [fieldValues, setFieldValues] = useState<Record<string, string>>({});
  const [title, setTitle] = useState("");
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    if (!open) return;
    const apiClient = new ApiClientProvider();
    apiClient.Catalogs.getTemplates()
      .then((data) => setTemplateCatalog(data as unknown as Catalog<string>[]))
      .catch(() => setError("Failed to load templates"));
  }, [open]);

  useEffect(() => {
    if (!open) {
      setStep(1);
      setSelectedTemplateId(null);
      setFile(null);
      setFullTemplate(null);
      setFieldValues({});
      setTitle("");
      setError(null);
      if (fileInputRef.current) fileInputRef.current.value = "";
    }
  }, [open]);

  const handleImport = async () => {
    if (!selectedTemplateId || !file) return;
    setLoading(true);
    setError(null);
    try {
      const apiClient = new ApiClientProvider();
      const ext = file.name.split(".").pop()?.toLowerCase();
      const result: ImportResult =
        ext === "pdf"
          ? await apiClient.Records.importPdf(selectedTemplateId, file)
          : await apiClient.Records.importCsv(selectedTemplateId, file);

      const template = await apiClient.Templates.get(selectedTemplateId);

      const values: Record<string, string> = {};
      result.values.forEach((v) => {
        values[v.fieldId] = v.value;
      });

      setFullTemplate(template);
      setFieldValues(values);
      setTitle(result.title || template.name);
      setStep(2);
    } catch (err: any) {
      const msg = err.response?.data?.message ?? err.message ?? "Import failed";
      setError(msg);
      toast({ title: "Import error", description: msg, variant: "destructive" });
    } finally {
      setLoading(false);
    }
  };

  const handleSave = async () => {
    if (!fullTemplate || !selectedTemplateId) return;
    setLoading(true);
    setError(null);
    try {
      const apiClient = new ApiClientProvider();
      const postData: RecordPostData = {
        templateId: selectedTemplateId,
        title,
        values: Object.entries(fieldValues)
          .filter(([, v]) => v !== "")
          .map(([fieldId, value]) => ({ id: "", fieldId, value })),
      };
      await apiClient.Records.post(postData);
      onSaved();
      onClose();
    } catch (err: any) {
      const msg = err.response?.data?.message ?? err.message ?? "Failed to save record";
      setError(msg);
      toast({ title: "Save error", description: msg, variant: "destructive" });
    } finally {
      setLoading(false);
    }
  };

  const canImport = !!selectedTemplateId && !!file && !loading;
  const canSave = !!selectedTemplateId && !loading;

  return (
    <Dialog open={open} onOpenChange={(isOpen) => !isOpen && onClose()}>
      <DialogContent className={cn("max-w-3xl max-h-[90vh] overflow-y-auto")}>
        <DialogHeader>
          <DialogTitle>
            {step === 1 ? "Import record from file" : "Review & save record"}
          </DialogTitle>
        </DialogHeader>

        {step === 1 && (
          <div className="flex flex-col gap-6 pt-2">
            <div className="flex flex-col gap-2">
              <Label>Template</Label>
              <ComboBox
                placeholder="Select template..."
                searchPlaceholder="Search template..."
                data={templateCatalog}
                value={selectedTemplateId ?? undefined}
                onSelect={(entry) => setSelectedTemplateId(entry?.id ?? null)}
              />
            </div>
            <div className="flex flex-col gap-2">
              <Label htmlFor="import-file">File (.pdf or .csv)</Label>
              <input
                id="import-file"
                ref={fileInputRef}
                type="file"
                accept=".pdf,.csv"
                className="text-sm file:mr-4 file:py-1 file:px-3 file:rounded file:border-0 file:text-sm file:bg-muted file:text-muted-foreground hover:file:bg-muted/80 cursor-pointer"
                onChange={(e) => setFile(e.target.files?.[0] ?? null)}
              />
            </div>
            {error && <p className="text-sm text-destructive">{error}</p>}
            <div className="flex justify-end">
              <Button onClick={handleImport} disabled={!canImport}>
                {loading ? "Importing..." : "Import"}
              </Button>
            </div>
          </div>
        )}

        {step === 2 && fullTemplate && (
          <div className="flex flex-col gap-6 pt-2">
            <div className="flex flex-col gap-2">
              <Label htmlFor="import-title">Title</Label>
              <Input
                id="import-title"
                type="text"
                value={title}
                onChange={(e) => setTitle(e.target.value)}
                placeholder="Record title"
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
                          value={fieldValues[field.id] ?? ""}
                          onChange={(e) =>
                            setFieldValues((prev) => ({
                              ...prev,
                              [field.id]: e.target.value,
                            }))
                          }
                        />
                      </div>
                    ))}
                  </div>
                </div>
              ))}
            </div>
            {error && <p className="text-sm text-destructive">{error}</p>}
            <div className="flex justify-between pt-2">
              <Button variant="outline" onClick={() => setStep(1)} disabled={loading}>
                Back
              </Button>
              <Button onClick={handleSave} disabled={!canSave}>
                {loading ? "Saving..." : "Save"}
              </Button>
            </div>
          </div>
        )}
      </DialogContent>
    </Dialog>
  );
}
