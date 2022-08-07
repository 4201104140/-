// import {
//   HostConfig,
//   defaultHostConfig,
//   BaseTextDefinition,
//   FontTypeDefinition,
//   ColorSetDefinition,
//   TextColorDefinition,
//   ContainerStyleDefinition,
//   TextStyleDefinition
// } from "./host-config";

import { CardObject } from './card-object';
import * as Enums from './enums';
// import {
//   ISeparationDefinition,
// } from './shared';

import {
  Version,
  Versions,
  property,
  StringProperty,
  BoolProperty,
  ValueSetProperty,
  EnumProperty,
  PropertyDefinition,
  SerializableObject,
  BaseSerializationContext,
  PropertyBag,
  SerializableObjectCollectionProperty,
  SerializableObjectSchema,
} from "./serialization";

// export function renderSeparation(
//   hostConfig: HostConfig,
//   separationDefinition: ISeparationDefinition,
//   orientation: Enums.Orientation
// ): HTMLElement | undefined {
//   if (
//     separationDefinition.spacing > 0 ||
//     (separationDefinition.lineThickness && separationDefinition.lineThickness > 0)
//   ) {
//     const separator = document.createElement("div");
//     separator.className = hostConfig.makeCssClassName(
//       "ac-" +
//         (orientation === Enums.Orientation.Horizontal ? "horizontal" : "vertical") +
//         "-separator"
//     );
//     separator.setAttribute("aria-hidden", "true");

//     const color = separationDefinition.lineColor
//       ? Utils.stringToCssColor(separationDefinition.lineColor)
//       : "";

//     if (orientation === Enums.Orientation.Horizontal) {
//       if (separationDefinition.lineThickness) {
//         separator.style.paddingTop = separationDefinition.spacing / 2 + "px";
//         separator.style.marginBottom = separationDefinition.spacing /2 + "px";
//         separator.style.borderBottom =
//           separationDefinition.lineThickness + "px solid " + color; 
//       } else {
//         separator.style.height = separationDefinition.spacing + "px";
//       }
//     } else {

//     }

//     return separator;
//   } else {
//     return undefined;
//   }
// }

// export type CardElementHeight = "auto" | "stretch";

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
  static readonly horizontalAlignmentProperty = new EnumProperty(
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

// export class ActionProperty extends PropertyDefinition {
//   parse(
//     sender: SerializableObject, 
//     source: PropertyBag, 
//     context: BaseSerializationContext
//   ): Action | undefined {
//     const parent = <CardElement>sender;

//     return context.parseAction(
//       parent,
//       source[this.name],
//       this.forbiddenActionTypes,
//       parent.isDeignMode()
//     );
//   }

//   toJSON(
//     sender: SerializableObject, 
//     target: PropertyBag, 
//     value: any, 
//     context: BaseSerializationContext
//   ) {
//     context.serializeValue(
//       target,
//       this.name,
//       value ? value.toJSON(context) : undefined,
//       undefined,
//       true
//     );
//   }

//   constructor(
//     readonly targetVersion: Version,
//     readonly name: string,
//     readonly forbiddenActionTypes: string[] = []
//   ) {
//     super(targetVersion, name, undefined);
//   }
// }

// export abstract class BaseTextBlock extends CardElement {
//   //#region Schema

//   static readonly textProperty = new StringProperty(Versions.v1_0, "text", true);
//   static readonly sizeProperty = new EnumProperty(Versions.v1_0, "size", Enums.TextSize);
//   static readonly weightProperty = new EnumProperty(Versions.v1_0, "weight", Enums.TextWeight);
//   static readonly colorProperty = new EnumProperty(Versions.v1_0, "color", Enums.TextColor);
//   static readonly isSubtleProperty = new BoolProperty(Versions.v1_0, "isSubtle");
//   static readonly fontTypeProperty = new EnumProperty(Versions.v1_2, "fontType", Enums.FontType);
//   static readonly selectActionProperty = new ActionProperty(Versions.v1_1, "selectAction", [
//     "Action.ShowCard"
//   ]);

//   protected populateSchema(schema: SerializableObjectSchema) {
//       super.populateSchema(schema);

//       schema.remove(BaseTextBlock.selectActionProperty);
//   }

//   //#endregion

//   size?: Enums.TextSize;

//   weight?: Enums.TextWeight;

//   color?: Enums.TextColor;

//   fontType?: Enums.FontType;

//   isSubtle?: boolean;

//   get text(): string | undefined {
//     return this.getValue(BaseTextBlock.textProperty);
//   }

//   protected getFontSize(fontType: FontTypeDefinition): number {
//     switch (this.effectiveSize) {
//       case Enums.TextSize.Small:
//         return fontType.fontSizes.small;
//       case Enums.TextSize.Medium:
//         return fontType.fontSizes.medium;
//       case Enums.TextSize.Large:
//         return fontType.fontSizes.large;
//       case Enums.TextSize.ExtraLarge:
//         return fontType.fontSizes.extraLarge;
//       default:
//         return fontType.fontSizes.default;
//     }
//   }

//   protected getColorDefinition(
//     colorSet: ColorSetDefinition,
//     color: Enums.TextColor
//   ): TextColorDefinition {
//     switch (color) {
//       case Enums.TextColor.Accent:
//         return colorSet.accent;
//       case Enums.TextColor.Dark:
//         return colorSet.dark;
//       case Enums.TextColor.Light:
//         return colorSet.light;
//       case Enums.TextColor.Good:
//         return colorSet.good;
//       case Enums.TextColor.Warning:
//         return colorSet.warning;
//       case Enums.TextColor.Attention:
//         return colorSet.attention;
//       default:
//         return colorSet.default;
//     }
//   }

//   protected setText(value: string | undefined) {
//     this.setValue(BaseTextBlock.textProperty, value);
//   }

//   ariaHidden: boolean = false;

//   constructor(text?: string) {
//     super();

//     if (text) {
//       this.text = text;
//     }
//   }

//   init(textDefinition: BaseTextDefinition) {
//     this.size = textDefinition.size;
//     this.weight = textDefinition.weight;
//     this.color = textDefinition.color;
//     this.isSubtle = textDefinition.isSubtle;
//   }

//   asString(): string | undefined {
//     return this.text;
//   }

//   applyStylesTo(targetElement: HTMLElement) {
//     const fontType = this.hostConfig.getFontTypeDefinition(this.effectiveFontType);


//   }

//   get effectiveSize(): Enums.TextSize {
//     return this.size !== undefined ? this.size : this.getEffectiveTextStyleDefinition().size;
//   }

//   get effectiveWeight(): Enums.TextWeight {
//     return this.weight !== undefined
//         ? this.weight
//         : this.getEffectiveTextStyleDefinition().weight;
//   }
// }

// export type TextBlockStyle = "default" | "heading" | "columnHeader";

// export class TextBlock extends BaseTextBlock {
  
// }

// export class TextRun extends BaseTextBlock {

// }

// export class RichTextBlock extends CardElement {

// }

// export class Fact extends SerializableObject {

// }

// export class FactSet extends CardElement {

// }

// class ImageDimensionProperty extends PropertyDefinition {

// }

// export class Image extends CardElement {

// }

// export abstract class CardElementContainer extends CardElement {

// }

// export class ImageSet extends CardElementContainer {

// }



// export abstract class Action {

// }

// export abstract class SubmitActionBase extends Action {
  
// }

// export class ExecuteAction extends SubmitActionBase {
//   // Note the "weird" way this field is declared is to work around a breaking
//   // change introduced is TS 3.1 wrt d.ts generation. DO NOT CHANGE
//   static readonly JsonTypeName: "Action.Execute" = "Action.Execute";

//   //#region  Schema

//   static readonly verbProperty = new StringProperty(Versions.v1_4, "verb");

//   @property(ExecuteAction.verbProperty)
//   verb: string;
//   //#endregion
// }

// export class AuthCardButton extends SerializableObject {
//   //#region Schema

//   static readonly typeProperty = new StringProperty(Versions.v1_4, "type");
//   static readonly titleProperty = new StringProperty(Versions.v1_4, "title");
//   static readonly imageProperty = new StringProperty(Versions.v1_4, "image");
//   static readonly valueProperty = new StringProperty(Versions.v1_4, "value");

//   protected getSchemaKey(): string {
//     return "AuthCardButton";
//   }

//   //#endregion

//   type: string;

//   title?: string;

//   image?: string;

//   value?: string;
// }

// export class TokenExchangeResource extends SerializableObject {
//   // #region Schema

//   static readonly idProperty = new StringProperty(Versions.v1_4, "id");
//   static readonly uriProperty = new StringProperty(Versions.v1_4, "uri");
//   static readonly providerIdProperty = new StringProperty(Versions.v1_4, "providerId");

//   protected getSchemaKey(): string {
//       return "TokenExchangeResource";
//   }

//   //#endregion

//   id?: string;

//   uri?: string;

//   providerId?: string;
// }

// export class Authentication extends SerializableObject {
//   //#region Schema

//   static readonly textProperty = new StringProperty(Versions.v1_4, "text")
//   static readonly connectionNameProperty = new StringProperty(Versions.v1_4, "connectionName");
//   static readonly buttonsProperty = new SerializableObjectCollectionProperty(
//     Versions.v1_4,
//     "buttons",
//     AuthCardButton
//   );
//   static readonly tokenExchangeResourceProperty = new SerializableObjectProperty(
//     Versions.v1_4,
//     "tokenExchangeResource",
//     TokenExchangeResource,
//     true
//   );

//   protected getSchemaKey(): string {
//       return "Authentication";
//   }

//   //#endregion

//   text?: string;

//   connectionName?: string;

//   buttons: AuthCardButton[];

//   tokenExchangeResource?: TokenExchangeResource;
// }

// export class SerializationContext extends BaseSerializationContext {
//   private _elemey
// }