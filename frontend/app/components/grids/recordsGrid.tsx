import { useState } from "react";
import { useNavigate, useLoaderData } from "@remix-run/react";
import {
  Table,
  TableBody,
  TableCell,
  TableFooter,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { ActionButton } from "@/components/utils/actionButton";
import { Button } from "@/components/ui/button";
import { Upload } from "lucide-react";
import { ApiClientProvider } from "@/data/ApiClientProvider";
import { Record } from "@/data/interfaces/Record";
import { cn } from "@/lib/utils";
import { useToast } from "@/hooks/use-toast";
import ImportRecordModal from "@/components/forms/record/ImportRecordModal";

export default function RecordsGrid() {
  const apiClient = new ApiClientProvider();
  const data = useLoaderData() as Array<Record>;
  const { toast } = useToast();
  const navigate = useNavigate();
  const [importOpen, setImportOpen] = useState(false);

  const handleAdd = async () => {
    navigate(`/records/add`);
  };

  const handleEdit = async (id: string) => {
    navigate(`/records/${id}?edit=true`);
  };

  const handleDelete = async (id: string) => {
    try {
      await apiClient.Records.delete(id);
      navigate(`/records`);
    } catch (error: any) {
      toast({
        title: "Error deleting item",
        description: error.response?.data?.message || error.message,
        variant: "destructive",
      });
    }
  };

  return (
    <div className="flex justify-center items-center py-6 px-20">
      <Table>
        <TableHeader>
          <TableRow>
            <TableHead className={cn(["w-80"])}>Template</TableHead>
            <TableHead className={cn(["w-80"])}>Title</TableHead>
            <TableHead className={cn(["w-auto"])}>Description</TableHead>
            <TableHead className={cn(["w-40"])}>Section count</TableHead>
            <TableHead className={cn(["w-24", "text-right"])}>
              <div className="flex justify-end gap-1">
                <Button
                  variant="outline"
                  size="icon"
                  className={cn([
                    "bg-blue-100",
                    "hover:bg-blue-400",
                    "text-gray-500",
                    "dark:text-gray-500",
                    "hover:text-white",
                    "dark:hover:text-white",
                  ])}
                  onClick={() => setImportOpen(true)}
                >
                  <Upload />
                </Button>
                <ActionButton
                  type="add"
                  onClick={async () => {
                    await handleAdd();
                  }}
                />
              </div>
            </TableHead>
          </TableRow>
        </TableHeader>
        <TableBody>
          {data.map((record) => (
            <TableRow
              key={record.id}
              onClick={() => {
                navigate(`/records/${record.id}`);
              }}
              className="cursor-pointer"
            >
              <TableCell className="font-medium">
                {record.template.name}
              </TableCell>
              <TableCell className="font-medium">{record.title}</TableCell>
              <TableCell className="font-medium">
                {record.template.description}
              </TableCell>
              <TableCell className="font-medium">
                {record.template.sections.length ?? 0}
              </TableCell>
              <TableCell className="font-medium flex flex-row gap-2">
                <ActionButton
                  type="edit"
                  onClick={async (e) => {
                    e.stopPropagation();
                    await handleEdit(record.id);
                  }}
                />
                <ActionButton
                  type="delete"
                  onClick={async (e) => {
                    e.stopPropagation();
                    await handleDelete(record.id);
                  }}
                />
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
        <TableFooter>
          <TableRow>
            <TableCell
              colSpan={4}
            >{`Total templates: ${data.length}`}</TableCell>
          </TableRow>
        </TableFooter>
      </Table>
      <ImportRecordModal
        open={importOpen}
        onClose={() => setImportOpen(false)}
        onSaved={() => navigate("/records")}
      />
    </div>
  );
}
