using System;
using System.Collections.Generic;
using Cappuccino.App.iOS.Extensions;
using Foundation;
using UIKit;


namespace Cappuccino.App.iOS.UI.Common;

public abstract class TableViewCellBase<TItem> : UITableViewCell {
    public TableViewCellBase(IntPtr handle) : base(handle) { Initialize(); }
    public TableViewCellBase() : base() { Initialize(); }

    protected abstract void Initialize();
    public abstract void Bind(TItem item);
}


public abstract class TableViewAdapterBase<TItem, TCell>: UITableViewDataSource, IUITableViewDelegate where TCell: TableViewCellBase<TItem> {
    protected readonly List<TItem> items = new List<TItem>();
    public int ItemLimit { get; set; } = 0;
    public Action<TItem>? OnItemClicked;
    public Action<TItem>? OnItemLongClicked;
    public Action<int>? OnLastItemBind;

    public override nint RowsInSection(UITableView tableView, nint section) => GetItemCount();
   
    public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath) {
        UITableViewCell view = tableView.DequeueReusableCell(typeof(TCell).Name)!;
        TCell? cell = view as TCell;

        cell?.Bind(this.items[indexPath.Row]);

        if (indexPath.Row == GetItemCount() - 1 && GetItemCount() < ItemLimit)
            this.OnLastItemBind?.Invoke(indexPath.Row);

        return cell!;
    }

    [Export("tableView:heightForRowAtIndexPath:")]
    public nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath) {
        UITableViewCell view = tableView.DequeueReusableCell(typeof(TCell).Name)!;
        view.LayoutSubviews();
        return view.ContentView.Frame.Height;
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