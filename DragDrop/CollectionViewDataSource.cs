using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace DragDrop
{
	public class CollectionViewDataSource:UICollectionViewDataSource
	{
		public List<ModelItem> items;
		public UICollectionView boundCollectionView;

		public CollectionViewDataSource ()
		{
			this.items = new List<ModelItem>();
		}

		public CollectionViewDataSource (ModelItem[] items)
		{
			this.items = new List<ModelItem>(items);
		}

		public CollectionViewDataSource (List<ModelItem> items)
		{
			this.items = items;
		}

		public void addItem(ModelItem item)
		{
//			#warning fix this!!
//			NSIndexPath lastCellIndexPath = NSIndexPath.FromIndex((nuint)this.items.Count);
//
//			this.items.Add(item);
//
//			this.boundCollectionView.InsertItems(new NSIndexPath[] {lastCellIndexPath});
		}

		public override UICollectionViewCell GetCell (UICollectionView collectionView, NSIndexPath indexPath)
		{
			CollectionCell cell = (CollectionCell)collectionView.DequeueReusableCell ("CollectionCell", indexPath);
			if (cell == null)
			{
				cell = (CollectionCell)new UICollectionViewCell();
			}

			cell.setText(this.items[(int)indexPath.Item].value.ToString());

			return cell;
		}

		public override nint GetItemsCount (UICollectionView collectionView, nint section)
		{
			return this.items.Count;
		}

	}
}

