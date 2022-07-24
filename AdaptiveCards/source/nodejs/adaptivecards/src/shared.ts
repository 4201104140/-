import * as Enums from "./enums";

export type Refresh = {
  mode: Enums.RefreshMode;
  timeBetweenAutomaticRefreshes: number;
  maximumConsecutiveAutomaticRefreshes: number;
}

export type AppletsSettings = {
  logEnabled: boolean;
  logLevel: boolean;
  maximumRetryAttempts: number;
  defaultTimeBetweenRetryAttempts: number;
  authPromptWidth: number;
  authPromptHeight: number;
  readonly refresh: Refresh;
  onLogEvent?: (level: Enums.L)
}

// eslint-disable-next-line @typescript-eslint/no-extraneous-class
export class GlobalSettings {
  static useAdvancedTextBlockTruncation: boolean = true;
  static useAdvancedCardBottomTruncation: boolean = false;
  static useMarkdownInRadioButtonAndCheckbox: boolean = true;
  static allowMarkForTextHighlighting: boolean = false;
  static alwaysBleedSeparators: boolean = false;
  static enableFullJsonRoundTrip: boolean = false;
}

export interface ISeparationDefinition {
  spacing: number;
  lineThickness?: number;
  lineColor?: string;
}

export type Dictionary<T> = { [key: string]: T };

export class SizeAndUnit {
  physicalSize: number;
  unit: Enums.SizeUnit;

  static parse(input: string, requireUnitSpecifier: boolean = false): SizeAndUnit {
    const result = new SizeAndUnit(0, Enums.SizeUnit.Weight);

    if (typeof input === "number") {
      result.physicalSize = input;

      return result;
    } else if (typeof input === "string") {
      const regExp = /^([0-9]+)(px|\*)?$/g;
      const matches = regExp.exec(input);
      const expectedMatchCount = requireUnitSpecifier ? 3 : 2;

      if (matches && matches.length >= expectedMatchCount) {
        result.physicalSize = parseInt(matches[1]);

        if (matches.length === 3) {
          if (matches[2] === "px") {
            result.unit = Enums.SizeUnit.Pixel;
          }
        }

        return result;
      }
    }

    throw new Error("Invalid size: " + input);
  }

  constructor(physicalSize: number, unit: Enums.SizeUnit) {
    this.physicalSize = physicalSize;
    this.unit = unit;
  }
}