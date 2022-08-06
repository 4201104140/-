export class Strings {
  static readonly errors = {
    unknownElementType: (typeName: string) =>
      `Unknown element type "${typeName}". Fallback will be used if present.`,
    unknownActionType: (typeName: string) => 
      `Unknown action type "${typeName}". Fallback will be used if present.`,
    elementTypeNotAllowed: (typeName: string) =>
      `Element type "${typeName}" is not allowed in this context.`,
    invalidPropertyValue: (value: any, propertyName: string) =>
      `Invalid value "${value}" for property "${propertyName}".`,
    invalidVersionString: (versionString: string) =>
      `Invalid version string "${versionString}".`,
    propertyValueNotSupported: (
      value: any,
      propertyName: string,
      supportedInVersion: string,
      versionUsed: string
    ) =>
      `Value "${value}" for property "${propertyName}" is supported in version "${supportedInVersion}, but you are using version ${versionUsed}."`,
    propertyNotSupported: (
      propertyName: string,
      supportedInVersion: string,
      versionUsed: string
    ) =>
      `Property "${propertyName}" is supported in version ${supportedInVersion}, but you are using version ${versionUsed}.`,
  }
}