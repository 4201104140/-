
export abstract class HostContainer {
  private _cardHost: HTMLElement;

  readonly name: string;
  readonly styleSheet: string;

  constructor(name: string, styleSheet: string) {
    this.name = name;
    this.styleSheet = styleSheet;
    
    this._cardHost = document.createElement("div");
    this._cardHost.className = "cardHost";
  }
}