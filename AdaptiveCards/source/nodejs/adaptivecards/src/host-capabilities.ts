import {
  TargetVersion,
  Version,
  SerializableObject,
  BaseSerializationContext,
  PropertyBag
} from "./serialization";

export class HostCapabilities extends SerializableObject {
  private _capabilities: { [key: string]: TargetVersion } = {};

  protected getSchemaKey(): string {
      return "HostCapabilities";
  }

  protected internalParse(source: any, context: BaseSerializationContext) {
    super.internalParse(source, context);

    if (source) {
      //
      for (const name in source) {
        const jsonVersion = source[name];

        if (typeof jsonVersion === "string") {
          if (jsonVersion === "*") {
            this.addCapability(name, "*");
          } else {
            const version = Version.parse(jsonVersion, context);

            if (version?.isValid) {
              this.addCapability(name, version);
            }
          }
        }
      }
    }
  }

  addCapability(name: string, version: TargetVersion) {
    this._capabilities[name] = version;
  }
}