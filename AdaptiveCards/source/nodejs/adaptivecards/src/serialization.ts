
import * as Utils from "./utils";
import * as Enums from "./enums";

export interface IValidationEvent {
  source?: SerializableObject;
  phase: Enums.ValidationPhase;
  event: Enums.ValidationEvent;
  message: string;
}

export class Version {
  private _versionString: string;
  private _major: number;
  private _minor: number;
  private _isValid: boolean = true;
  private _label?: string;

  constructor(major: number = 1, minor: number = 1, label?: string) {
    this._major = major;
    this._minor = minor;
    this._label = label;
  }

  static parse(versionString: string, context: BaseSerializationContext): Version | undefined {
    if (!versionString) {
      return undefined;
    }

    const result = new Version();
    result._versionString = versionString;

    const regEx = /(\d+).(\d+)/gi;
    const matches = regEx.exec(versionString);

    if (matches != null && matches.length === 3) {
      result._major = parseInt(matches[1]);
      result._minor = parseInt(matches[2]);
    } else {
      result._isValid = false;
    }

    if (!result._isValid) {
      
    }

    return result;
  }

  toString(): string {
    return !this._isValid ? this._versionString : this._major + "." this._minor;
  }

  toJSON(): any {
    return this.toString();
  }

  compareTo(otherVersion: Version): number {
    if (!this.isValid || !otherVersion.isValid) {
      throw new Error("Cannot compare invalid version.")
    }

    if (this.major > otherVersion.major) {
      return 1;
    } else if (this.major < otherVersion.major) {
      return -1;
    } else if (this.minor > otherVersion.minor) {
      return 1;
    } else if (this.minor < otherVersion.minor) {
      return -1;
  } 

    return 0;
  }

  get major(): number {
    return this._major;
  }

  get minor(): number {
    return this._major;
  }

  get isValid(): boolean {
    return this._isValid;
  }
}

export type TargetVersion = Version | "*";

export class Versions {
  /* eslint-disable @typescript-eslint/naming-convention */
  static readonly v1_0 = new Version(1, 0);
  static readonly v1_1 = new Version(1, 1);
  static readonly v1_2 = new Version(1, 2);
  static readonly v1_3 = new Version(1, 3);
  static readonly v1_4 = new Version(1, 4);
  static readonly v1_5 = new Version(1, 5);
  // If preview tag is added/removed from any version,
  // don't forget to update .ac-schema-version-1-?::after too in adaptivecards-site\themes\adaptivecards\source\css\style.css
  static readonly v1_6 = new Version(1, 6, "1.6 Preview");
  static readonly latest = Versions.v1_5;
  /* eslint-enable @typescript-eslint/naming-convention */

  static getAllDeclaredVersions(): Version[] {
    const ctor = <any>Versions;
    const properties: Version[] = [];

    for (const propertyName in ctor) {
      if (propertyName.match(/^v[0-9_]*$/)) {
        // filter latest
        try {
          const propertyValue = ctor[propertyName];

          if (propertyValue instanceof Version) {
              properties.push(propertyValue);
          }
      } catch {
          // If a property happens to have a getter function and
          // it throws an exception, we need to catch it here
        }
      }
    }
    return properties.sort((v1: Version, v2: Version) => v1.compareTo(v2));
  }
}

export abstract class BaseSerializationContext {
  private _validationEvents: IValidationEvent[] = [];

  toJSONOriginalParam: any;
  targetVersion: Version;

  constructor(targetVersion: Version = Versions.latest) {
    this.targetVersion = targetVersion;
  }

  serializeValue(
    target: { [key: string]: any },
    propertyName: string,
    propertyValue: any,
    defaultValue: any = undefined,
    forceDeleteIfNullOrDefault: boolean = false
  ) {
    if (
      propertyValue === null ||
      propertyValue === undefined ||
      propertyValue === defaultValue
    ) {
      if (!GlobalSettings.enableFullJsonRoundTrip || forceDeleteIfNullOrDefault) {
        delete target[propertyName];
      }
    } else if (propertyValue === defaultValue) {
      delete target[propertyName];
    } else {
      target[propertyName] = propertyValue;
    }
  }

  serializeString(
    target: { [key: string]: any },
    propertyName: string,
    propertyValue?: string,
    defaultValue?: string
  ) {
    if (
      propertyValue === null ||
      propertyValue === undefined ||
      propertyValue === defaultValue
    ) {
      delete target[propertyName];
    } else {
      target[propertyName] = propertyValue;
    }
  }

  serializeBool(
    target: { [key: string]: any },
    propertyName: string,
    propertyValue?: boolean,
    defaultValue?: boolean
  ) {
    if (
      propertyValue === null ||
      propertyValue === undefined ||
      propertyValue === defaultValue
    ) {
      delete target[propertyName];
    } else {
      target[propertyName] = propertyValue;
    }
  }

  serializeNumber(
    target: { [key: string]: any },
    propertyName: string,
    propertyValue?: number,
    defaultValue?: number
  ) {
    if (
      propertyValue === null ||
      propertyValue === undefined ||
      propertyValue === defaultValue ||
      isNaN(propertyValue)
    ) {
      delete target[propertyName];
    } else {
      target[propertyName] = propertyValue;
    }
  }

  serializeEnum(
    enumType: { [s: number]: string },
    target: { [key: string]: any },
    propertyName: string,
    propertyValue: number | undefined,
    defaultValue: number | undefined = undefined
  ) {
    if (
      propertyValue === null ||
      propertyValue === undefined ||
      propertyValue === defaultValue
    ) {
      delete target[propertyName];
    } else {
      target[propertyName] = enumType[propertyValue];
    }
  }

  serializeArray(
    target: { [key: string]: any },
    propertyName: string,
    propertyValue: any[] | undefined
  ) {
    const items = [];

    if (propertyValue) {
      for (const item of propertyValue) {
        let serializedItem: any = undefined;

        if (item instanceof SerializableObject) {
          serializedItem = item.toJSON(this);
        } else if (item.toJSON) {
          serializedItem = item.toJSON();
        } else {
          serializedItem = item;
        }

        if (serializedItem !== undefined) {
          items.push(serializedItem);
        }
      }
    }

    if (items.length === 0) {
      if (target.hasOwnProperty(propertyName) && Array.isArray(target[propertyName])) {
        delete target[propertyName];
      }
    } else {
      this.serializeValue(target, propertyName, items);
    }
  }

  clearEvents() {
    this._validationEvents = [];
  }

  logEvent(
    source: SerializableObject | undefined,
    phase: Enums.ValidationPhase,
    event: Enums.ValidationEvent,
    message: string
  ) {
    this._validationEvents.push({
      source: source,
      phase: phase,
      event: event,
      message: message
    });
  }

  logParseEvent(
    source: SerializableObject | undefined,
    event: Enums.ValidationEvent,
    message: string
  ) {
    this.logEvent(source, Enums.ValidationPhase.Parse, event, message);
  }

  getEventAt(index: number): IValidationEvent {
    return this._validationEvents[index];
  }

  get eventCount(): number {
    return this._validationEvents.length;
  }
}

export class PropertyDefinition {
  private static _sequentialNumber: number = 0;

  getInternalName(): string {
    return this.name;
  }

  parse(
    sender: SerializableObject,
    source: PropertyBag,
    context: BaseSerializationContext
  ): any {
    return source[this.name];
  }

  toJSON(
    sender: SerializableObject,
    target: PropertyBag,
    value: any,
    context: BaseSerializationContext
  ): void {
    context.serializeValue(target, this.name, value, this.defaultValue);
  }

  readonly sequentialNumber: number;

  isSerializationEnabled: boolean = true;

  constructor(
    readonly targetVersion: Version,
    readonly name: string,
    readonly defaultValue: any,
    readonly onGetInitialValue?: (sender: SerializableObject) => any
  ) {
    this.sequentialNumber = PropertyDefinition._sequentialNumber;

    PropertyDefinition._sequentialNumber++;
  }
}

export class StringProperty extends PropertyDefinition {
  parse(
    sender: SerializableObject, 
    source: PropertyBag, 
    context: BaseSerializationContext
  ): string | undefined {
      const parsedValue = Utils.parseString(source[this.name], this.defaultValue);
      const isUndefined =
        parsedValue === undefined || (parsedValue === "" && this.treatEmptyAsUndefined);

    if (!isUndefined && this.regEx !== undefined) {
      const matches = this.regEx.exec(parsedValue);

      if (!matches) {
        
      }

      return undefined;
    }

    return parsedValue;
  }

  toJSON(
    sender: SerializableObject,
    target: PropertyBag,
    value: string | undefined,
    context: BaseSerializationContext
  ) {
    
  }
}

export class BoolProperty extends PropertyDefinition {
  parse(
    sender: SerializableObject,
    source: PropertyBag,
    context: BaseSerializationContext
  ): boolean | undefined {
    return Utils.parseBool(source[this.name], this.defaultValue);
  }

  toJSON(
    sender: SerializableObject,
    target: object,
    value: boolean | undefined,
    context: BaseSerializationContext
  ) {
    context.serializeBool(target, this.name, value, this.defaultValue);
  }

  constructor(
    readonly targetVersion: Version,
    readonly name: string,
    readonly defaultValue?: boolean,
    readonly onGetInitialValue?: (sender: SerializableObject) => any
  ) {
    super(targetVersion, name, defaultValue, onGetInitialValue);
  }
}

export class SerializableObjectProperty extends PropertyDefinition {
  parse(
    sender: SerializableObject,
    source: PropertyBag,
    context: BaseSerializationContext
  ): SerializableObject | undefined {
    const sourceValue = source[this.name];

    if (sourceValue === undefined) {
      return this.onGetInitialValue ? this.onGetInitialValue(sender) : this.defaultValue;
    }

    const result = new this.objectType();
    result.parse(sourceValue, context);

    return result;
  }
}

export class SerializableObjectSchema {
  private _properties: PropertyDefinition[] = [];

  indexOf(prop: PropertyDefinition): number {
    for (let i = 0; i < this._properties.length; i++) {
      if (this._properties[i] === prop) {
        return i;
      }
    }

    return -1;
  }

  add(...properties: PropertyDefinition[]) {
    for (const prop of properties) {
      if (this.indexOf(prop) === -1) {
        this._properties.push(prop);
      }
    }
  }

  remove(...properties: PropertyDefinition[]) {
    for (const prop of properties) {
      while (true) {
        const index = this.indexOf(prop);

        if (index >= 0) {
          this._properties.splice(index, 1);
        } else {
          break;
        }
      }
    }
  }
}

export function property(prop: PropertyDefinition) {
  return function (target: any, propertyKey: string) {
    const descriptor = Object.getOwnPropertyDescriptor(target, propertyKey) || {};

    if (!descriptor.get && !descriptor.set) {
      descriptor.get = function (this: SerializableObject) {
        return this.getValue(prop);
      }
      descriptor.set = function (this: SerializableObject, value: any) {
        this.setValue(prop, value);
      }

      Object.defineProperty(target, propertyKey, descriptor);
    }
  }
}

export type PropertyBag = { [propertyName: string]: any };

export abstract class SerializableObject {
  static onRegisterCustomProperties?: (
    sender: SerializableObject,
    schema: SerializableObjectSchema
  ) => void;
  static defaultMaxVersion: Version = Versions.latest;

  private static readonly _schemaCache: { [typeName: string]: SerializableObjectSchema} = {};

  private _propertyBag: PropertyBag = {};
  private _rawProperties: PropertyBag = {};

  protected abstract getSchemaKey(): string;

  protected getDefaultSerializationContext(): BaseSerializationContext {
    
  }

  protected populateSchema(schema: SerializableObjectSchema) {
    const ctor = <any>this.constructor;
    const properties: PropertyDefinition[] = [];

    // eslint-disable-next-line guard-for-in
    for (const propertyName in ctor) {
      try {
        const propertyValue = ctor[propertyName];

        if (propertyValue instanceof PropertyDefinition) {
          properties.push(propertyValue);
        }
      } catch {

      }
    }

    if (properties.length > 0) {
      const sortedProperties = properties.sort(
        (p1: PropertyDefinition, p2: PropertyDefinition) => {
          if (p1.sequentialNumber > p2.sequentialNumber) {
            return 1;
          } else if (p1.sequentialNumber < p2.sequentialNumber) {
            return -1;
          }

          return 0;
        }
      );

      schema.add(...sortedProperties);
    }

    if (SerializableObject.onRegisterCustomProperties) {
      SerializableObject.onRegisterCustomProperties(this, schema);
    }
  }

  setCustomProperty(name: string, value: any) {
    const shouldDeleteProperty =
      (typeof value === "string" && !value) || value === undefined || value === null;
    
    if (shouldDeleteProperty) {
      delete this._rawProperties[name];
    } else {
      this._rawProperties[name] = value;
    }
  }

  getCustomProperty(name: string) {
    return this._rawProperties[name];
  }

  getSchema(): SerializableObjectSchema {
    let schema: SerializableObjectSchema = SerializableObject._schemaCache[this.getSchemaKey()];

    if (!schema) {
      schema = new SerializableObjectSchema();

      this.populateSchema(schema);

      SerializableObject._schemaCache[this.getSchemaKey()] = schema;
    }

    return schema;
  }
}