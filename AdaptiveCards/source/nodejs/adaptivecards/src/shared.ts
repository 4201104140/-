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

export interface ISeparationDefinition {
  spacing: number;
  lineThickness?: number;
  lineColor?: string;
}