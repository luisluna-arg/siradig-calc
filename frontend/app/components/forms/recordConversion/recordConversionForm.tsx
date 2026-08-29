import { Fragment, useState } from "react";
import { useLoaderData } from "@remix-run/react";
import { pdf } from "@react-pdf/renderer";
import { FileDown } from "lucide-react";
import { RecordFlat as DataRecordFlat } from "@/data/interfaces/Record";
import { FieldComposition } from "@/data/interfaces/FieldComposition";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { Button } from "@/components/ui/button";
import { Label } from "@/components/ui/label";
import { Separator } from "@/components/ui/separator";
import { TemplateLink } from "@/data/interfaces/TemplateLink";
import { useToast } from "@/hooks/use-toast";
import { cn } from "@/lib/utils";
import { ConversionPdfDocument } from "@/components/forms/recordConversion/ConversionPdfDocument";

interface DataConversion {
  id: string;
  haberes: number;
  retenciones: number;
  neto: number;
  values: Array<FieldComposition>;
  recordTemplateLinkId: string;
  recordTemplateLink: TemplateLink;
  source: DataRecordFlat;
  target: DataRecordFlat;
}

export default function RecordConversionForm() {
  const data = useLoaderData() as DataConversion;
  const { toast } = useToast();
  const [exporting, setExporting] = useState(false);

  const handleExportPdf = async () => {
    setExporting(true);
    try {
      const blob = await pdf(<ConversionPdfDocument data={data} />).toBlob();
      const url = URL.createObjectURL(blob);
      const link = document.createElement("a");
      link.href = url;
      link.download = `conversion-${data.target.title || data.id}.pdf`;
      document.body.appendChild(link);
      link.click();
      document.body.removeChild(link);
      URL.revokeObjectURL(url);
    } catch (error: any) {
      toast({
        title: "Error al exportar el PDF",
        description: error.message,
        variant: "destructive",
      });
    } finally {
      setExporting(false);
    }
  };

  function HeaderTable({
    tableHeader,
    templateName,
    title,
    description,
    className,
  }: {
    tableHeader: string;
    templateName: string;
    title: string;
    description: string;
    className?: string;
  }) {
    return (
      <div className={className}>
        <Table>
          <TableHeader>
            <TableRow>
              <TableHead
                className={cn(["text-center", "min-w-80"])}
                colSpan={2}
              >
                {tableHeader}
              </TableHead>
            </TableRow>
          </TableHeader>
          <TableBody>
            <TableRow>
              <TableCell className={cn(["w-20"])}>Template</TableCell>
              <TableCell className={cn("font-medium")}>
                {templateName}
              </TableCell>
            </TableRow>
            <TableRow>
              <TableCell className={cn(["w-20"])}>Título</TableCell>
              <TableCell className={cn("font-medium")}>{title}</TableCell>
            </TableRow>
            <TableRow>
              <TableCell className={cn(["w-20"])}>Descripción</TableCell>
              <TableCell className={cn("font-medium")}>{description}</TableCell>
            </TableRow>
          </TableBody>
        </Table>
      </div>
    );
  }

  function ConversionTable({
    tableHeader,
    values,
    className,
  }: {
    tableHeader: string;
    values: Array<any>;
    className?: string;
  }) {
    return (
      <Table className={cn("w-200 mr-5", className)}>
        <TableHeader>
          <TableRow>
            <TableHead className={"text-center"} colSpan={2}>
              {tableHeader}
            </TableHead>
          </TableRow>
          <TableRow>
            <TableHead>Ítem</TableHead>
            <TableHead className="text-right">Valor</TableHead>
          </TableRow>
        </TableHeader>
        <TableBody>
          {values.map((labelValue) => (
            <TableRow key={`${labelValue.label}-${labelValue.value}`}>
              <TableCell className="min-w-40 max-w-60">
                {labelValue.label}
              </TableCell>
              <TableCell>{labelValue.value}</TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    );
  }

  function CompositionCard({ composition }: { composition: FieldComposition }) {
    return (
      <div className="pr-8">
        <div className="flex items-baseline gap-4 mb-2 whitespace-nowrap">
          <Label>{composition.label}</Label>
          <span className="font-medium">{composition.value}</span>
        </div>
        <div className="text-xs text-muted-foreground mb-1">
          Compuesto por
        </div>
        <div className="flex flex-col gap-1">
          {composition.sourceFields.map((sourceField) => (
            <div
              key={sourceField.fieldId}
              className="flex items-center gap-4 text-sm bg-muted rounded px-3 py-2 whitespace-nowrap"
            >
              <span>{sourceField.label}</span>
              <span>{sourceField.value}</span>
            </div>
          ))}
        </div>
      </div>
    );
  }

  return (
    <div className="flex flex-col">
      <div className="flex justify-end items-center gap-2 px-5 py-2 border-b">
        <Button
          variant="outline"
          size="sm"
          onClick={handleExportPdf}
          disabled={exporting}
        >
          <FileDown className="h-4 w-4" />
          {exporting ? "Generando..." : "Exportar PDF"}
        </Button>
      </div>
      <div className="flex flex-row">
        <div className="flex flex-col border-r p-5 max-w-50">
          <h2 className="text-xl font-semibold mb-4">Detalles</h2>
          <HeaderTable
            tableHeader={"Origen"}
            description={data.source.description}
            templateName={data.source.name}
            title={data.source.title}
            className={"mb-5"}
          />
          <HeaderTable
            tableHeader={"Destino"}
            description={data.target.description}
            templateName={data.target.name}
            title={data.target.title}
          />
        </div>
        <div className="flex flex-col p-5">
          <h2 className="text-xl font-semibold mb-4">Conversión</h2>
          <div className="flex flex-row mb-5">
            <div className="space-y-3">
              <ConversionTable
                tableHeader="Origen"
                values={[
                  { label: "Haberes", value: data.haberes },
                  { label: "Retenciones", value: data.retenciones },
                  { label: "Neto", value: data.neto },
                ]}
              />
            </div>
          </div>
          <h2 className="text-xl font-semibold mb-4">Composición</h2>
          <div className="flex flex-row flex-wrap items-start gap-6">
            {data.values.map((composition, i) => (
              <Fragment key={composition.fieldId}>
                {i > 0 && <Separator orientation="vertical" className="self-stretch" />}
                <CompositionCard composition={composition} />
              </Fragment>
            ))}
          </div>
        </div>
      </div>
    </div>
  );
}
