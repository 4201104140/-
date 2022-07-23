import * as Enums from "./enums";
import * as Shared from "./shared";

export function parseString(obj: any, defaultValue?: string): string | undefined {
  return typeof obj === "string" ? obj : defaultValue;
}