namespace Utilities
{
    /// <summary>
    /// Toolbox-class used for defining an (undirected) edge of the graph.
    /// </summary>
    /// <typeparam name="T">The type used of vertex representation.</typeparam>
    internal class UndirectedEdge<T>
    {
        public T Endpoint1 { get; init; }

        public T Endpoint2 { get; init; }

        public UndirectedEdge(T v1, T v2)
        {
            this.Endpoint1 = v1;
            this.Endpoint2 = v2;
        }
    }
}
