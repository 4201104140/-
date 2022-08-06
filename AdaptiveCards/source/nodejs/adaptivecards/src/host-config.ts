import { HostCapabilities } from "./host-capabilities";

export class HostConfig {
  readonly hostCapabilities = new HostCapabilities();

  
}

export interface IFontSizeDefinitions {
  small: number;
  default: number;
  medium: number;
  large: number;
  extraLarge: number;
}

export interface IFontWeightDefinitions {
  lighter: number;
  default: number;
  bolder: number;
}

export class FontTypeDefinition {
  static readonly monospace = new FontTypeDefinition("'Courier New', Courier, monospace");

  fontFamily?: string = "Segoe UI,Segoe,Segoe WP,Helvetica Neue,Helvetica,sans-serif";

  fontSizes: IFontSizeDefinitions = {
    small: 12,
    default: 14,
    medium: 17,
    large: 21,
    extraLarge: 26
  };

  fontWeights: IFontWeightDefinitions = {
    lighter: 200,
    default: 400,
    bolder: 600
  };

  constructor(fontFamily?: string) {
    if (fontFamily) {
      this.fontFamily = fontFamily;
    }
  }

  parse(obj?: any) {
    this.fontFamily = obj["fontFamily"] || this.fontFamily;
    this.fontSizes = {
      small: (obj.fontSizes && obj.fontSizes["small"]) || this.fontSizes.small,
      default: (obj.fontSizes && obj.fontSizes["default"]) || this.fontSizes.default,
      medium: (obj.fontSizes && obj.fontSizes["medium"]) || this.fontSizes.medium,
      large: (obj.fontSizes && obj.fontSizes["large"]) || this.fontSizes.large,
      extraLarge: (obj.fontSizes && obj.fontSizes["extraLarge"]) || this.fontSizes.extraLarge
    };
    this.fontWeights = {
      lighter: (obj.fontWeights && obj.fontWeights["lighter"]) || this.fontWeights.lighter,
      default: (obj.fontWeights && obj.fontWeights["default"]) || this.fontWeights.default,
      bolder: (obj.fontWeights && obj.fontWeights["bolder"]) || this.fontWeights.bolder
    };
  }
}