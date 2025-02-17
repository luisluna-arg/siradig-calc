import { TypedRouteMatch } from "./types";

export function hasHandleWithTitle(
  match: any
): match is TypedRouteMatch {
  return (
    match.handle !== undefined &&
    typeof match.handle.title === "string"
  );
}
