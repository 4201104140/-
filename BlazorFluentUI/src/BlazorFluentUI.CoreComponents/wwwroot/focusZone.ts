import * as FluentUIBaseComponent from './baseComponent.js'

type DotNetReferenceType = FluentUIBaseComponent.DotNetReferenceType;

interface Set<T> {
    add(value: T): Set<T>;
    clear(): void;
    delete(value: T): boolean;
}

interface IFocusZoneProps {
    allowFocusRoot: boolean,
    checkForNoWrap: boolean,
    defaultActiveElement: HTMLElement,
    direction: FocusZoneDirection,
    disabled: boolean,
    doNotAllowFocusEventToPropagate: boolean,
    handleTabKey: FocusZoneTabbableElements,
    id: string,
    isCircularNavigation: boolean,
    innerZoneKeystrokeTriggers: FluentUIBaseComponent.KeyCodes[],
    onBeforeFocusExists: boolean,
    //root: HTMLElement,
    shouldInputLoseFocusOnArrowKeyExists: boolean
}

enum FocusZoneDirection {
    /** Only react to up/down arrows. */
    vertical = 0,
    /** Only react to left/right arrows. */
    horizontal = 1,
    /** React to all arrows. */
    bidirectional = 2,
    /**
     * React to all arrows. Navigate next item in DOM on right/down arrow keys and previous - left/up arrow keys.
     * Right and Left arrow keys are swapped in RTL mode.
     */
    domOrder = 3
}
enum FocusZoneTabbableElements {
    /** Tabbing is not allowed */
    none = 0,
    /** All tabbing action is allowed */
    all = 1,
    /** Tabbing is allowed only on input elements */
    inputOnly = 2
}

const focusZones = new Map<number, FocusZone>();

export function unregisterFocusZone(dotNet: DotNetReferenceType) {
    let focusZone = focusZones.get(dotNet._id);
    if (typeof focusZone !== 'undefined' && focusZone !== null) {
        focusZone.
    }
}

class FocusZone {
    dotNet: DotNetReferenceType;
    root: HTMLElement;
    props: IFocusZoneProps;

    private _disposables: Function[] = [];

    /** The most recently focused child element. */
    private _activeElement: HTMLElement | null;

    /**
     * The index path to the last focused child element.
     */
    private _lastIndexPath: number[] | undefined;

    /**
    * Flag to define when we've intentionally parked focus on the root element to temporarily
    * hold focus until items appear within the zone.
    */
    private _isParked: boolean;

    /** The child element with tabindex=0. */
    private _defaultFocusElement: HTMLElement | null;
    private _focusAligment: FluentUIBaseComponent.IPoint;

    public unRegister(): void {
        if (!this._is)
    }
}