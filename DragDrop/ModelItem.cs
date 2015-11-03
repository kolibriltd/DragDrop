using System;

namespace DragDrop
{
	public class ModelItem
	{
        public int value;
		public string picture;

        public ModelItem (int value)
		{
            this.value = value;
		}

		public ModelItem (string picture)
		{
			this.picture = picture;
		}

		public ModelItem (int value, string picture)
		{
			this.value = value;
			this.picture = picture;
		}
	}
}

