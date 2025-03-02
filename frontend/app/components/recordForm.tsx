import { useLoaderData } from "@remix-run/react";
import RecordDisplay from "./recordDisplay";
import RecordEditForm from "./recordEditForm";

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
