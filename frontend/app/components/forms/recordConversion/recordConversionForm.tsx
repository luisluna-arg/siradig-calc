import { useLoaderData } from "@remix-run/react";
import { RecordFlat as DataRecordFlat } from "@/data/interfaces/Record";
import { RecordValue } from "@/data/interfaces/RecordValue";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { TemplateLink } from "@/data/interfaces/TemplateLink";
import { cn } from "@/lib/utils";

interface DataConversion {
  id: string;
  haberes: number;
  retenciones: number;
  neto: number;
  values: Array<RecordValue>;
  recordTemplateLinkId: string;
  recordTemplateLink: TemplateLink;
  source: DataRecordFlat;
  target: DataRecordFlat;
}

export default function RecordConversionForm() {
  const data = useLoaderData() as DataConversion;

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
              <TableCell className={cn(["w-20"])}>Title</TableCell>
              <TableCell className={cn("font-medium")}>{title}</TableCell>
            </TableRow>
            <TableRow>
              <TableCell className={cn(["w-20"])}>Description</TableCell>
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
            <TableHead>Item</TableHead>
            <TableHead className="text-right">Value</TableHead>
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

  return (
    <div className="flex flex-row">
      <div className="flex flex-col border-r p-5 max-w-50">
        <h2 className="text-xl font-semibold mb-4">Details</h2>
        <HeaderTable
          tableHeader={"Source"}
          description={data.source.description}
          templateName={data.source.name}
          title={data.source.title}
          className={"mb-5"}
        />
        <HeaderTable
          tableHeader={"Target"}
          description={data.target.description}
          templateName={data.target.name}
          title={data.target.title}
        />
      </div>
      <div className="flex flex-col p-5">
        <h2 className="text-xl font-semibold mb-4">Conversion</h2>
        <div className="flex flex-row mb-5">
          <div className="space-y-3">
            <ConversionTable
              tableHeader="Origin"
              values={[
                { label: "Haberes", value: data.haberes },
                { label: "Retenciones", value: data.retenciones },
                { label: "Neto", value: data.neto },
              ]}
            />
          </div>
          <div>
            <ConversionTable
              tableHeader="F572"
              values={data.values.map((value) => {
                return {
                  label: value.label,
                  value: value.value,
                };
              })}
            />
          </div>
        </div>
        <h2 className="text-xl font-semibold mb-4">Values</h2>
        <div className={"flex flex-row"}>
          <ConversionTable tableHeader="Source" values={data.source.values} />
          <ConversionTable tableHeader="Target" values={data.target.values} />
        </div>
      </div>
    </div>
  );
}
