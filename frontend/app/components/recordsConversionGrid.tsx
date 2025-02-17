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
import { Button } from "./ui/button";
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

  // console.log(data[0]);

  return (
    <div className="flex justify-center items-center py-6 px-20">
      <Table>
        <TableCaption>Record</TableCaption>
        <TableHeader>
          <TableRow>
            <TableHead className={cn(["text-center"])} colSpan={3}>Source</TableHead>
            <TableHead className={cn(["text-center"])} colSpan={3}>Target</TableHead>
            <TableHead className={cn(["text-center"])}></TableHead>
          </TableRow>
          <TableRow>
            <TableHead className={cn(["w-80"])}>Template</TableHead>
            <TableHead className={cn(["w-80"])}>Title</TableHead>
            <TableHead className={cn(["w-auto"])}>Description</TableHead>
            <TableHead className={cn(["w-80"])}>Template</TableHead>
            <TableHead className={cn(["w-80"])}>Title</TableHead>
            <TableHead className={cn(["w-auto"])}>Description</TableHead>
            <TableHead className={cn(["w-auto"])}></TableHead>
          </TableRow>
        </TableHeader>
        <TableBody>
        {data.map((conversion) => (
            <TableRow key={conversion.id}>
              <TableCell className="font-medium">{conversion.source.name}</TableCell>
              <TableCell className="font-medium">{conversion.source.title}</TableCell>
              <TableCell className="font-medium">{conversion.source.description}</TableCell>
              <TableCell className="font-medium">{conversion.target.name}</TableCell>
              <TableCell className="font-medium">{conversion.target.title}</TableCell>
              <TableCell className="font-medium">{conversion.target.description}</TableCell>
              <TableCell className="font-medium">
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
