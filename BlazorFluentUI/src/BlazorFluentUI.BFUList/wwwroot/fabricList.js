//declare interface Window { debounce(func: Function, wait: number, immediate: boolean): Function }
// /// <reference path="../../BlazorFluentUI.BFUFocusTrapZone/wwwroot/focusTrapZone.ts" />
/// <reference path="../../BlazorFluentUI.BFUBaseComponent/wwwroot/baseComponent.ts" />
var BlazorFluentUiList;
(function (BlazorFluentUiList) {
    //  interface IElementMeasurements {
    //      left: number;
    //      top: number;
    //      width: number;
    //      height: number;
    //      right?: number;
    //      bottom?: number;
    //      cwidth: number;
    //      cheight: number;
    //      test: string;
    //  }
    //type ICancelable<T> = {
    //  flush: () => T;
    //  cancel: () => void;
    //  pending: () => boolean;
    //};
    //export function measureElement(element: HTMLElement): IRectangle {
    //  var rect: IRectangle = {
    //    width: element.clientWidth,
    //    height: element.clientHeight,
    //    left: 0,
    //    top: 0
    //  }
    //  return rect;
    //};
    //export function measureScrollWindow(element: HTMLElement): IRectangle {
    //  var rect: IRectangle = {
    //    width: element.scrollWidth,
    //    height: element.scrollHeight,
    //    top: element.scrollTop,
    //    left: element.scrollLeft,
    //    bottom: element.scrollTop + element.clientHeight,
    //      right: element.scrollLeft + element.clientWidth        
    //  }
    //  return rect;
    //};
    //  export function measureElementRect(element: HTMLElement): IElementMeasurements {
    //      var rect = element.getBoundingClientRect();
    //      var elementMeasurements: IElementMeasurements = { height : rect.height, width: rect.width, left: rect.left, right: rect.right, top: rect.top, bottom: rect.bottom, cheight:element.clientHeight, cwidth:element.clientWidth, test:"Random!"};
    //      return elementMeasurements;
    //  };
    var _lastId = 0;
    var cachedLists = new Map();
    class BFUList {
        constructor(component, spacerBefore, spacerAfter) {
            this.cachedSizes = new Map();
            this.averageHeight = 40;
            this.id = _lastId++;
            this.component = component;
            //this.surfaceElement = rootElement.children.item(0) as HTMLElement;
            this.scrollElement = BlazorFluentUiBaseComponent.findScrollableParent(spacerBefore);
            this.rootElement = spacerBefore.parentElement;
            //this.scrollElement = scrollElement;
            this.spacerBefore = spacerBefore;
            this.spacerAfter = spacerAfter;
            const rootMargin = 50;
            this.intersectionObserver = new IntersectionObserver((entries, observer) => {
                entries.forEach((entry) => {
                    var _a;
                    if (!entry.isIntersecting) {
                        return;
                    }
                    const spacerBeforeRect = this.spacerBefore.getBoundingClientRect();
                    const spacerAfterRect = this.spacerAfter.getBoundingClientRect();
                    const spacerSeparation = spacerAfterRect.top - spacerBeforeRect.bottom;
                    const containerSize = (_a = entry.rootBounds) === null || _a === void 0 ? void 0 : _a.height;
                    if (entry.target === this.spacerBefore) {
                        component.invokeMethodAsync('OnBeforeSpacerVisible', entry.intersectionRect.top - entry.boundingClientRect.top, spacerSeparation, containerSize);
                    }
                    else if (entry.target === this.spacerAfter && this.spacerAfter.offsetHeight > 0) {
                        // When we first start up, both the "before" and "after" spacers will be visible, but it's only relevant to raise a
                        // single event to load the initial data. To avoid raising two events, skip the one for the "after" spacer if we know
                        // it's meaningless to talk about any overlap into it.
                        component.invokeMethodAsync('OnAfterSpacerVisible', entry.boundingClientRect.bottom - entry.intersectionRect.bottom, spacerSeparation, containerSize);
                    }
                });
            }, {
                root: this.scrollElement, rootMargin: `${rootMargin}px`
            });
            this.intersectionObserver.observe(this.spacerBefore);
            this.intersectionObserver.observe(this.spacerAfter);
            // After each render, refresh the info about intersections
            this.mutationObserverBefore = new MutationObserver(() => {
                this.intersectionObserver.unobserve(this.spacerBefore);
                this.intersectionObserver.observe(this.spacerBefore);
            });
            this.mutationObserverBefore.observe(this.spacerBefore, { attributes: true });
            this.mutationObserverAfter = new MutationObserver(() => {
                this.intersectionObserver.unobserve(this.spacerAfter);
                this.intersectionObserver.observe(this.spacerAfter);
            });
            this.mutationObserverAfter.observe(this.spacerAfter, { attributes: true });
        }
        disconnect() {
            this.mutationObserverBefore.disconnect();
            this.mutationObserverAfter.disconnect();
            this.intersectionObserver.unobserve(this.spacerBefore);
            this.intersectionObserver.unobserve(this.spacerAfter);
            this.intersectionObserver.disconnect();
        }
        getAverageHeight() {
            let calculate = false;
            let averageHeight = 0;
            let newItems = {};
            for (let i = 0; i < this.surfaceElement.children.length; i++) {
                let item = this.surfaceElement.children.item(i);
                let index = item.getAttribute("data-item-index");
                if (index != null && !this.cachedSizes.has(index) && this.cachedSizes.get(index) != item.clientHeight) {
                    this.cachedSizes.set(index, item.clientHeight);
                    newItems[index] = item.clientHeight;
                    calculate = true;
                }
            }
            if (calculate) {
                this.component.invokeMethodAsync("UpdateHeightCache", newItems);
                averageHeight = [...this.cachedSizes.values()].reduce((p, c, i, a) => p + c) / this.cachedSizes.size;
            }
            return averageHeight;
        }
        updateItemHeights() {
        }
    }
    function getInitialAverageHeight(id) {
        let list = cachedLists.get(id);
        if (list == null) {
            return 0;
        }
        else {
            return list.getAverageHeight();
        }
    }
    BlazorFluentUiList.getInitialAverageHeight = getInitialAverageHeight;
    function initialize(component, spacerBefore, spacerAfter, reset = false) {
        let list = new BFUList(component, spacerBefore, spacerAfter);
        cachedLists.set(list.id, list);
        return list.id;
    }
    BlazorFluentUiList.initialize = initialize;
    function removeList(id) {
        let list = cachedLists.get(id);
        list.disconnect();
        cachedLists.delete(id);
    }
    BlazorFluentUiList.removeList = removeList;
    function getViewport(scrollElement) {
        const visibleRect = {
            top: 0,
            left: 0,
            width: scrollElement.clientWidth,
            height: scrollElement.clientHeight,
            bottom: scrollElement.scrollHeight,
            right: scrollElement.scrollWidth
        };
        return visibleRect;
    }
    BlazorFluentUiList.getViewport = getViewport;
    function readClientRectWithoutTransform(elem) {
        const rect = elem.getBoundingClientRect();
        const translateY = parseFloat(elem.getAttribute('data-translateY'));
        return {
            top: rect.top - translateY, bottom: rect.bottom - translateY, left: rect.left, right: rect.right, height: rect.height, width: rect.width, x: 0, y: 0, toJSON: null
        };
    }
    function _expandRect(rect, pagesBefore, pagesAfter) {
        const top = rect.top - pagesBefore * rect.height;
        const height = rect.height + (pagesBefore + pagesAfter) * rect.height;
        return {
            top: top,
            bottom: top + height,
            height: height,
            left: rect.left,
            right: rect.right,
            width: rect.width
        };
    }
    function _isContainedWithin(innerRect, outerRect) {
        return (innerRect.top >= outerRect.top &&
            innerRect.left >= outerRect.left &&
            innerRect.bottom <= outerRect.bottom &&
            innerRect.right <= outerRect.right);
    }
    function _mergeRect(targetRect, newRect) {
        targetRect.top = newRect.top < targetRect.top || targetRect.top === -1 ? newRect.top : targetRect.top;
        targetRect.left = newRect.left < targetRect.left || targetRect.left === -1 ? newRect.left : targetRect.left;
        targetRect.bottom = newRect.bottom > targetRect.bottom || targetRect.bottom === -1 ? newRect.bottom : targetRect.bottom;
        targetRect.right = newRect.right > targetRect.right || targetRect.right === -1 ? newRect.right : targetRect.right;
        targetRect.width = targetRect.right - targetRect.left + 1;
        targetRect.height = targetRect.bottom - targetRect.top + 1;
        return targetRect;
    }
})(BlazorFluentUiList || (BlazorFluentUiList = {}));
window['BlazorFluentUiList'] = BlazorFluentUiList || {};
//# sourceMappingURL=fabricList.js.map