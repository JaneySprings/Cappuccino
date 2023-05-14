

namespace Cappuccino.App.iOS.Extensions;


public static class TableViewExtensions {
    public static void ScrollDown(this UITableView tableView, bool animated = true) {
        if (tableView.NumberOfSections() == IntPtr.Zero)
            return;
        
        var lastSectionIndex = tableView.NumberOfSections() - 1;
        if (tableView.NumberOfRowsInSection(lastSectionIndex) == IntPtr.Zero)
            return;

        var lastRowIndex = tableView.NumberOfRowsInSection(lastSectionIndex) - 1;
        var pathToLastRow = NSIndexPath.FromRowSection(lastRowIndex, lastSectionIndex);
        tableView.ScrollToRow(pathToLastRow, UITableViewScrollPosition.Bottom, animated);
    }

    public static void InsertLastRow(this UITableView tableView) {
        if (tableView.NumberOfSections() == IntPtr.Zero)
            return;
        
        var lastSectionIndex = tableView.NumberOfSections() - 1;
        var lastRowIndex = tableView.NumberOfRowsInSection(lastSectionIndex);
        if (lastRowIndex == IntPtr.Zero)
            return;

        var targetRowIndexPath = NSIndexPath.FromRowSection(lastRowIndex, new IntPtr(0));
        tableView.InsertRows(new[] { targetRowIndexPath }, UITableViewRowAnimation.Automatic);
        tableView.ScrollDown();
    }

    public static void ReloadRow(this UITableView tableView, int index) {
        if (tableView.NumberOfSections() == IntPtr.Zero)
            return;
        
        var lastSectionIndex = tableView.NumberOfSections() - 1;
        var targetRowIndexPath = NSIndexPath.FromRowSection(index, new IntPtr(0));
        tableView.ReloadRows(new[] { targetRowIndexPath }, UITableViewRowAnimation.Automatic);
    }

    public static void DeleteRow(this UITableView tableView, int index) {
        if (tableView.NumberOfSections() == IntPtr.Zero)
            return;
        
        var lastSectionIndex = tableView.NumberOfSections() - 1;
        var targetRowIndexPath = NSIndexPath.FromRowSection(index, new IntPtr(0));
        tableView.DeleteRows(new[] { targetRowIndexPath }, UITableViewRowAnimation.Automatic);
    }
}
