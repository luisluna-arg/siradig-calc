import { useLoaderData } from "@remix-run/react";
import {
  Card,
  CardContent,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { Record as DataRecord } from "@/data/interfaces/Record";
import {
  Tabs,
  TabsContent,
  TabsList,
  TabsTrigger,
} from "@/components/ui/tabs";
import { Label } from "@/components/ui/label";
import { Separator } from "@/components/ui/separator";
import { RecordValue } from "../data/interfaces/RecordValue";
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table";

export default function RecordForm() {
  const data = useLoaderData() as DataRecord;

  const idLookup: Record<string, string> = data.values.reduce(
    (
      lookup: Record<string, string>,
      item: RecordValue
    ): Record<string, string> => {
      lookup[item.fieldId] = item.value;
      return lookup;
    },
    {} as Record<string, string>
  );

  return (
    <div className="flex justify-center items-center min-h-screen p-4">
      <Card className="w-full max-w-lg bg-white">
        <CardHeader>
          <CardTitle>
            {data.template.name}: {data.template.description}
          </CardTitle>
        </CardHeader>
        <CardContent>
          <Tabs
            defaultValue={data.template.sections[0].id}
            className="w-[400px]"
          >
            <TabsList>
              {data.template.sections.map((section) => (
                <TabsTrigger key={section.id} value={section.id}>
                  {section.name}
                </TabsTrigger>
              ))}
            </TabsList>
            <Separator className="my-4" />
            {data.template.sections.map((section) => (
              <TabsContent key={section.id} value={section.id}>
                <Table>
                  <TableHeader>
                    <TableRow>
                      <TableHead>Item</TableHead>
                      <TableHead className="text-right">Value</TableHead>
                    </TableRow>
                  </TableHeader>
                  <TableBody>
                    {section.fields.map((field) => (
                      <TableRow key={field.id}>
                        <TableCell>{field.label}</TableCell>
                        <TableCell className="text-right">{idLookup[field.id] ?? "-"}</TableCell>
                      </TableRow>
                    ))}
                  </TableBody>
                </Table>
              </TabsContent>
            ))}
          </Tabs>
        </CardContent>
      </Card>
    </div>
  );
}
