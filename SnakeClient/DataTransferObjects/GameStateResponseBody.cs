using System.Collections.Generic;
using System.Windows;

namespace SnakeClient.DataTransferObjects
{
	public class GameStateResponseBody
	{
        public bool isStarted { get; set; }
		public bool isPaused { get; set; }
        public int roundNumber { get; set; }
        public int turnNumber { get; set; }
        public int turnTimeMilliseconds { get; set; }
        public int timeUntilNextTurnMilliseconds { get; set; }
        public Size gameBoardSize { get; set; }
        public int maxFood { get; set; }
        public List<PlayerStateResponseBody> players { get; set; }
        public List<Point> food { get; set; }
        public List<Rectangle> walls { get; set; }
    }
}
