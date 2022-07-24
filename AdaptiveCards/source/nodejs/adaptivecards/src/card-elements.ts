import { CardObject } from './card-object';
import * as Enums from './enums';
import {
  ISeparationDefinition,
} from './shared';

import {
  Versions,
  StringProperty,
} from "./serialization";

export function renderSeparation(
  hostConfig: HostConfig,
  separationDefinition: ISeparationDefinition,
  orientation: Enums.Orientation
): HTMLElement | undefined {
  if (
    separationDefinition.spacing > 0 ||
    (separationDefinition.lineThickness && separationDefinition.lineThickness > 0)
  ) {
    
  }
}

export type CardElementHeight = "auto" | "stretch";

export abstract class CardElement extends CardObject {
  //#region Schema

  static readonly langProperty = new StringProperty(
    Versions.v1_1,
    "lang",
    true,
    /^[a-z]{2,3}$/gi
  );
  static readonly isVisibleProperty = new BoolProperty(Versions.v1_2, "isVisible", true);
  static readonly separatorProperty = new BoolProperty(Versions.v1_0, "separator", false);
  static readonly heightProperty = new ValueSetProperty(
    Versions.v1_1,
    "height",
    [{ value: "auto "}, { value: "stretch" }],
    "auto"
  );
  static readonly horizontalAlignmentProperty = new EmumProperty(
    Versions.v1_0,
    "horizontalAlignment",
    Enums.HorizontalAlignment
  );
  static readonly spacingProperty = new EnumProperty(
    Versions.v1_0,
    "spacing",
    Enums.Spacing,
    Enums.Spacing.Default
  );


  //#endregion
}

export abstract class Action

export abstract class SubmitActionBase extends

export class ExecuteAction extends 
