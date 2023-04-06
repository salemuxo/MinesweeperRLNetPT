namespace SalemLib
{
    public struct Point
    {
        public int X;
        public int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"{X}, {Y}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Point p = (Point)obj;
                return X == p.X && Y == p.Y;
            }
        }

        // generated GetHashCode override
        public override int GetHashCode()
        {
            int hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }
    }

    public struct Rectangle
    {
        public int X;
        public int Y;
        public int W;
        public int H;

        public Rectangle(int x, int y, int w, int h)
        {
            X = x;
            Y = y;
            W = w;
            H = h;
        }

        // generated Equals and GetHashCode overrides
        public override bool Equals(object obj)
        {
            return obj is Rectangle rectangle &&
                   X == rectangle.X &&
                   Y == rectangle.Y &&
                   W == rectangle.W &&
                   H == rectangle.H;
        }

        public override int GetHashCode()
        {
            int hashCode = -1965233216;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            hashCode = hashCode * -1521134295 + W.GetHashCode();
            hashCode = hashCode * -1521134295 + H.GetHashCode();
            return hashCode;
        }
    }
}
