import { SchemaClass } from "./SchemaClass";
import { SchemaType } from "./SchemaType";
import { SchemaEnum } from "./SchemaEnum";

export class Schema {
  private _typeDictionary: Map<string, SchemaType> = new Map<string, SchemaType>();

  constructor(types: any[]) {

    types.forEach(type => {
      this._typeDictionary.set(type.type, this.parse(type));
    });

    // Resolve types
    this._typeDictionary.forEach(type => {
      
    })
  }
}