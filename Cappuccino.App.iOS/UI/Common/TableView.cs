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
    public Action<TItem>? ItemClicked { get; set; }
    public Action<TItem>? ItemLongClicked { get; set; }
    public Action<int>? LastItemBind { get; set; }

    public override nint RowsInSection(UITableView tableView, nint section) => ItemCount;

   
    public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath) {
        UITableViewCell view = tableView.DequeueReusableCell(typeof(TCell).Name)!;
        TCell? cell = view as TCell;

        if (ItemCount <= indexPath.Row) 
            return cell!;
        
        cell!.Bind(this.items[indexPath.Row]);

        if (indexPath.Row == ItemCount - 1 && ItemCount < ItemLimit)
            this.LastItemBind?.Invoke(ItemCount);

        return cell!;
    }

    [Export("tableView:didSelectRowAtIndexPath:")]
    public void DidSelectRow(UITableView tableView, NSIndexPath indexPath) {
        tableView.DeselectRow(indexPath, true);

        if (ItemCount <= indexPath.Row) 
            return;

        ItemClicked?.Invoke(items[indexPath.Row]);
    }

    public int ItemCount => this.items.Count;
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