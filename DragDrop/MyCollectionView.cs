// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;
using CoreGraphics;

namespace DragDrop
{
	public partial class MyCollectionView : UICollectionView
	{
		public MyCollectionView (IntPtr handle) : base (handle)
		{
		}

		public override UIView HitTest(CGPoint point, UIEvent uievent)
		{
			if (this.PointInside(point, uievent))
			{
				return this;
			}
			return null;
		}
	}
}
