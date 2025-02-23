import { useLoaderData } from "@remix-run/react";
import {
  Table,
  TableBody,
  TableCell,
  TableFooter,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { useToast } from "@/hooks/use-toast";
import { Record } from "@/data/interfaces/Record";
import { useNavigate } from "@remix-run/react";
import { cn } from "@/lib/utils";
import { ApiClient } from "@/data/ApiClient";
import ActionButton from "./utils/actionButton";

export default function RecordsGrid() {
  const apiClient = new ApiClient();
  const data = useLoaderData() as Array<Record>;
  const { toast } = useToast();
  const navigate = useNavigate();

  const handleAdd = async () => {
    navigate(`/records/add`);
  };

  const handleDelete = async (id: string) => {
    try {
      await apiClient.deleteRecord(id);
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
            <TableHead className={cn(["w-10"])}>
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
              <TableCell className="font-medium">
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
    </div>
  );
}
