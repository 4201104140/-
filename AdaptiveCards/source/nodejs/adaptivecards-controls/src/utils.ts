import * as Enums from "./enums";

export class CalendarSettings {
  static monthsInYear = 12;
  static daysInWeek = 7;
  static firstDayOfWeek = Enums.DayOfWeek.Sunday;
  static longDayNames = [ "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" ];
  static shortDayNames = [ "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" ];
  static minimalDayNames = [ "Su", "Mo", "Tu", "We", "Th", "Fr", "Sa" ];
  static longMonthNames = [ "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" ];
  static shortMonthNames = [ "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" ];

  static getLongDayName(dayIndex: number) {
    if (dayIndex >= 0 && dayIndex < CalendarSettings.longDayNames.length) {
      return CalendarSettings.longDayNames[dayIndex];
    }

    throw new Error("Day index out of range: " + dayIndex);
  }

  static getShortDayName(dayIndex: number) {
    if (dayIndex >= 0 && dayIndex < CalendarSettings.shortDayNames.length) {
      return CalendarSettings.shortDayNames[dayIndex];
    }

    throw new Error("Day index out of range: " + dayIndex);
  }

  static getInitialDayName(dayIndex: number) {
    if (dayIndex >= 0 && dayIndex < CalendarSettings.minimalDayNames.length) {
      return CalendarSettings.minimalDayNames[dayIndex];
    }

    throw new Error("Day index out of range: " + dayIndex);
  }

  static getLongMonthName(monthIndex: number) {
    if (monthIndex >= 0 && monthIndex < CalendarSettings.longMonthNames.length) {
      return CalendarSettings.longMonthNames[monthIndex];
    }

    throw new Error("Month index out of range: " + monthIndex);
  }

  static getShortMonthName(monthIndex: number) {
    if (monthIndex >= 0 && monthIndex < CalendarSettings.shortMonthNames.length) {
      return CalendarSettings.shortMonthNames[monthIndex];
    }

    throw new Error("Month index out of range: " + monthIndex);
  }
}

export function daysInMonth(year: number, month: number) {
  switch (month) {
    case 1:
      return (year % 4 == 0 && year % 100) || year % 400 == 0 ? 29 : 28;
    case 3:
    case 5:
    case 8:
    case 10:
      return 30;
    default:
      return 31
  }
}

export function addDays(date: Date, days: number): Date {
  var result = new Date(date.getTime());

  result.setDate(result.getDate() + days);

  return result;
}