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
import { Template } from "../data/interfaces/Template";
import { useNavigate } from "@remix-run/react";

export default function TemplatesGrid() {
  const data = useLoaderData() as Array<Template>;

  const navigate = useNavigate();

  return (
    <div className="flex justify-center items-center min-h-screen p-4">
      <Card className="w-[80vw] bg-white">
        <CardContent>
          <Table>
            <TableCaption>Record templates</TableCaption>
            <TableHeader>
              <TableRow>
                <TableHead>Name</TableHead>
                <TableHead>Description</TableHead>
                <TableHead className="text-right">Section count</TableHead>
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
