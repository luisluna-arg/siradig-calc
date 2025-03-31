import { useLoaderData, useNavigate } from "@remix-run/react";
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
import { ApiClientProvider } from "@/data/ApiClientProvider";
import { Template } from "@/data/interfaces/Template";
import { useToast } from "@/hooks/use-toast";

import { cn } from "@/lib/utils";

export default function TemplatesGrid() {
  const data = useLoaderData() as Array<Template>;
  const navigate = useNavigate();
  const { toast } = useToast();
  const apiClient = new ApiClientProvider();

  const baseRoute = "/records/templates";

  const handleAdd = async () => {
    navigate(`${baseRoute}/add`);
  };

  const handleEdit = async (id: string) => {
    navigate(`${baseRoute}/${id}?edit=true`);
  };

  const handleDelete = async (id: string) => {
    try {
      await apiClient.Templates.delete(id);
      navigate(`${baseRoute}`);
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
            <TableHead className={cn(["w-80"])}>Name</TableHead>
            <TableHead className={cn(["w-auto"])}>Description</TableHead>
            <TableHead className={cn(["w-40", "text-right"])}>
              Section count
            </TableHead>
            <TableHead className={cn(["w-10", "text-right"])}>
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
          {data.map((template) => (
            <TableRow
              key={template.id}
              onClick={() => navigate(`/records/templates/${template.id}`)}
              className="cursor-pointer"
            >
              <TableCell className="font-medium">{template.name}</TableCell>
              <TableCell className="font-medium">
                {template.description}
              </TableCell>
              <TableCell className="text-right">
                {template.sections.length ?? 0}
              </TableCell>
              <TableCell className="font-medium flex flex-row gap-2">
                <ActionButton
                  type="edit"
                  onClick={async (e) => {
                    e.stopPropagation();
                    await handleEdit(template.id);
                  }}
                />
                <ActionButton
                  type="delete"
                  onClick={async (e) => {
                    e.stopPropagation();
                    await handleDelete(template.id);
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
