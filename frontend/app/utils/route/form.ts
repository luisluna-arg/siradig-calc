export function showToast(toast: any, actionData: any)
{
    let description = `${actionData?.title ?? ""}`;
    description += `${actionData?.statusText ?? ""}`;
    if (actionData?.errorData?.errors) {
      description += `${Object.keys(actionData?.errorData?.errors)?.map(
        (k: any) =>
          `\n\t${k}: ${JSON.stringify(actionData?.errorData?.errors[k])}`
      )}`;
    }
    toast({
      title: "Error during Submit operation",
      description: description,
      variant: "destructive",
      duration: Infinity,
    });
}