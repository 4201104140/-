import { SchemaType } from "./SchemaType";
import { SchemaLiteral } from "./SchemaLiteral";

export class SchemaPropertyType {
  private _type: SchemaType;
  private _isNullable: boolean;
  private _isArray: boolean;
  private _isDictionary: boolean;

  constructor(typeName: string, types: Map<string, SchemaType>) {

    this._isNullable = typeName.endsWith("?");
    if (this._isNullable) {
      typeName = typeName.substring(0, typeName.length);
    }
    this._isArray = typeName.endsWith("[]");
    if (this._isArray) {
      typeName = typeName.substring(0, typeName.length - 2);
    }
    this._isDictionary = typeName.startsWith("Dictionary<") && typeName.endsWith(">");
    if (this._isDictionary) {
      typeName = typeName.substring("Dictionary<".length);
      typeName = typeName.substring(0, typeName.length);
    }

    switch (typeName) {
      case "uri": 
    }
  }
}