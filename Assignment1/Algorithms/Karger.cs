using Utilities;

namespace Assignment1.Algorithms
{
    internal static class Karger
    {
        private const int lowerBoundVertices = 2;

        public static int SolveKarger(this Multigraph graph)
        {
            while (graph.V > lowerBoundVertices)
            {
                graph.ContractRandomEdge(enableDisplay: true);
                graph.DisplayNeighbors();
                Console.WriteLine("================");
            }

            // The min-cut is the degree of one of the two edges.
            return graph.DegreeOf(0);
        }
    }
}
