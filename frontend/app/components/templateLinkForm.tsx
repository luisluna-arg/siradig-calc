import { MetaFunction, useLoaderData } from "@remix-run/react";
import {
  Card,
  CardContent,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import {
  Tabs,
  TabsContent,
  TabsList,
  TabsTrigger,
} from "@/components/ui/tabs";
import { TemplateLink } from "@/data/interfaces/TemplateLink";
import { RecordTemplateFieldLink } from "../data/interfaces/RecordTemplateFieldLink";
import { Separator } from "./ui/separator";
import { Label } from "./ui/label";

export default function TemplateLinkForm() {
  const data = useLoaderData() as TemplateLink;

  console.log(data);

  return (
    <div className="flex justify-center items-center min-h-screen p-4">
      <Card className="bg-white">
        <CardHeader>
          <CardTitle>Link {data.leftTemplate.name} to {data.rightTemplate.name}</CardTitle>
        </CardHeader>
        <CardContent>
          <Tabs defaultValue={data.recordTemplateFieldLinks[0].id}>
            <TabsList>
              {data.recordTemplateFieldLinks.map((recordTemplateFieldLink: RecordTemplateFieldLink) => (
                <TabsTrigger key={recordTemplateFieldLink.rightField.id} value={recordTemplateFieldLink.rightField.id}>
                  {recordTemplateFieldLink.rightField.label}
                </TabsTrigger>
              ))}
            </TabsList>
            <Separator className="my-4" />
            {data.recordTemplateFieldLinks.map((recordTemplateFieldLink: RecordTemplateFieldLink) => (
              <TabsContent key={recordTemplateFieldLink.rightField.id} value={recordTemplateFieldLink.rightField.id}>
                {recordTemplateFieldLink.leftFields.map((field) => (
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
