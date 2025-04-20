import { useLoaderData } from "@remix-run/react";
import RecordDisplay from "@/components/forms/record/recordDisplay";
import RecordEditForm from "@/components/forms/record/recordEditForm";

export default function RecordForm() {
  const { isEdit } = useLoaderData() as {
    isEdit: boolean;
  };

  if (isEdit) {
    return <RecordEditForm />;
  } else {
    return <RecordDisplay />;
  }
}
