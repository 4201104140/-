const IS_FOCUSABLE_ATTRIBUTE = 'data-is-focusable';
const IS_SCROLLABLE_ATTRIBUTE = 'data-is-scrollable';
const IS_VISIBLE_ATTRIBUTE = 'data-is-visible';
const FOCUSZONE_ID_ATTRIBUTE = 'data-focuszone-id';
const FOCUSZONE_SUB_ATTRIBUTE = 'data-is-sub-focuszone';
const IsFocusVisibleClassName = 'ms-Fabric--isFocusVisible';


export interface DotNetReferenceType {

    invokeMethod<T>(methodIdentifier: string, ...args: any[]): T;
    invokeMethodAsync<T>(methodIdentifier: string, ...args: any[]): Promise<T>;
    _id: number;
}

export interface IRectangle {
    left: number;
    top: number;
    width: number;
    height: number;
    right?: number;
    bottom?: number;
}

export function getWindowRect(): IRectangle {
    var rect: IRectangle = {
        width: window.innerWidth,// - scrollbarwidth
        height: window.innerHeight,
        top: 0,
        left: 0
    }
    return rect;
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
}

export interface IPoint {
    x: number;
    y: number;
}