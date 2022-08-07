import * as Utils from "./utils";
import { Constants } from "./constants";
import { InputControl } from "./inputcontrol";

export class DayCell {
  private _element: HTMLElement;
  private _isSubdued: boolean = false;
  private _isSelected: boolean = false;

  private selected() {
    if (this.onSelected) {
      this.onSelected(this);
    }
  }

  readonly date: Date;

  onSelected: (cell: DayCell) => void;

  constructor(date: Date) {
    this.date = date;
  }

  render(): HTMLElement {
    this._element = document.createElement("div");
    this._element.className = "ms-ctrl ms-ctrl-calenderDay";
    this._element.innerHTML = this.date.getDate().toString();
    this._element.tabIndex = 0;
    this._element.onclick = (e) => { this.selected(); };
    this._element.onkeydown = (e) => {
      if (e.key == Constants.keys.enter) {
        this.selected();
        return false;
      }
    };

    return this._element;
  }

  focus() {
    this._element.focus();
  }

  get isSubdued(): boolean {
    return this._isSubdued;
  }

  set isSubdued(value: boolean) {
    this._isSubdued = value;

    if (this._isSubdued) {
      this._element.classList.add("subdued");
    }
    else {
      this._element.classList.remove("subdued");
    }
  }

  get isSelected(): boolean {
    return this._isSelected;
  }

  set isSelected(value: boolean) {
    this._isSelected = value;

    if (this._isSelected) {
      this._element.classList.add("selected");
    }
    else {
      this._element.classList.remove("selected");
    }
  }
}

export class Calendar extends InputControl {
  private _date: Date;
  private _days: Array<DayCell>;
  private _selectedDay: DayCell = null;
  private _rootContainerElement: HTMLElement;

  private generateDayCells(baseDate: Date) {
    this._days = [];

    var inputMonth = baseDate.getMonth();
    var inputYear = baseDate.getFullYear();

    var start = new Date(inputYear, inputMonth, 1);
    var end = new Date(inputYear, inputMonth, Utils.daysInMonth(inputYear, inputMonth));

    var startDateDaysOfWeek = start.getDay();

    if ((startDateDaysOfWeek - Utils.CalendarSettings.firstDayOfWeek) > 0) {
      start = Utils.addDays(start, Utils.CalendarSettings.firstDayOfWeek - startDateDaysOfWeek);
    }

    var endDateDayOfWeek = end.getDate();
    var lastDayOfWeek = Utils.CalendarSettings.firstDayOfWeek + 6;

    if ((lastDayOfWeek - endDateDayOfWeek) > 0) {
      end = 
    }
  }
}