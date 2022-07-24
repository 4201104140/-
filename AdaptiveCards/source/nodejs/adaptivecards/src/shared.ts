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
  static use
}

export interface ISeparationDefinition {
  spacing: number;
  lineThickness?: number;
  lineColor?: string;
}