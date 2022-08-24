using System;
using System.Collections.Generic;
using Cappuccino.App.iOS.Extensions;
using Foundation;
using UIKit;

namespace Cappuccino.App.iOS.UI.Common {
    public abstract class TableViewCellBase<TItem> : UITableViewCell {
        public TableViewCellBase(IntPtr handle) : base(handle) { Initialize(); }
        public TableViewCellBase() : base() { Initialize(); }

        protected abstract void Initialize();
        public abstract void Bind(TItem item);
    }

    public abstract class TableViewAdapterBase<TItem, TCell>: UITableViewDataSource, IUITableViewDelegate where TCell: TableViewCellBase<TItem> {
        protected readonly List<List<TItem>> sections = new List<List<TItem>>();
        public int ItemLimit { get; set; } = 0;
        public Action<TItem>? OnItemClicked;
        public Action<TItem>? OnItemLongClicked;
        public Action<int>? OnLastItemBind;

        private string? cellIdentifier;
        private string? headerCellIdentifier;

        public TableViewAdapterBase(int sections) {
            for (int i = 0; i < sections; i++) {
                this.sections.Add(new List<TItem>());
            }
        }


        public override nint RowsInSection(UITableView tableView, nint section) {
            return GetItemCountInSection((int)section);
        }
        public override nint NumberOfSections(UITableView tableView) {
            return this.sections.Count;
        }
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath) {
            UITableViewCell view = tableView.DequeueReusableCell(typeof(TCell).Name)!;
            TCell? cell = view as TCell;

            cell?.Bind(this.sections[indexPath.Section][indexPath.Row]);

            if (indexPath.Section == this.sections.Count)
                if (indexPath.Row == GetItemCountInSection(indexPath.Section) - 1 && GetItemCountInSection(indexPath.Section) < ItemLimit)
                    this.OnLastItemBind?.Invoke(indexPath.Row);

            return cell!;
        }

        [Export("tableView:heightForRowAtIndexPath:")]
        public nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath) {
            UITableViewCell view = tableView.DequeueReusableCell(typeof(TCell).Name)!;
            view.LayoutSubviews();
            return view.ContentView.Frame.Height;
        }


        public int GetItemCountInSection(int section) {
            return this.sections[section].Count;
        }
        public void Add(IEnumerable<TItem> items, int section = 0) {
            this.sections[section].AddRange(items);
        }
        public void Replace(IEnumerable<TItem> items, int section = 0) {
            this.sections[section].Clear();
            this.sections[section].AddRange(items);
        }
        public void ClearAll() {
            for(int i = 0; i < this.sections.Count; i++) {
                this.sections[i].Clear();
            }
        }
        public void Clear(int section = 0) {
            this.sections[section].Clear();
        }
    }
}