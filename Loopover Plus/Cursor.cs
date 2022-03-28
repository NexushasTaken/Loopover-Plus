namespace Loopover_Plus
{
    public class Cursor
    {
        public int Size { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public Cursor(int x, int y, int size)
        {
            X = x;
            Y = y;
            Size = size;
        }

        public void Move(EMove dir) // dir == direction
        {
            switch (dir)
            {
                case EMove.Up:
                    if (Y > 0)
                        --Y;
                    else
                        Y = Size - 1;
                    break;
                case EMove.Down:
                    if (Y < Size - 1)
                        ++Y;
                    else
                        Y = 0;
                    break;
                case EMove.Left:
                    if (X > 0)
                        --X;
                    else
                        X = Size - 1;
                    break;
                case EMove.Right:
                    if (X < Size - 1)
                        ++X;
                    else
                        X = 0;
                    break;
                default:
                    break;
            }

        }
    }
}

