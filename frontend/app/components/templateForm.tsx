import { useLoaderData } from "@remix-run/react";
import TemplateEditForm from "./templateEditForm";
import TemplateDisplay from "./templateDisplay";

export default function TemplateForm() {
  const { isEdit } = useLoaderData() as {
    isEdit: boolean;
  };

  if (isEdit) {
    return <TemplateEditForm />;
  } else {
    return <TemplateDisplay />;
  }
}
