using System;
using System.Collections.Generic;
using Foundation;
using UIKit;
using CoreGraphics;

namespace DragDrop
{
    public partial class ViewController : UIViewController, IUIGestureRecognizerDelegate
	{
        NSIndexPath tappedCellPath;
        UIView draggableView;

		public ViewController (IntPtr handle) : base (handle)
		{
			
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.

			List<ModelItem> unsortedItems = new List<ModelItem>();

			for (int i = 0; i < 30; i++) 
			{
				ModelItem item = new ModelItem(i);
				unsortedItems.Add(item);
			}

			CollectionViewDataSource collectionSource = new CollectionViewDataSource(unsortedItems);
			collectionSource.boundCollectionView = unsortedCollectionView;
			this.unsortedCollectionView.DataSource = /*(UICollectionViewDataSource)*/collectionSource;

			TableVIewDataSource tableSource = new TableVIewDataSource(3);
			this.tableView.DataSource = /*(UITableViewDataSource)*/tableSource;

            ObjCRuntime.Selector sel = new ObjCRuntime.Selector("HandleLongPress:");
            this.dragRecognizer.AddTarget(this, sel);

            unsortedCollectionView.AddGestureRecognizer(this.dragRecognizer);
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

        [Export("HandleLongPress:")]
        public void HandleLongPress(UILongPressGestureRecognizer sender)
        {
			CGPoint tapPointInCollectionView = sender.LocationInView(sender.View);
			CGPoint tapPointOnScreen = sender.LocationInView(this.View);
            switch (sender.State)
            {
				case UIGestureRecognizerState.Began:
				{
					Console.WriteLine("Gesture Began");

					tappedCellPath = unsortedCollectionView.IndexPathForItemAtPoint(tapPointInCollectionView);
					if (tappedCellPath == null)
					{
						sender.Enabled = false;
						sender.Enabled = true;
						Console.WriteLine("Tap location does not point to any cell");
						return;
					}
					else
					{
						CollectionCell tappedCell = (CollectionCell)unsortedCollectionView.CellForItem(tappedCellPath);
						tappedCell.Alpha = 0.25f;
						NSData cellViewMirrorData = NSKeyedArchiver.ArchivedDataWithRootObject(tappedCell);
						this.draggableView = (UIView)NSKeyedUnarchiver.UnarchiveObject(cellViewMirrorData);
						this.draggableView.Center = tapPointOnScreen;
						this.draggableView.Alpha = 0.5f;
						this.draggableView.Hidden = false;
						this.View.AddSubview(this.draggableView);
					}
				}
                break;
				case UIGestureRecognizerState.Changed:
				{
					Console.WriteLine("Gesture Moved");

					this.draggableView.Center = tapPointOnScreen;
				}
                break;
				case UIGestureRecognizerState.Ended:
				{
					Console.WriteLine("Gesture Ended");

					CollectionCell tappedCell = (CollectionCell)unsortedCollectionView.CellForItem(tappedCellPath);
					tappedCell.Alpha = 1.0f;

//					ModelItem item = ((CollectionViewDataSource)this.unsortedCollectionView.DataSource).items[(int)tappedCellPath.Item];
//					((CollectionViewDataSource)this.unsortedCollectionView.DataSource).items.Remove(item);
//					this.unsortedCollectionView.DeleteItems(new NSIndexPath[] { tappedCellPath });

					this.draggableView.RemoveFromSuperview();

					this.draggableView = null;
					this.tappedCellPath = null;
				}
                break;
            }
        }
	}
}

