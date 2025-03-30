import { useLoaderData } from "@remix-run/react";
import { Template } from "@/data/interfaces/Template";
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs";
import { Label } from "@/components/ui/label";
import { Separator } from "@/components/ui/separator";

export default function TemplateDisplay() {
  const { template } = useLoaderData() as { template: Template };

  return (
    <div className="flex justify-center items-center min-h-screen p-4">
      <Tabs defaultValue={template.sections[0].id}>
        <TabsList>
          {template.sections.map((section) => (
            <TabsTrigger key={section.id} value={section.id}>
              {section.name}
            </TabsTrigger>
          ))}
        </TabsList>
        <Separator className="my-4" />
        {template.sections.map((section) => (
          <TabsContent key={section.id} value={section.id}>
            {section.fields.map((field) => (
              <div key={field.id}>
                <Label>{field.label}</Label>
              </div>
            ))}
          </TabsContent>
        ))}
      </Tabs>
    </div>
  );
}
