using System;
using UIKit;
using CoreGraphics;

namespace DragDrop
{
	public class TableViewDelegate: UITableViewDelegate
	{
		public TableViewDelegate()
		{
		}

		public override UIView GetViewForHeader(UITableView tableView, nint section)
		{
			CGRect frame = new CGRect(new CGPoint(0, 0), new CGSize(tableView.Frame.Width, 20));
			UIView view = new UIView(frame);
			view.BackgroundColor = UIColor.Blue;
			return view;
		}

		public override UIView GetViewForFooter(UITableView tableView, nint section)
		{
			CGRect frame = new CGRect(new CGPoint(0, 0), new CGSize(tableView.Frame.Width, 20));
			UIView view = new UIView(frame);
			view.BackgroundColor = UIColor.Orange;
			return view;
		}

		public override nfloat GetHeightForHeader(UITableView tableView, nint section)
		{
			return 20;
		}

		public override nfloat GetHeightForFooter(UITableView tableView, nint section)
		{
			return 20;
		}
	}
}

