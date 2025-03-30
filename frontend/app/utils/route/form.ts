export function showToast(toast: any, actionData: any) {
  console.log("actionData", actionData);

  const errorData = actionData?.errorData ?? actionData?.data;
  console.log("errorData", errorData);

  let description = `${actionData?.title ?? errorData?.title ?? ""}`;
  description += `${actionData?.statusText ?? errorData?.statusText ?? ""}`;
  description += `${actionData?.message ?? errorData?.message ?? ""}`;
  description += `${actionData?.Message ?? errorData?.Message ?? ""}`;

  let errors = errorData?.errors ?? errorData?.Errors;
  if (errors) {
    if (!Array.isArray(errors)) {
      errors = Object.keys(errors)?.map((k) => ({
        property: k,
        error: errors[k],
      }));
    }

    description += `${errors?.map(
      (errorItem: any) =>
        `\n\t${errorItem.property}: ${JSON.stringify(errorItem.error)}`
    )}`;
  }
  toast({
    title: "Error during Submit operation",
    description: description,
    variant: "destructive",
    duration: Infinity,
  });
}
