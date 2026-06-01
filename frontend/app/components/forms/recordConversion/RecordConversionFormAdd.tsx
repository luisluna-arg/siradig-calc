import { useRef } from "react";
import { Form, useLoaderData } from "@remix-run/react";
import { cn } from "@/lib/utils";
import { ComboBox } from "@/components/utils/comboBox";
import { ActionButton } from "@/components/utils/actionButton";

export default function RecordConversionFormAdd() {
  const formRef = useRef<HTMLFormElement>(null);

  const { recordCatalog, templateCatalog } = useLoaderData() as {
    recordCatalog?: any;
    templateCatalog?: any;
  };

  const comboButtonClass = cn(["min-w-80"]);

  return (
    <Form ref={formRef} method={"post"}>
      <div className={cn(["grid", "grid-cols-2", "gap-6", "justify-between"])}>
        <ComboBox
          placeholder={"Select record..."}
          searchPlaceholder={"Search record..."}
          className={cn("ml-auto")}
          buttonClassName={cn(comboButtonClass)}
          data={recordCatalog}
          value={undefined}
          name={"recordId"}
        />
        <ComboBox
          placeholder={"Select template..."}
          searchPlaceholder={"Search template..."}
          className={cn("mr-auto")}
          buttonClassName={cn(comboButtonClass)}
          data={templateCatalog}
          value={undefined}
          name={"templateId"}
        />
        <div className={cn(["flex", "flex-row", "justify-end", "col-span-2"])}>
          <ActionButton type="submit" text="Submit" />
        </div>
      </div>
    </Form>
  );
}
