import { useLoaderData, useNavigate } from "@remix-run/react";
import { useToast } from "@/hooks/use-toast";
import { ApiClientProvider } from "@/data/ApiClientProvider";
import { ActionButton } from "@/components/utils/actionButton";
import {
  Table,
  TableBody,
  TableCell,
  TableFooter,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { TemplateLinkReduced } from "@/data/interfaces/TemplateLinkReduced";
import { cn } from "@/lib/utils";

export default function TemplatesGrid() {
  const data = useLoaderData() as Array<TemplateLinkReduced>;
  const navigate = useNavigate();
  const { toast } = useToast();
  const apiClient = new ApiClientProvider();

  const baseRoute = "/records/templates/links";

  const handleAdd = async () => {
    navigate(`${baseRoute}/add`);
  };

  const handleEdit = async (leftTemplateId: string, rightTemplateId: string) => {
    navigate(`${baseRoute}/${leftTemplateId}/to/${rightTemplateId}?edit=true`);
  };

  const handleDelete = async (leftTemplateId: string, rightTemplateId: string) => {
    try {
      await apiClient.TemplateLinks.deleteByIds(leftTemplateId, rightTemplateId);
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
            <TableHead className={cn(["w-2", "text-center"])} colSpan={2}>
              Left
            </TableHead>
            <TableHead className={cn(["w-2", "text-center"])} colSpan={2}>
              Right
            </TableHead>
          </TableRow>
          <TableRow>
            <TableHead className={cn(["w-80"])}>Name</TableHead>
            <TableHead className={cn(["w-80"])}>Description</TableHead>
            <TableHead className={cn(["w-auto"])}>Name</TableHead>
            <TableHead className={cn(["w-40", "text-right"])}>
              Description
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
          {data.map((link) => (
            <TableRow
              key={link.id}
              onClick={() =>
                navigate(
                  `${baseRoute}/${link.leftTemplate.id}/to/${link.rightTemplate.id}`
                )
              }
              className="cursor-pointer"
            >
              <TableCell className="font-medium">
                {link.leftTemplate.name}
              </TableCell>
              <TableCell className="font-medium">
                {link.leftTemplate.description}
              </TableCell>
              <TableCell className="font-medium">
                {link.rightTemplate.name}
              </TableCell>
              <TableCell className="font-medium">
                {link.rightTemplate.description}
              </TableCell>
              <TableCell className="font-medium flex flex-row gap-2">
                <ActionButton
                  type="edit"
                  onClick={async (e) => {
                    e.stopPropagation();
                    await handleEdit(link.leftTemplate.id, link.rightTemplate.id);
                  }}
                />
                <ActionButton
                  type="delete"
                  onClick={async (e) => {
                    e.stopPropagation();
                    await handleDelete(link.leftTemplate.id, link.rightTemplate.id);
                  }}
                />
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
        <TableFooter>
          <TableRow>
            <TableCell colSpan={4}>{`Total links: ${data.length}`}</TableCell>
          </TableRow>
        </TableFooter>
      </Table>
    </div>
  );
}
