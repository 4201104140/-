import { SchemaType } from "./SchemaType";
import { SchemaProperty } from "./SchemaProperty";

export class SchemaClass extends SchemaType {
  private _properties: Map<string, SchemaProperty> = new Map<string, SchemaProperty>();
  private _isAbstract: boolean = false;
  private _extends: SchemaClass[] = [];
  private _implementations: SchemaClass[] = [];
  private _shorthand?: SchemaProperty;

  constructor(sourceObj: any) {
    super(sourceObj);

    if (sourceObj.properties) {
      for (let key in sourceObj.properties) {
        this._properties.set(key, new SchemaProperty(key, sourceObj.properties[key]));
      }
    }
    if (sourceObj.isAbstract) {
      this._isAbstract = true;
    }
  }

  get properties() {
    return this._properties;
  }

  get isAbstract() {
    return this._isAbstract;
  }

  get extends() {
    return this._extends;
  }

  get implementation() {
    return this._implementations;
  }

  get shorthand() {
    return this._shorthand;
  }

  // Gets all extended, including descendants
  getAllExtended() {
    var answer: SchemaClass[] = [];
    this.extends.forEach(extended => {
      answer.push(extended);

      extended.getAllExtended().forEach(descendantExtended => {
        answer.push(descendantExtended);
      });
    });
    return answer;
  }

  getAllExtendedProperties() {
    
  }
}