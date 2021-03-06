namespace BlazorFluentUiBaseComponent {
  const test = 12333;

  const DATA_IS_SCROLLABLE_ATTRIBUTE = 'data-is-scrollable';

  const IsFocusVisibleClassName = 'ms-Fabric--isFocusVisible';


  export function initializeFocusRects(): void {
    if (!(<any>window).__hasInitializeFocusRects__) {
      (<any>window).__hasInitializeFocusRects__ = true;
      window.addEventListener("mousedown", _onFocusRectMouseDown, true);
      window.addEventListener("keydown", _onFocusRectKeyDown, true);
    }
  }


  function _onFocusRectMouseDown(ev: MouseEvent) {
    if (window.document.body.classList.contains(IsFocusVisibleClassName)) {
      window.document.body.classList.remove(IsFocusVisibleClassName);
    }
  }
  function _onFocusRectKeyDown(ev: MouseEvent) {
    if (isDirectionalKeyCode(ev.which) && !window.document.body.classList.contains(IsFocusVisibleClassName)) {
      window.document.body.classList.add(IsFocusVisibleClassName);
    }
  }

  const DirectionalKeyCodes: { [key: number]: number } = {
    [KeyCodes.up]: 1,
    [KeyCodes.down]: 1,
    [KeyCodes.left]: 1,
    [KeyCodes.right]: 1,
    [KeyCodes.home]: 1,
    [KeyCodes.end]: 1,
    [KeyCodes.tab]: 1,
    [KeyCodes.pageUp]: 1,
    [KeyCodes.pageDown]: 1
  };

  function isDirectionalKeyCode(which: number): boolean {
    return !!DirectionalKeyCodes[which];
  }


  export function findScrollableParent(startingElement: HTMLElement | null): HTMLElement | null {
    let el: HTMLElement | null = startingElement;

    // First do a quick scan for the scrollable attribute.
    while (el && el !== document.body) {
      if (el.getAttribute(DATA_IS_SCROLLABLE_ATTRIBUTE) === 'true') {
        return el;
      }
      el = el.parentElement;
    }

    // If we haven't found it, then use the slower method: compute styles to evaluate if overflow is set.
    el = startingElement;

    while (el && el !== document.body) {
      if (el.getAttribute(DATA_IS_SCROLLABLE_ATTRIBUTE) !== 'false') {
        const computedStyles = getComputedStyle(el);
        let overflowY = computedStyles ? computedStyles.getPropertyValue('overflow-y') : '';

        if (overflowY && (overflowY === 'scroll' || overflowY === 'auto')) {
          return el;
        }
      }

      el = el.parentElement;
    }

    // Fall back to window scroll.
    if (!el || el === document.body) {
      // tslint:disable-next-line:no-any
      el = window as any;
    }

    return el;
  }


  interface MapSimple<T> {
    [K: string]: T;
  }


  class Guid {
    static newGuid() {
      return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0,
          v = c == 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
      });
    }
  }



/* Focus stuff */

  /* Since elements can be stored in Blazor and we don't want to create more js files, this will hold last focused elements for restoring focus later. */
  var _lastFocus: MapSimple<HTMLElement> = {};

  export function storeLastFocusedElement(): string {
    let element = document.activeElement;
    let htmlElement = <HTMLElement>element;
    if (htmlElement) {
      let guid = Guid.newGuid();
      _lastFocus[guid] = htmlElement;
      return guid;
    }
    return null;
  }

  export function restoreLastFocus(guid: string, restoreFocus: boolean = true) {
    var htmlElement = _lastFocus[guid];
    if (htmlElement != null) {
      if (restoreFocus) {
        htmlElement.focus();
      }
      delete _lastFocus[guid];
    }
  }

  export const enum KeyCodes {
    backspace = 8,
    tab = 9,
    enter = 13,
    shift = 16,
    ctrl = 17,
    alt = 18,
    pauseBreak = 19,
    capslock = 20,
    escape = 27,
    space = 32,
    pageUp = 33,
    pageDown = 34,
    end = 35,
    home = 36,
    left = 37,
    up = 38,
    right = 39,
    down = 40,
    insert = 45,
    del = 46,
    zero = 48,
    one = 49,
    two = 50,
    three = 51,
    four = 52,
    five = 53,
    six = 54,
    seven = 55,
    eight = 56,
    nine = 57,
    a = 65,
    b = 66,
    c = 67,
    d = 68,
    e = 69,
    f = 70,
    g = 71,
    h = 72,
    i = 73,
    j = 74,
    k = 75,
    l = 76,
    m = 77,
    n = 78,
    o = 79,
    p = 80,
    q = 81,
    r = 82,
    s = 83,
    t = 84,
    u = 85,
    v = 86,
    w = 87,
    x = 88,
    y = 89,
    z = 90,
    leftWindow = 91,
    rightWindow = 92,
    select = 93,
    zero_numpad = 96,
    one_numpad = 97,
    two_numpad = 98,
    three_numpad = 99,
    four_numpad = 100,
    five_numpad = 101,
    six_numpad = 102,
    seven_numpad = 103,
    eight_numpad = 104,
    nine_numpad = 105,
    multiply = 106,
    add = 107,
    subtract = 109,
    decimalPoint = 110,
    divide = 111,
    f1 = 112,
    f2 = 113,
    f3 = 114,
    f4 = 115,
    f5 = 116,
    f6 = 117,
    f7 = 118,
    f8 = 119,
    f9 = 120,
    f10 = 121,
    f11 = 122,
    f12 = 123,
    numlock = 144,
    scrollLock = 145,
    semicolon = 186,
    equalSign = 187,
    comma = 188,
    dash = 189,
    period = 190,
    forwardSlash = 191,
    graveAccent = 192,
    openBracket = 219,
    backSlash = 220,
    closeBracket = 221,
    singleQuote = 222
  }
}

//declare global {
interface Window {
  BlazorFluentUiBaseComponent: typeof BlazorFluentUiBaseComponent
}
//}

window.BlazorFluentUiBaseComponent = BlazorFluentUiBaseComponent;