import * as Enums from "./enums";

import {
  Versions,
  SerializableObject,
  StringProperty
} from "./serialization";

export abstract class CardObject extends SerializableObject {
  //#region Schema

  static readonly typeNameProperty = new StringProperty(
    Versions.v1_0,
    "type",
    undefined,
    undefined,
    undefined,
    (sender: object) => {
      return (<CardObject>sender).getJsonTypeName();
    }
  );
  static readonly idProperty = new StringProperty(Versions.v1_0, "id");
  static readonly requiresProperty = new SerializableObject
}