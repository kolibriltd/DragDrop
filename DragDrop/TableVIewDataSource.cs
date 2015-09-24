using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace DragDrop
{
	public class TableVIewDataSource:UITableViewDataSource
	{
		nint numberOfSections;

		public TableVIewDataSource()
		{
			
		}

		public TableVIewDataSource(nint sectionsCount)
		{
			this.numberOfSections = sectionsCount;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			TableCell cell = (TableCell)tableView.DequeueReusableCell("TableCell", indexPath);
			if (cell == null)
			{
				cell = (TableCell)new UITableViewCell();
			}
			CollectionViewDataSource collectionSource = new CollectionViewDataSource();
			collectionSource.boundCollectionView = cell.collectionView;
			collectionSource.addItem(new ModelItem(55));
			cell.collectionView.DataSource = collectionSource;
			return cell;
		}

		public override nint NumberOfSections(UITableView tableView)
		{
			return numberOfSections;
		}

		public override nint RowsInSection(UITableView tableView, nint section)
		{
			return 1;
		}

		public override string TitleForHeader(UITableView tableView, nint section)
		{
			return string.Format("Section {0}", section + 1);
		}
	}
}

