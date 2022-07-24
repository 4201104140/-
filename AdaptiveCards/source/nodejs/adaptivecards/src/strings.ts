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
  }
}