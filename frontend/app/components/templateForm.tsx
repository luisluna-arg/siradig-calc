import { useLoaderData } from "@remix-run/react";
import {
  Card,
  CardContent,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { Template } from "@/data/interfaces/Template";
import {
  Tabs,
  TabsContent,
  TabsList,
  TabsTrigger,
} from "@/components/ui/tabs";
import { Label } from "@/components/ui/label";
import { Separator } from "@/components/ui/separator";

export default function TemplateForm() {
  const data = useLoaderData() as Template;

  return (
    <div className="flex justify-center items-center min-h-screen p-4">
      <Card className="w-full max-w-lg bg-white">
        <CardHeader>
          <CardTitle>{data.name}: {data.description}</CardTitle>
        </CardHeader>
        <CardContent>
          <Tabs defaultValue={data.sections[0].id} className="w-[400px]">
            <TabsList>
              {data.sections.map((section) => (
                <TabsTrigger key={section.id} value={section.id}>
                  {section.name}
                </TabsTrigger>
              ))}
            </TabsList>
            <Separator className="my-4" />
            {data.sections.map((section) => (
              <TabsContent key={section.id} value={section.id}>
                {section.fields.map((field) => (
                  <div key={field.id}>
                    <Label>{field.label}</Label>
                  </div>
                ))}
              </TabsContent>
            ))}
          </Tabs>
        </CardContent>
      </Card>
    </div>
  );
}
