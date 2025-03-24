import { useLoaderData } from "@remix-run/react";
import {
  Table,
  TableBody,
  TableCaption,
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
import { ApiClient } from "@/data/ApiClient";
import { RecordConversion } from "@/data/interfaces/RecordConversion";

export default function ConversionsGrid() {
  const apiClient = new ApiClient();
  const { toast } = useToast();
  const navigate = useNavigate();
  const data = useLoaderData() as Array<RecordConversion>;

  const handleDelete = async (sourceId: string, conversionId: string) => {
    try {
      await apiClient.deleteConversion(sourceId, conversionId);
      navigate(`/records/conversions`);
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
            <TableHead className={cn(["text-center"])} colSpan={3}>
              Source
            </TableHead>
            <TableHead className={cn(["text-center", "border-l"])} colSpan={3}>
              Target
            </TableHead>
            <TableHead className={cn(["text-center", "border-l"])}></TableHead>
          </TableRow>
          <TableRow>
            <TableHead className={cn(["w-auto"])}>Template</TableHead>
            <TableHead className={cn(["w-auto"])}>Title</TableHead>
            <TableHead className={cn(["w-60"])}>Description</TableHead>
            <TableHead className={cn(["w-auto", "border-l"])}>Template</TableHead>
            <TableHead className={cn(["w-auto"])}>Title</TableHead>
            <TableHead className={cn(["w-60"])}>Description</TableHead>
            <TableHead className={cn(["w-auto", "border-l"])}></TableHead>
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
