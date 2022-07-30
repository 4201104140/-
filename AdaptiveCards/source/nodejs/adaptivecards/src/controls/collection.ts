export class Collection<TItem> {
  private _items: Array<TItem> = [];

  onItemAdded: (item: TItem) => void;
}