namespace Utilities
{
    internal class Vertex
    {
        public int Number { get; set; }

        public Vertex(int number)
            => this.Number = number;

        public static bool operator ==(Vertex v1, Vertex v2)
            => v1.Number == v2.Number;

        public static bool operator ==(Vertex v1, int v2)
            => v1.Number == v2;

        public static bool operator !=(Vertex v1, Vertex v2)
            => v1.Number != v2.Number;

        public static bool operator !=(Vertex v1, int v2)
            => v1.Number != v2;
    }
}
