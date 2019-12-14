namespace SnakeClient.DataTransferObjects
{
    public class Point
    {
        public int X;
        public int Y;

        public override string ToString()
        {
            return "(" + X + ", " + Y + ")";
        }
    }
}
