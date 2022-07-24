import { SchemaType } from "./SchemaType";

export class SchemaLiteral extends SchemaType {

  private _schemaType?: string;
  private _schemaFormat?: string;

  constructor(type: string, schemaType?: string, schemaFormat?: string) {
    super({
      type: type
    });
    this._schemaType = schemaType;
    this._schemaFormat = schemaFormat;
  }

  get schemaType() {
    return this._schemaType;
  }

  get schemaFormat() {
    return this._schemaFormat;
  }

  resolve(types: Map<string, SchemaType>): void {
      
  }

  private static _string = new SchemaLiteral("string", "string");

  static get string() {
    return this._string;
  }
}