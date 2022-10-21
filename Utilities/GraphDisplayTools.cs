namespace Utilities
{
    public static class GraphDisplayTools
    {
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
