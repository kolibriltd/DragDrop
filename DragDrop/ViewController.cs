using System;
using System.Collections.Generic;
using Foundation;
using UIKit;
using CoreGraphics;

namespace DragDrop
{
	public partial class ViewController : UIViewController, IUIGestureRecognizerDelegate
	{
		MyCollectionView sourceCollection;
		NSIndexPath sourceIndexPath;

        UIView draggableView;
		UIImageView draggableImageView;

		public ViewController (IntPtr handle) : base (handle)
		{
			
		}

		public override bool ShouldAutorotate()
		{
			Console.WriteLine("Rotating");
			dragRecognizer.Enabled = false;
			dragRecognizer.Enabled = true;
			return base.ShouldAutorotate();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.

			List<ModelItem> unsortedItems = new List<ModelItem>();

			for (int i = 0; i < 30; i++) 
			{
				ModelItem item = null;
				if (i == 0)
				{
					item = new ModelItem(i, "Image");
				}
				else
				{
					item = new ModelItem(i);
				}
				unsortedItems.Add(item);
			}

			CollectionViewDataSource collectionSource = new CollectionViewDataSource(unsortedItems);
			collectionSource.boundCollectionView = unsortedCollectionView;
			this.unsortedCollectionView.DataSource = collectionSource;

			TableVIewDataSource tableSource = new TableVIewDataSource(15);
			this.tableView.DataSource = tableSource;
//			this.tableView.Delegate = new TableViewDelegate();

			this.dragRecognizer.AddTarget(this, new ObjCRuntime.Selector("HandleLongPress:"));
			this.View.AddGestureRecognizer(this.dragRecognizer);
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

//		private UIImage getUIImageForUIView(UIView view)
//		{
//			UIGraphics.BeginImageContextWithOptions(view.Frame.Size, view.Opaque, (nfloat)0.0);
//			view.Layer.RenderInContext(UIGraphics.GetCurrentContext());
//			UIImage image = UIGraphics.GetImageFromCurrentImageContext();
//			UIGraphics.EndImageContext();
//			return image;
//		}

		[Export("HandleLongPress:")]
		public void HandleTableLongPress(UILongPressGestureRecognizer sender)
		{
			CGPoint tapLocation = sender.LocationInView(this.View);
			UIView hitView = this.View.HitTest(tapLocation, null);
//			Console.WriteLine(hitView.ToString());
//			Console.WriteLine(sender.State.ToString());

			switch (sender.State)
			{
				case UIGestureRecognizerState.Began:
					{
						Console.WriteLine("Gesture Began");
						if (hitView.GetType() == typeof(MyCollectionView))
						{
							sourceCollection = (MyCollectionView)hitView;
							sourceIndexPath = sourceCollection.IndexPathForItemAtPoint(sender.LocationInView(sourceCollection));
							if (sourceIndexPath == null)
							{
								sender.Enabled = false;
								sender.Enabled = true;
								Console.WriteLine("Tap location does not point to any cell");
								return;
							}

							CollectionCell sourceCell = (CollectionCell)sourceCollection.CellForItem(sourceIndexPath);
							sourceCell.Alpha = 0.25f;
//							NSData cellViewMirrorData = NSKeyedArchiver.ArchivedDataWithRootObject(sourceCell);
//							this.draggableView = (UIView)NSKeyedUnarchiver.UnarchiveObject(cellViewMirrorData);

							this.draggableView = sourceCell.SnapshotView(true);
							this.draggableView.Center = tapLocation;
							this.draggableView.Alpha = 0.5f;
							this.draggableView.Hidden = false;
							this.View.AddSubview(this.draggableView);
						}
					}
				break;
				case UIGestureRecognizerState.Changed:
					{
						this.draggableView.Center = tapLocation;
					}
				break;
				case UIGestureRecognizerState.Ended:
					{
						Console.WriteLine("Gesture Ended");

						this.draggableView.RemoveFromSuperview();
						this.draggableView = null;
						
						tapLocation = sender.LocationInView(this.View);
						hitView = this.View.HitTest(tapLocation, null);

						CollectionCell sourceCell = (CollectionCell)sourceCollection.CellForItem(sourceIndexPath);

						if (hitView.GetType() != typeof(MyCollectionView) || hitView.IsEqual(sourceCollection))
						{
							Console.WriteLine("Drop location does not point to any collection");
							sourceCell.Alpha = 1.0f;
							return;
						}
						MyCollectionView destinationCollection = (MyCollectionView)hitView;
						ModelItem item = ((CollectionViewDataSource)sourceCollection.DataSource).items[(int)sourceIndexPath.Item];

						NSIndexPath destinationIndexPath = destinationCollection.IndexPathForItemAtPoint(sender.LocationInView(destinationCollection));
						((CollectionViewDataSource)destinationCollection.DataSource).addItemAtIndexPath(item, destinationIndexPath);

						((CollectionViewDataSource)sourceCollection.DataSource).removeItemAtIndexPath(sourceIndexPath);
//						((CollectionViewDataSource)sourceCollection.DataSource).items.Remove(item);
//						sourceCollection.DeleteItems(new NSIndexPath[] { sourceIndexPath });
					}
				break;
				case UIGestureRecognizerState.Cancelled:
					{
						Console.WriteLine("Gesture Cancelled");
						if (this.draggableView != null)
						{
							this.draggableView.RemoveFromSuperview();
							this.draggableView = null;
						}
						if (sourceIndexPath != null)
						{
							CollectionCell sourceCell = (CollectionCell)sourceCollection.CellForItem(sourceIndexPath);
							sourceCell.Alpha = 1.0f;
						}
					}
				break;
			}
        }
	}
}

