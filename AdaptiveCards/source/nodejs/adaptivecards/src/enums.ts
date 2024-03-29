/**
 * This should really be a string enum, e.g
 * 
 */
export class ContainerStyle {
  static readonly Default: "default" = "default";
  static readonly Emphasis: "emphasis" = "emphasis";
  static readonly Accent: "accent" = "accent";
}

export class ActionStyle {
  static readonly Default: "default" = "default";
  static readonly Positive: "positive" = "positive";
}

export enum Size {
  Auto,
  Stretch,
  Small,
  Medium,
  Large
}

export enum ImageSize {
  Small,
  Medium,
  Large
}

export enum SizeUnit {
  Weight,
  Pixel
}

export enum TextSize {
  Small,
  Default,
  Medium,
  Large,
  ExtraLarge
}

export enum TextWeight {
  Lighter,
  Default,
  Bolder
}

export enum FontType {
  Default,
  Monospace
}

export enum Spacing {
  None,
  Small,
  Default,
  Medium,
  Large,
  ExtraLarge,
  Padding
}

export enum TextColor {
  Default,
  Dark,
  Light,
  Accent,
  Good,
  Warning,
  Attention
}

export enum HorizontalAlignment {
  Left,
  Center,
  Right
}

export enum Orientation {
  Horizontal,
  Vertical
}

export enum ValidationPhase {
  Parse,
  ToJSON,
  Validation
}

export enum ValidationEvent {
  Hint,
  ActionTypeNotAllowed,
  CollectionCantBeEmpty,
  Deprecated,
  ElementTypeNotAllowed,
  InteractivityNotAllowed,
  InvalidPropertyValue,
  MissingCardType,
  PropertyCantBeNull,
  TooManyActions,
  UnknownActionType,
  UnknownElementType,
  UnsupportedCardVersion,
  DuplicateId,
  UnsupportedProperty,
  RequiredInputsShouldHaveLabel,
  RequiredInputsShouldHaveErrorMessage,
  Other
}

export enum RefreshMode {
  Disabled,
  Manual,
  Automatic
}

export enum LogLevel {
  Info,
  Warning,
  Error
}
