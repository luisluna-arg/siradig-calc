import { useLoaderData } from "@remix-run/react";
import { Card, CardContent } from "@/components/ui/card";
import {
  Table,
  TableBody,
  TableCaption,
  TableCell,
  TableFooter,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { Record } from "@/data/interfaces/Record";
import { useNavigate } from "@remix-run/react";

export default function RecordsGrid() {
  const data = useLoaderData() as Array<Record>;

  const navigate = useNavigate();

  return (
    <div className="flex justify-center items-center min-h-screen p-4">
      <Card className="w-[80vw] bg-white">
        <CardContent>
          <Table>
            <TableCaption>Record</TableCaption>
            <TableHeader>
              <TableRow>
                <TableHead>Template</TableHead>
                <TableHead>Title</TableHead>
                <TableHead>Description</TableHead>
                <TableHead className="text-right">Section count</TableHead>
              </TableRow>
            </TableHeader>
            <TableBody>
              {data.map((record) => (
                <TableRow
                  key={record.id}
                  onClick={() => navigate(`/records/${record.id}`)}
                  className="cursor-pointer"
                >
                  <TableCell className="font-medium">
                    {record.template.name}
                  </TableCell>
                  <TableCell className="font-medium">{record.title}</TableCell>
                  <TableCell className="font-medium">
                    {record.template.description}
                  </TableCell>
                  <TableCell className="text-right">
                    {record.template.sections.length ?? 0}
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
            <TableFooter>
              <TableRow>
                <TableCell
                  colSpan={2}
                >{`Total templates: ${data.length}`}</TableCell>
              </TableRow>
            </TableFooter>
          </Table>
        </CardContent>
      </Card>
    </div>
  );
}
