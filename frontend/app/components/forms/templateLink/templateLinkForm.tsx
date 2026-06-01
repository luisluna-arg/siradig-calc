import { TemplateLinkFormDisplay } from "@/components/forms/templateLink/templateLinkFormDisplay";
import { useLoaderData } from "@remix-run/react";
import { TemplateLinkEditForm } from "@/components/forms/templateLink/templateLinkEditForm";

export function TemplateLinkForm() {
  const { isEdit } = useLoaderData() as {
    isEdit: boolean;
  };

  if (isEdit) {
    return <TemplateLinkEditForm />;
  } else {
    return <TemplateLinkFormDisplay />;
  }
}
