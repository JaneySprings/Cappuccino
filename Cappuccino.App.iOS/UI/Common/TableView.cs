namespace Cappuccino.App.iOS.UI.Common;


public abstract class TableViewCellBase<TItem> : UITableViewCell {
    public TableViewCellBase(IntPtr handle) : base(handle) { Initialize(); }
    public TableViewCellBase() : base() { Initialize(); }

    protected abstract void Initialize();
    public abstract void Bind(TItem item);
}


public abstract class TableViewAdapterBase<TItem, TCell>: UITableViewDataSource, IUITableViewDelegate where TCell: TableViewCellBase<TItem> {
    private readonly List<TItem> items = new List<TItem>();

    public int ItemLimit { get; set; }
    public Action<TItem>? OnItemClicked { get; set; }
    public Action<TItem>? OnItemLongClicked { get; set; }
    public Action<int>? OnLastItemBind { get; set; }

    public override nint RowsInSection(UITableView tableView, nint section) => GetItemCount();
   
    public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath) {
        UITableViewCell view = tableView.DequeueReusableCell(typeof(TCell).Name)!;
        TCell? cell = view as TCell;

        cell?.Bind(this.items[indexPath.Row]);

        if (indexPath.Row == GetItemCount() - 1 && GetItemCount() < ItemLimit)
            this.OnLastItemBind?.Invoke(indexPath.Row);

        return cell!;
    }

    public int GetItemCount() => this.items.Count;
    public void AddItems(IEnumerable<TItem> items) {
        this.items.AddRange(items);
    }
    public void ReplaceItems(IEnumerable<TItem> items) {
        this.items.Clear();
        this.items.AddRange(items);
    }
    public void ClearItems() {
        this.items.Clear();
    }
}