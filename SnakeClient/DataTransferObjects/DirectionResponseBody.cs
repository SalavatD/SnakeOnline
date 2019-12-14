namespace SnakeClient.DataTransferObjects
{
	public class DirectionResponseBody
	{
		public string direction;
		public string token;

		public DirectionResponseBody(string direction, string token)
		{
			this.direction = direction;
			this.token = token;
		}

		public override string ToString()
		{
			return "Direction: " + direction + ", Token: " + token;
		}
	}
}
