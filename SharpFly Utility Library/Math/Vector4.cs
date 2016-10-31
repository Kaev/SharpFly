namespace SharpFly_Utility_Library.Math
{
    public class Vector4<T>
    {
        public T X { get; set; }
        public T Y { get; set; }
        public T Z { get; set; }
        public T O { get; set; }

        public Vector4() { }

        public Vector4(T x, T y, T z, T o)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.O = o;
        }

        public Vector4(Vector4<T> vector)
        {
            this.X = vector.X;
            this.Y = vector.Y;
            this.Z = vector.Z;
            this.O = vector.O;
        }
    }
}
