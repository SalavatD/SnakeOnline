using System.Collections.Generic;

namespace SnakeClient.DataTransferObjects
{
	public class PlayerStateResponseBody
	{
		public string Name { get; set; }        
		public bool isSpawnProtected { get; set; }
		public List<Point> snake { get; set; }
	}
}
