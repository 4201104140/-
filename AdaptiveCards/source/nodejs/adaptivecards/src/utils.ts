import * as Enums from "./enums";
import * as Shared from "./shared";

export function parseString(obj: any, defaultValue?: string): string | undefined {
  return typeof obj === "string" ? obj : defaultValue;
}

export function parseNumber(obj: any, defaultValue?: number): number | undefined {
  return typeof obj === "number" ? obj : defaultValue;
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

export function getEnumValueByName(
  enumType: { [s: number]: string },
  name: string
): number | undefined {
  // eslint-disable-next-line guard-for-in
  for (const key in enumType) {
    const keyAsNumber = parseInt(key, 10);

    if (keyAsNumber >= 0) {
      const value = enumType[key];

      if (value && typeof value === "string" && value.toLowerCase() === name.toLowerCase()) {
        return keyAsNumber;
      }
    }
  }

  return undefined;
}

export function parseEnum(
  enumType: { [s: number]: string },
  name: string,
  defaultValue?: number
): number | undefined {
  if (!name) {
    return defaultValue;
  }

  const enumValue = getEnumValueByName(enumType, name);

  return enumValue !== undefined ? enumValue : defaultValue;
}