namespace SnakeClient.DataTransferObjects
{
    public class Size
	{
		public int width;
		public int height;

		public override string ToString()
		{
			return "(" + width + ", " + height + ")";
		}
	}
}
