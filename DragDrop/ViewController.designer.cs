// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace DragDrop
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		UIKit.UILongPressGestureRecognizer dragRecognizer { get; set; }

		[Outlet]
		UIKit.UITableView tableView { get; set; }

		[Outlet]
		UIKit.UICollectionView unsortedCollectionView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (tableView != null) {
				tableView.Dispose ();
				tableView = null;
			}

			if (dragRecognizer != null) {
				dragRecognizer.Dispose ();
				dragRecognizer = null;
			}

			if (unsortedCollectionView != null) {
				unsortedCollectionView.Dispose ();
				unsortedCollectionView = null;
			}
		}
	}
}
