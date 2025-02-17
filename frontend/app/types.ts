import { Params } from "@remix-run/react";

export interface Handle {
  title?: string;
}

export interface TypedRouteMatch {
  id: string;
  pathname: string;
  params: Params;
  data: unknown;
  handle: Handle;
}
