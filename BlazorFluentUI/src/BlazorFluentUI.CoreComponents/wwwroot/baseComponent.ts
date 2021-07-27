const IS_FOCUSABLE_ATTRIBUTE = 'data-is-focusable';
const IS_SCROLLABLE_ATTRIBUTE = 'data-is-scrollable';
const IS_VISIBLE_ATTRIBUTE = 'data-is-visible';
const FOCUSZONE_ID_ATTRIBUTE = 'data-focuszone-id';
const FOCUSZONE_SUB_ATTRIBUTE = 'data-is-sub-focuszone';
const IsFocusVisibleClassName = 'ms-Fabric--isFocusVisible';

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