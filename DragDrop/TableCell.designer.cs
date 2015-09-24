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
	[Register ("TableCell")]
	partial class TableCell
	{
		[Outlet]
		public UIKit.UICollectionView collectionView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (collectionView != null) {
				collectionView.Dispose ();
				collectionView = null;
			}
		}
	}
}
