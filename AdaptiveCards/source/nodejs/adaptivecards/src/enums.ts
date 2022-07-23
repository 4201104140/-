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