import { useState } from "react";
import { useLoaderData } from "@remix-run/react";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { useToast } from "@/hooks/use-toast";
import { useNavigate } from "@remix-run/react";
import { cn } from "@/lib/utils";
import { Button } from "@/components/ui/button";
import { Trash2Icon } from "lucide-react";
import { ApiClientProvider } from "@/data/ApiClientProvider";
import { Catalog } from "@/data/interfaces/Catalog";
import { RecordConversion } from "@/data/interfaces/RecordConversion";
import { ActionButton } from "../utils/actionButton";
import { ComboBox } from "../utils/comboBox";

const ALL_TEMPLATES_ID = "";

export default function ConversionsGrid() {
  const apiClient = new ApiClientProvider();
  const { toast } = useToast();
  const navigate = useNavigate();
  const { conversions, templateCatalog } = useLoaderData() as {
    conversions: Array<RecordConversion>;
    templateCatalog: Array<Catalog<string>>;
  };
  const [sourceTemplateFilter, setSourceTemplateFilter] = useState(ALL_TEMPLATES_ID);
  const [targetTemplateFilter, setTargetTemplateFilter] = useState(ALL_TEMPLATES_ID);

  const templateFilterOptions: Array<Catalog<string>> = [
    { id: ALL_TEMPLATES_ID, label: "Todos" },
    ...templateCatalog,
  ];

  const data = conversions.filter(
    (conversion) =>
      (!sourceTemplateFilter ||
        conversion.source.recordTemplateId === sourceTemplateFilter) &&
      (!targetTemplateFilter ||
        conversion.target.recordTemplateId === targetTemplateFilter)
  );

  const baseRoute = "/records/conversions";

  const handleAdd = async () => {
    navigate(`${baseRoute}/add`);
  };

  const handleDelete = async (sourceId: string, conversionId: string) => {
    try {
      await apiClient.Conversions.deleteByIds(sourceId, conversionId);
      navigate(`/records/conversions`);
    } catch (error: any) {
      toast({
        title: "Error al eliminar el elemento",
        description: error.response?.data?.message || error.message,
        variant: "destructive",
      });
    }
  };

  return (
    <div className="flex flex-col items-center gap-4 py-6 px-20">
      <div className="flex items-center gap-6 self-start">
        <div className="flex items-center gap-2">
          <span className="text-sm font-medium">Template origen:</span>
          <ComboBox
            placeholder="Todos"
            searchPlaceholder="Buscar template..."
            buttonClassName="min-w-60"
            data={templateFilterOptions}
            value={sourceTemplateFilter}
            onSelect={(entry) =>
              setSourceTemplateFilter(entry?.id ?? ALL_TEMPLATES_ID)
            }
          />
        </div>
        <div className="flex items-center gap-2">
          <span className="text-sm font-medium">Template destino:</span>
          <ComboBox
            placeholder="Todos"
            searchPlaceholder="Buscar template..."
            buttonClassName="min-w-60"
            data={templateFilterOptions}
            value={targetTemplateFilter}
            onSelect={(entry) =>
              setTargetTemplateFilter(entry?.id ?? ALL_TEMPLATES_ID)
            }
          />
        </div>
      </div>
      <Table>
        <TableHeader>
          <TableRow>
            <TableHead className={cn(["text-center"])} colSpan={3}>
              Origen
            </TableHead>
            <TableHead className={cn(["text-center", "border-l"])} colSpan={3}>
              Destino
            </TableHead>
            <TableHead className={cn(["text-center", "border-l"])}></TableHead>
          </TableRow>
          <TableRow>
            <TableHead className={cn(["w-auto"])}>Template</TableHead>
            <TableHead className={cn(["w-auto"])}>Título</TableHead>
            <TableHead className={cn(["w-60"])}>Descripción</TableHead>
            <TableHead className={cn(["w-auto", "border-l"])}>
              Template
            </TableHead>
            <TableHead className={cn(["w-auto"])}>Título</TableHead>
            <TableHead className={cn(["w-60"])}>Descripción</TableHead>
            <TableHead className={cn(["w-auto", "border-l"])}>
              <ActionButton
                type="add"
                onClick={async () => {
                  await handleAdd();
                }}
              />
            </TableHead>
          </TableRow>
        </TableHeader>
        <TableBody>
          {data.map((conversion) => (
            <TableRow
              key={conversion.id}
              onClick={() => {
                navigate(`/records/conversions/${conversion.id}`);
              }}
              className="cursor-pointer"
            >
              <TableCell className={cn("font-medium")}>
                {conversion.source.name}
              </TableCell>
              <TableCell className={cn("font-medium")}>
                {conversion.source.title}
              </TableCell>
              <TableCell className={cn("font-medium")}>
                {conversion.source.description}
              </TableCell>
              <TableCell className={cn("font-medium", "border-l")}>
                {conversion.target.name}
              </TableCell>
              <TableCell className={cn("font-medium")}>
                {conversion.target.title}
              </TableCell>
              <TableCell className={cn("font-medium")}>
                {conversion.target.description}
              </TableCell>
              <TableCell className={cn("font-medium", "border-l")}>
                <Button
                  variant="destructive"
                  size="icon"
                  onClick={async (e) => {
                    e.stopPropagation();
                    await handleDelete(conversion.source.id, conversion.id);
                  }}
                >
                  <Trash2Icon />
                </Button>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </div>
  );
}
