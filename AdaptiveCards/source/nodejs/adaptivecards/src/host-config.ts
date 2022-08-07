import * as Utils from "./utils";
import { HostCapabilities } from "./host-capabilities";

function parseHostConfigEnum(
  targetEnum: { [s: number]: string },
  value: string | number,
  defaultValue: number
): number {
  if (typeof value === "string") {
    const parsedValue = Utils.parseEnum(targetEnum, value, defaultValue);

    return parsedValue !== undefined ? parsedValue : defaultValue;
  } else if (typeof value === "number") {
    return value;
  } else {
    return defaultValue;
  }
}

export class ColorDefinition {
  default: string = "#000000";
  subtle: string = "#666666";

  constructor(defaultColor?: string, subtleColor?: string) {
    if (defaultColor) {
      this.default = defaultColor;
    }

    if (subtleColor) {
      this.subtle = subtleColor;
    }
  }

  parse(obj?: any) {
    if (obj) {
      this.default = obj["default"] || this.default;
      this.subtle = obj["subtle"] || this.subtle;
    }
  }
}

export class TextColorDefinition extends ColorDefinition {
  readonly highlightColors = new ColorDefinition("#22000000", "#11000000");

  parse(obj?: any) {
    super.parse(obj);

    if (obj) {
      this.highlightColors.parse(obj["highlightColors"]);
    }
  }
}

export class AdaptiveCardConfig {
  allowCustomStyle: boolean = false;

  constructor(obj?: any) {
    if (obj) {
      this.allowCustomStyle = obj["allowCustomStyle"] || this.allowCustomStyle;
    }
  }
}



export class ContainerStyleDefinition {
  backgroundColor?: string;

  //readonly foregroundColors: 
}

export interface ILineHeightDefinitions {
  small: number;
  medium: number;
  default: number;
  large: number;
  extraLarge: number;
}

export class ContainerStyleSet {
  //private _allStyles: { [key: string]: C}
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

export class HostConfig {
  readonly hostCapabilities = new HostCapabilities();

  private _legacyFontType: FontTypeDefinition;

  choiceSetInputValueSeparator: string = ",";
  supportsInteractivity: boolean = true;
  //lineHeights?: ILine
}