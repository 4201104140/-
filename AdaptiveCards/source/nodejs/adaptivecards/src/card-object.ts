import * as Enums from "./enums";
import { Dictionary, GlobalSettings } from "./shared";

import {
  Versions,
  SerializableObject,
  StringProperty,
  IValidationEvent
} from "./serialization";

export class ValidationResults {
  readonly allIds: Dictionary<number> = {};
  readonly validationEvents: IValidationEvent[] = [];

  addFailure(cardObject: CardObject, event: Enums.ValidationEvent, message: string) {
    this.validationEvents.push({
      phase: Enums.ValidationPhase.Validation,
      source: cardObject,
      event: event,
      message: message
    });
  }
}

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