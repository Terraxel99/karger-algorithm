namespace Utilities
{
    /// <summary>
    /// Class giving tools to display graph structure.
    /// </summary>
    public static class GraphDisplayTools
    {
        /// <summary>
        /// Displays the neighbors of every vertex of the given graph.
        /// </summary>
        /// <param name="graph">The graph.</param>
        public static void DisplayNeighbors(this Multigraph graph)
        {
            foreach (int v in Enumerable.Range(0, graph.V))
            {
                Console.Write($"Vertex {v} is linked to : ");

                foreach (int w in graph.AdjOf(v))
                {
                    Console.Write(w + " ");
                }

                Console.WriteLine();
            }
        }
    }
}
