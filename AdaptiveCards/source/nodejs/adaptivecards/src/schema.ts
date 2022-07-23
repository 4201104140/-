export type Size = "auto" | "stretch" | "small" | "medium" | "large";
export type HorizontalAlignment = "left" | "center" | "right";
export type Spacing = "none" | "small" | "default" | "medium" | "large" | "extraLarge" | "padding";
export type ActionStyle = "default" | "positive" | "destructive";

export interface IAction {
  id: string;
  title?: string;
  style?: ActionStyle;
}

export interface ISubmitAction extends IAction {
  type: "Action.Submit";
  data: any;
}

export interface IOpenUrlAction extends IAction {
  type: "Action.OpenUrl";
  url: string;
}

export interface IShowCardAction extends IAction {
  type: "Action.ShowCard";
  card: 
}

export interface ICardElement {
  id?: string;
  speak?: string;
  horizontalAlignment?: HorizontalAlignment;
  spacing?: Spacing;
  separator?: boolean;
  height?: "auto" | "stretch";
  [propName: string]: any;
}