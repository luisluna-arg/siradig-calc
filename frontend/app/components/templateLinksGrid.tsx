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
import { useNavigate } from "@remix-run/react";
import { TemplateLinkReduced } from "../data/interfaces/TemplateLinkReduced";
import { cn } from "@/lib/utils";

export default function TemplatesGrid() {
  const data = useLoaderData() as Array<TemplateLinkReduced>;

  const navigate = useNavigate();

  return (
    <div className="flex justify-center items-center py-6 px-20">
      <Table>
        <TableCaption>Record template links</TableCaption>
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
          </TableRow>
        </TableHeader>
        <TableBody>
          {data.map((link) => (
            <TableRow
              key={link.id}
              onClick={() =>
                navigate(
                  `/records/templates/links/${link.leftTemplate.id}/to/${link.rightTemplate.id}`
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
