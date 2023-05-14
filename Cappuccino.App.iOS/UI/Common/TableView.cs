namespace Cappuccino.App.iOS.UI.Common;


public abstract class TableViewCellBase<TItem> : UITableViewCell {
    protected TableViewCellBase(IntPtr handle) : base(handle) { Initialize(); }
    protected TableViewCellBase() : base() { Initialize(); }

    protected abstract void Initialize();
    public abstract void Bind(TItem item);
}


public abstract class TableViewAdapterBase<TItem, TCell>: UITableViewDataSource, IUITableViewDelegate where TCell: TableViewCellBase<TItem> {
    public List<TItem> Items { get; private set; } = new List<TItem>();

    public int ItemCount => Items.Count;
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
        
        cell!.Bind(Items[indexPath.Row]);

        if (indexPath.Row == ItemCount - 1 && ItemCount < ItemLimit)
            this.LastItemBind?.Invoke(ItemCount);

        return cell!;
    }

    [Export("tableView:didSelectRowAtIndexPath:")]
    public void DidSelectRow(UITableView tableView, NSIndexPath indexPath) {
        tableView.DeselectRow(indexPath, true);

        if (ItemCount <= indexPath.Row) 
            return;

        ItemClicked?.Invoke(Items[indexPath.Row]);
    }
}