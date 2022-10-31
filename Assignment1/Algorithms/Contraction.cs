using Utilities;

namespace Assignment1.Algorithms
{
    /// <summary>
    /// Class dedicated to the contraction of graphs.
    /// </summary>
    internal static class Contraction
    {
        private const int kargerVerticesRemaining = 2;

        /// <summary>
        /// Solves the minimum cut problem on the given graph using Karger's contraction algorithm.
        /// </summary>
        /// <param name="graph">The graph on which the algorithm should be executed.</param>
        /// <returns>The cut of minimum size.</returns>
        public static int SolveKarger(this Multigraph graph)
        {
            graph.ContractionSequence(limitVtx: kargerVerticesRemaining, enableDisplay: false);

            // The min-cut is the degree of one of the two edges.
            return graph.DegreeOf(0);
        }

        /// <summary>
        /// Performs a contraction sequence on a given graph until a given amount of vertices has been reached.
        /// </summary>
        /// <param name="graph">The graph on which the algorithm should be executed.</param>
        /// <param name="limitVtx">The number of vertices after which the sequence should stop.</param>
        /// <param name="enableDisplay">Whether the contraction sequence steps should be displayed or not.</param>
        /// <returns></returns>
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
