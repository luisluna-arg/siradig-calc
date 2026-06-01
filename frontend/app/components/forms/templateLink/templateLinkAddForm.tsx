import { Form, useLoaderData } from "@remix-run/react";
import { cn } from "@/lib/utils";
import { ComboBox } from "@/components/utils/comboBox";
import { ActionButton } from "@/components/utils/actionButton";
import { Catalog } from "@/data/interfaces/Catalog";

export default function TemplateLinkAddForm() {
  const { templateCatalog } = useLoaderData() as {
    templateCatalog: Array<Catalog<string>>;
  };

  const comboButtonClass = cn(["min-w-80"]);

  return (
    <Form method="post">
      <div className={cn(["grid", "grid-cols-2", "gap-6", "justify-between"])}>
        <ComboBox
          placeholder={"Select left template..."}
          searchPlaceholder={"Search template..."}
          className={cn("ml-auto")}
          buttonClassName={comboButtonClass}
          data={templateCatalog}
          value={undefined}
          name={"leftTemplateId"}
        />
        <ComboBox
          placeholder={"Select right template..."}
          searchPlaceholder={"Search template..."}
          className={cn("mr-auto")}
          buttonClassName={comboButtonClass}
          data={templateCatalog}
          value={undefined}
          name={"rightTemplateId"}
        />
        <div className={cn(["flex", "flex-row", "justify-end", "col-span-2"])}>
          <ActionButton type="submit" text="Create Link" />
        </div>
      </div>
    </Form>
  );
}
