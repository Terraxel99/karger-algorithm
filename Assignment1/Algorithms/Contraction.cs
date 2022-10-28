using Utilities;

namespace Assignment1.Algorithms
{
    internal static class Contraction
    {
        private const int kargerVerticesRemaining = 2;

        public static int SolveKarger(this Multigraph graph)
        {
            graph.ContractionSequence(limitVtx: kargerVerticesRemaining);

            // The min-cut is the degree of one of the two edges.
            return graph.DegreeOf(0);
        }

        public static Multigraph ContractionSequence(this Multigraph graph, int limitVtx, bool enableDisplay = true)
        {
            while (graph.V > limitVtx)
            {
                graph.ContractRandomEdge(enableDisplay);

                if (enableDisplay)
                {
                    graph.DisplayNeighbors();
                    Console.WriteLine("===============");
                }
            }

            return graph;
        }
    }
}
