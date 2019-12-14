namespace SnakeClient.DataTransferObjects
{
	public class Rectangle
	{
		public int X;
		public int Y;
		public int width;
		public int height;

		public override string ToString()
		{
			return
				"(" + X + ", " + Y + "), " +
				"(" + (X + width) + ", " + Y + "), " +
				"(" + X + ", " + (Y + height) + "), " +
				"(" + (X + width) + ", " + (Y + height) + "), ";
		}
	}
}
