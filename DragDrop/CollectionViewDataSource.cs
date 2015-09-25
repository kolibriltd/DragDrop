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
			NSIndexPath lastCellIndexPath = NSIndexPath.FromItemSection(this.boundCollectionView.NumberOfItemsInSection(0), 0);
			this.items.Add(item);

			if (this.boundCollectionView.DataSource != null)
			{
				this.boundCollectionView.InsertItems(new NSIndexPath[] { lastCellIndexPath });
			}
		}

		public void addItemAtIndexPath(ModelItem item, NSIndexPath indexPath)
		{
			//this.items.Add(item);
			if (indexPath == null)
			{
				this.addItem(item);
				return;
			}
			this.items.Insert((int)indexPath.Item, item);
			if (this.boundCollectionView.DataSource != null)
			{
				this.boundCollectionView.InsertItems(new NSIndexPath[] { indexPath });
			}
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

