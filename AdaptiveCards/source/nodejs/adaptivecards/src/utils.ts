import * as Enums from "./enums";
import * as Shared from "./shared";

export function parseString(obj: any, defaultValue?: string): string | undefined {
  return typeof obj === "string" ? obj : defaultValue;
}

export function parseBool(value: any, defaultValue?: boolean): boolean | undefined {
  if (typeof value === "boolean") {
    return value;
  } else if (typeof value === "string") {
    switch (value.toLowerCase()) {
      case "true":
        return true;
      case "false":
        return false;
      default:
        return defaultValue;
    }
  }

  return defaultValue;
}